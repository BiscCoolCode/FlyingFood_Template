using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text _ScoreText;
    public int Score { get; private set; }


    private void Awake()
    {
        if(Instance == null)
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
        _ScoreText.text = "Chewinggum - score: 0";
    }

    public void IncreaseScore()
    {
        Score++;
        _ScoreText.text = "Chewinggum-score: "+Score;
    }
}
