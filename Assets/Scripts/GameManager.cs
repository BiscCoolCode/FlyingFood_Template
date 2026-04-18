using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using DG.Tweening;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ExamplePlayer _Player;
    [SerializeField] private GameObject _Foodcanon;
    [SerializeField] private Camera _MenuCamera;
    [SerializeField] private GameObject _MenuUI;
    [SerializeField] private GameObject _GameUI;
    [SerializeField] private GameObject _Crosshair;
    [SerializeField] private float _TransitionDurarion = 5f;
    [SerializeField] private GumsDialog _GumsDialog;

    public static GameManager Instance { get; private set; }
    public GameState CurrentState {  get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState(GameState.StartMenu);
    }


    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.StartMenu:
                CurrentState = GameState.StartMenu;
                EnableMenu(true);
                Cursor.lockState = CursorLockMode.None;
                _Player.gameObject.SetActive(false);
                _MenuCamera.gameObject.SetActive(true);
                break;

            case GameState.Intro:
                CurrentState = GameState.Intro;
                Time.timeScale = 0;
                _Player.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                _GumsDialog.ChangeText();
                _GameUI.SetActive(true);
                break;

            case GameState.Play:
                CurrentState = GameState.Play;
                Time.timeScale = 1;
                _Player.gameObject.SetActive(true);
                _MenuCamera.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                EnableMenu(false);
                break;
        }
    }

    private void EnableMenu(bool enabled)
    {
        if (enabled)
        {
            _GameUI.SetActive(false);
            _MenuUI.SetActive(true);
        }
        else
        {
            _GameUI.SetActive(true);
            _MenuUI.SetActive(false);
        }
    }

    public async void StartGame()
    {
        _MenuUI.SetActive(false);
        _MenuCamera.gameObject.transform.DOMove(_Player.CharacterCamera.transform.position, _TransitionDurarion);
        await Task.Delay(1250);
        _MenuCamera.gameObject.transform.DORotate(_Player.CharacterCamera.transform.rotation.eulerAngles, _TransitionDurarion).OnComplete(() => 
        {
            print("OnComplete");
            ChangeState(GameState.Intro);
        });
    }
}
