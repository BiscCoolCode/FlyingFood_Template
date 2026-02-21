using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public class GumsDialog : MonoBehaviour
{
    [TextAreaAttribute][SerializeField] private string[] _Dialog;

    private TMP_Text _textbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _textbox = GetComponent<TMP_Text>();
        _textbox.maxVisibleCharacters = 0;
        PrintText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void PrintText()
    {
        while(_textbox.maxVisibleCharacters < _textbox.text.Length)
        {
            _textbox.maxVisibleCharacters++;
            await Task.Delay(50);
        }
        
    }
}
