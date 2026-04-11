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
                //Time.timeScale = 0;
                //_Player.enabled = false;
                //_Player.CharacterCamera.enabled = false;
                //_Foodcanon.SetActive(false);
                _Player.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                
                break;

            case GameState.Play:
                CurrentState = GameState.Play;
                Time.timeScale = 1;
                //_Player.enabled = true;
                //_Player.CharacterCamera.enabled = true;
                //_Foodcanon.SetActive(true);
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
            //_Player.enabled = false;
            _GameUI.SetActive(false);
            //_MenuCamera.enabled = true;
            _MenuUI.SetActive(true);
            //_Crosshair.SetActive(false);
        }
        else
        {
            //_Player.enabled = true;
            _GameUI.SetActive(true);
            //_MenuCamera.enabled = false;
            _MenuUI.SetActive(false);
            //_Crosshair.SetActive(true);
        }
    }

    public async void StartGame()
    {
        EnableMenu(false);
        _MenuCamera.gameObject.transform.DOMove(_Player.CharacterCamera.transform.position, _TransitionDurarion);
        await Task.Delay(1250);
        _MenuCamera.gameObject.transform.DORotate(_Player.CharacterCamera.transform.rotation.eulerAngles, _TransitionDurarion).OnComplete(() => ChangeState(GameState.Intro));
    }
}
