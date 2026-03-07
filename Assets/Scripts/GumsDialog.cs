using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.UI;

public class GumsDialog : MonoBehaviour
{
    [TextAreaAttribute][SerializeField] private string[] _Dialog;
    [SerializeField] private Button _OkButton;

    private TMP_Text _textbox;
    private int _dialogNumber;

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

    private void ChangeText()
    {
        print(_dialogNumber);
        if(_dialogNumber == _Dialog.Length)
        {
            GameManager.Instance.ChangeState(GameState.Play);
            return;
        }

        _textbox.text = _Dialog[_dialogNumber];
        _OkButton.gameObject.SetActive(false);
        _textbox.maxVisibleCharacters = 0;
        PrintText();
        _dialogNumber++;
    }
}
