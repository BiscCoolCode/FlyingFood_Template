
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GumsDialog : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string[] _Dialog;

    [SerializeField] private Button _OkButton;
    [SerializeField] private GameObject _Gum;

    private TMP_Text _textbox;
    private int _dialogNumber;
    private float _targetScaleGum;
    private AudioSource _talkAudio;
    private Coroutine _printCoroutine;
    private bool _initialized;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _textbox = GetComponent<TMP_Text>();
        _talkAudio = _Gum.GetComponent<AudioSource>();

        _OkButton.onClick.AddListener(ChangeText);

        _initialized = true;
    }

    public void StartDialog()
    {
        // Das UI wird im GameManager aktiviert.
        // Aktiviere zusätzlich das Dialog-Panel.
        if (transform.parent != null)
        {
            transform.parent.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);

        Initialize();

        ChangeText();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Intro)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SkipText();
        }

        _Gum.transform.localScale = new Vector3(
            _Gum.transform.localScale.x,
            Mathf.Lerp(
                _Gum.transform.localScale.y,
                _targetScaleGum,
                0.5f
            ),
            _Gum.transform.localScale.z
        );
    }

    private IEnumerator PrintText()
    {
        _talkAudio.Play();

        while (_textbox.maxVisibleCharacters < _textbox.text.Length)
        {
            _targetScaleGum = Random.Range(2.9f, 3.1f);
            _textbox.maxVisibleCharacters++;

            // Funktioniert auch bei Time.timeScale = 0.
            yield return new WaitForSecondsRealtime(0.025f);
        }

        _talkAudio.Stop();
        _OkButton.gameObject.SetActive(true);

        _printCoroutine = null;
    }

    private void SkipText()
    {
        if (_printCoroutine == null)
        {
            return;
        }

        _textbox.maxVisibleCharacters = _textbox.text.Length;

        StopCoroutine(_printCoroutine);
        _printCoroutine = null;

        _talkAudio.Stop();
        _OkButton.gameObject.SetActive(true);
    }

    public void ChangeText()
    {
        if (_dialogNumber >= _Dialog.Length)
        {
            GameManager.Instance.ChangeState(GameState.Play);

            if (transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }

            return;
        }

        if (_printCoroutine != null)
        {
            StopCoroutine(_printCoroutine);
            _printCoroutine = null;
        }

        _talkAudio.Stop();

        _textbox.text = _Dialog[_dialogNumber];
        _textbox.maxVisibleCharacters = 0;
        _OkButton.gameObject.SetActive(false);

        _printCoroutine = StartCoroutine(PrintText());

        _dialogNumber++;
    }

    private void OnDisable()
    {
        if (_printCoroutine != null)
        {
            StopCoroutine(_printCoroutine);
            _printCoroutine = null;
        }

        if (_talkAudio != null)
        {
            _talkAudio.Stop();
        }
    }

    private void OnDestroy()
    {
        if (_OkButton != null)
        {
            _OkButton.onClick.RemoveListener(ChangeText);
        }
    }
}
