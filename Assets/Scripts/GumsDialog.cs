using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GumsDialog : MonoBehaviour
{
    [TextAreaAttribute][SerializeField] private string[] _Dialog;
    [SerializeField] private Button _OkButton;
    [SerializeField] private GameObject _Gum;

    private TMP_Text _textbox;
    private int _dialogNumber;
    public float _targetScaleGum;
    private AudioSource _talkAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textbox = GetComponent<TMP_Text>();
        _talkAudio = _Gum.GetComponent<AudioSource>();
        // ChangeText();
        _OkButton.onClick.AddListener(ChangeText);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentState != GameState.Intro)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // 0 = Left Click
        {
            SkipText();
        }

        _Gum.transform.localScale = new Vector3(_Gum.transform.localScale.x, Mathf.Lerp(_Gum.transform.localScale.y, _targetScaleGum, 0.5f), _Gum.transform.localScale.z);
    }

    private void PrintText()
    {
        _talkAudio.Play();
        print("PRINT TEXT");
        TextAsync();
    }

    private void TextAsync()
    {
        print("TEXT ASYNC");
        if(_textbox.maxVisibleCharacters < _textbox.text.Length)
        {
            print("Text");
            _targetScaleGum = Random.Range(2.9f, 3.1f);
            _textbox.maxVisibleCharacters++;
            Invoke(nameof(TextAsync), 1f);
        }
        else
        {
            print("DIALOG ENDED");
            _talkAudio.Stop();
            _OkButton.gameObject.SetActive(true);
        }

    }

    private void SkipText()
    {
        _textbox.maxVisibleCharacters = _textbox.text.Length;
    }

    public void ChangeText()
    {
        print(_dialogNumber);
        if(_dialogNumber == _Dialog.Length)
        {
            GameManager.Instance.ChangeState(GameState.Play);
            transform.parent.gameObject.SetActive(false);
            return;
        }

        _textbox.text = _Dialog[_dialogNumber];
        _OkButton.gameObject.SetActive(false);
        _textbox.maxVisibleCharacters = 0;
        PrintText();
        _dialogNumber++;
    }
}
