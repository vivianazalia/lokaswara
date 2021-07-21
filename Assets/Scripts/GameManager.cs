using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    public bool isGameOver = false;
    public bool resultCount = true;

    public int score = 0;
    private int highscore;
    private int exp;
    private int totalExp;
    public int star = 0;
    public const int maxStar = 3;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text resultScoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text expText;

    [SerializeField] private string locationHighscore;
    [SerializeField] private string locationStar;

    private Scene currentScene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt(locationHighscore);
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        scoreText.text = score.ToString();
        GameOver();
    }

    public int GetMaxStar()
    {
        return maxStar;
    }

    private void GameOver()
    {
        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            exp = Mathf.RoundToInt(score / 10);
            if(exp < 1)
            {
                exp = 1;
            }
            expText.text = exp.ToString();
            if (resultCount)
            {
                totalExp = PlayerPrefs.GetInt("Exp Point") + exp;
                PlayerPrefs.SetInt("Exp Point", totalExp);
                resultCount = false;
            }
            
            if(score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt(locationHighscore, highscore);
            }
            resultScoreText.text = score.ToString();
            highscoreText.text = highscore.ToString();

            if(PlayerPrefs.GetInt(locationStar) < maxStar)
            {
                PlayerPrefs.SetInt(locationStar, star);
            }
        }
    }

    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void Retry()
    {
        int currentHeart = PlayerPrefs.GetInt("Heart");
        if(currentHeart > 0)
        {
            currentHeart--;
            PlayerPrefs.SetInt("Heart", currentHeart);
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
