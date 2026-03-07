using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _Foodcanon;

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
        ChangeState(GameState.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Intro:
                CurrentState = GameState.Intro;
                Time.timeScale = 0;
                _Player.GetComponent<ExamplePlayer>().enabled = false;
                _Foodcanon.SetActive(false);
                break;

            case GameState.Play:
                CurrentState = GameState.Play;
                Time.timeScale = 1;
                _Player.GetComponent<ExamplePlayer>().enabled = true;
                _Foodcanon.SetActive(true);
                break;
        }
    }
}
