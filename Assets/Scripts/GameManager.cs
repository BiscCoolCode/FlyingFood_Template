
using KinematicCharacterController.Examples;
using UnityEngine;
using DG.Tweening;
using System.Collections;

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
    public GameState CurrentState { get; private set; }

    private Coroutine _startGameCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.StartMenu);
    }

    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.StartMenu:
                CurrentState = GameState.StartMenu;
                Time.timeScale = 1f;

                EnableMenu(true);
                Cursor.lockState = CursorLockMode.None;
                _Player.gameObject.SetActive(false);
                _MenuCamera.gameObject.SetActive(true);
                break;

            case GameState.Intro:
                CurrentState = GameState.Intro;
                Time.timeScale = 0f;

                _Player.gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;

                // Zuerst das UI aktivieren.
                _GameUI.SetActive(true);

                // Danach den Dialog starten.
                _GumsDialog.StartDialog();
                break;

            case GameState.Play:
                CurrentState = GameState.Play;
                Time.timeScale = 1f;

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

    public void StartGame()
    {
        if (_startGameCoroutine != null)
        {
            StopCoroutine(_startGameCoroutine);
        }

        _startGameCoroutine = StartCoroutine(StartGameTransition());
    }

    private IEnumerator StartGameTransition()
    {
        _MenuUI.SetActive(false);

        _MenuCamera.transform.DOMove(
            _Player.CharacterCamera.transform.position,
            _TransitionDurarion
        );

        yield return new WaitForSecondsRealtime(1.25f);

        _MenuCamera.transform.DORotate(
            _Player.CharacterCamera.transform.rotation.eulerAngles,
            _TransitionDurarion
        ).OnComplete(() =>
        {
            _startGameCoroutine = null;
            ChangeState(GameState.Intro);
        });
    }
}
