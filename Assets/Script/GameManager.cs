using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    
    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public Text scoreText;
    private void Start()
    {
        startPage.SetActive(true);
    }
    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }
    int score = 0;
    bool gameOver = true;
    public bool GameOver { get { return gameOver; } }
    public int Score { get { return score; } }
    private void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        CountDown.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
        AdManager.Instance.ShowInterstitial();
        AdManager.Instance.ShowBanner();
    }
    void OnDisable()
    {
        CountDown.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }
    void OnPlayerDied()
    {
        gameOver = true;
        int saveScore = PlayerPrefs.GetInt("HighScore");
        if (score > saveScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);
    }
    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }
    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }
    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);
                break;
        }
    }
    public void ConfirmGameOver()
    {
        //activate replay button on hit 
        OnGameOverConfirmed();
        scoreText.text = "0";
        SetPageState(PageState.Start);
    }
    public void StartGame()
    {
        //activated when play button is hit
        SetPageState(PageState.Countdown);
    }
}
//Design and Made by Rathod Studio