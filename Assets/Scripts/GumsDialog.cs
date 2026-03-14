using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GumsDialog : MonoBehaviour
{
    [TextAreaAttribute][SerializeField] private string[] _Dialog;
    [SerializeField] private Button _OkButton;
    [SerializeField] private GameObject _Gum;

    private TMP_Text _textbox;
    private int _dialogNumber;
    private float _targetScaleGum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textbox = GetComponent<TMP_Text>();
        ChangeText();
        _OkButton.onClick.AddListener(ChangeText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = Left Click
        {
            SkipText();
        }

        _Gum.transform.localScale = new Vector3(_Gum.transform.localScale.x, Mathf.Lerp(_Gum.transform.localScale.y, _targetScaleGum, 0.5f), _Gum.transform.localScale.z);
    }

    private async void PrintText()
    {
        while (_textbox.maxVisibleCharacters < _textbox.text.Length)
        {
            _textbox.maxVisibleCharacters++;
            await Task.Delay(50);
        }

        _OkButton.gameObject.SetActive(true);
        
    }

    private void SkipText()
    {
        _textbox.maxVisibleCharacters = _textbox.text.Length;
    }

    private void ChangeText()
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
