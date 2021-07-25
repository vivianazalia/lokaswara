using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;
    public bool resultCount = true;
    private bool alreadySetStar = false;

    public int score = 0;
    private int highscore;
    private int exp;
    private int totalExp;
    public int star = 0;
    public const int maxStar = 3;
    private float countdownToStart = 3;
    public bool startScroll;
    private bool isPaused = false;

    [Header("Assets for UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text resultScoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text expText;
    [SerializeField] protected Text countdownToStartText;
    [SerializeField] private GameObject[] starsImage = new GameObject[3];
    [SerializeField] private Sprite[] starsSprite = new Sprite[2];

    [SerializeField] private string locationHighscore;
    [SerializeField] private string locationStar;

    private Scene currentScene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt(locationHighscore);
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        CountdownToStart();
        GameOver();
    }

    private void CountdownToStart()
    {
        if (countdownToStart > 1 && !isPaused)
        {
            startScroll = false;
            countdownToStart -= Time.deltaTime;
            countdownToStartText.text = Mathf.RoundToInt(countdownToStart).ToString();
            pauseButton.SetActive(false);
        }
        else
        {
            if (!isPaused)
            {
                pauseButton.SetActive(true);
                startScroll = true;
            }

            countdownToStartText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            scoreText.text = score.ToString();
        }
    }

    public int GetMaxStar()
    {
        return maxStar;
    }

    private void GameOver()
    {
        if (isGameOver)
        {
            scoreText.gameObject.SetActive(false);
            pauseButton.SetActive(false);
            exp = Mathf.RoundToInt(score / 10);
            if(exp < 1)
            {
                exp = 1;
            }
            expText.text = "Exp : " + exp.ToString();
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
            highscoreText.text = "Highscore : " + highscore.ToString();

            if(PlayerPrefs.GetInt(locationStar) < maxStar)
            {
                PlayerPrefs.SetInt(locationStar, star);
            }

            StartCoroutine(DelayBeforeGameoverPanel());

            SetStarImage();

            if (!PlayerPrefs.HasKey("AppFirstRun"))
            {
                PlayerPrefs.SetInt("AppFirstRun", 1);
            }
        }
    }

    IEnumerator DelayBeforeGameoverPanel()
    {
        yield return new WaitForSeconds(1);
        gameOverPanel.SetActive(true);
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

    public void Pause()
    {
        if (!isPaused)
        {
            startScroll = false;
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
            isPaused = true;
        }
        
    }

    public void Resume()
    {
        if (pausePanel.activeInHierarchy && isPaused)
        {
            pausePanel.SetActive(false);
            countdownToStart = 3;
            countdownToStartText.gameObject.SetActive(true);
            isPaused = false;
        }
    }

    void SetStarImage()
    {
        if (!alreadySetStar)
        {
            if (star >= starsImage.Length)
            {
                star = starsImage.Length;
            }

            for (int i = 0; i < star; i++)
            {
                if (i % 2 == 0)
                {
                    GameObject smallStar = new GameObject("star");
                    smallStar.transform.SetParent(starsImage[i].transform);
                    Image star = smallStar.AddComponent<Image>();
                    star.sprite = starsSprite[0];
                    RectTransform starRect = smallStar.GetComponent<RectTransform>();
                    starRect.localScale = new Vector3(.8f, .8f, .8f);
                    starRect.localPosition = new Vector3(0, 0, 0);
                    starRect.sizeDelta = new Vector2(70, 70);
                }
                else
                {
                    GameObject bigStar = new GameObject("star");
                    bigStar.transform.SetParent(starsImage[i].transform);
                    Image star = bigStar.AddComponent<Image>();
                    star.sprite = starsSprite[1];
                    RectTransform starRect = bigStar.GetComponent<RectTransform>();
                    starRect.localScale = new Vector3(.8f, .8f, .8f);
                    starRect.localPosition = new Vector3(0, 0, 0);
                    starRect.sizeDelta = new Vector2(120, 120);
                }
            }
            alreadySetStar = true;
        }
    }

}
