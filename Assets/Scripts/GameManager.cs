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

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text resultScoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text expText;

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
        highscore = PlayerPrefs.GetInt("Song A");
        currentScene = SceneManager.GetActiveScene();
        //Debug.Log(Camera.main.pixelWidth);
    }

    void Update()
    {
        scoreText.text = score.ToString();
        GameOver();
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
                PlayerPrefs.SetInt("Song A", highscore);
            }
            resultScoreText.text = score.ToString();
            highscoreText.text = highscore.ToString();

            PlayerPrefs.SetInt("Star", star);
        }
    }

    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void Retry()
    {
        SceneManager.LoadScene(currentScene.name);
    }
}
