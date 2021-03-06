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
    [SerializeField] private Text countdownToStartText;
    [SerializeField] private Text gameoverText;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private GameObject[] starsImage = new GameObject[3];
    [SerializeField] private Sprite[] starsSprite = new Sprite[2];

    [SerializeField] private string locationHighscore;
    [SerializeField] private string locationStar;

    [SerializeField] private AudioSource sfx;

    [SerializeField] private GameObject popupHeart;

    private string[] motivationText = new string[] { "Semangat!!", "Yuk, pasti bisa!", "Jangan menyerah!", "Ayo, coba lagi!"}; 

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
        sfx.volume = PlayerPrefs.GetFloat("SfxVolume");

        SetGameoverText();
    }

    void Update()
    {
        CountdownToStart();
        GameOver();
    }

    void SetGameoverText()
    {
        int randIndex = Random.Range(0, motivationText.Length);
        gameoverText.text = motivationText[randIndex];
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
            countdownPanel.SetActive(false);
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
            expText.text = "+" + exp.ToString();
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
            highscoreText.text = "Skor tertinggi : " + highscore.ToString();

            if(star > PlayerPrefs.GetInt(locationStar))
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
        sfx.Play();
        SceneManager.LoadScene("Map");
    }

    public void Retry()
    {
        sfx.Play();
        int currentHeart = PlayerPrefs.GetInt("Heart");
        if(currentHeart > 0)
        {
            currentHeart -= 1;
            PlayerPrefs.SetInt("Heart", currentHeart);
            SceneManager.LoadScene(currentScene.name);
        }
        else
        {
            PopupHeart(true);
        }
    }

    public void Pause()
    {
        sfx.Play();
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
        sfx.Play();
        if (pausePanel.activeInHierarchy && isPaused)
        {
            pausePanel.SetActive(false);
            countdownToStart = 3;
            countdownToStartText.gameObject.SetActive(true);
            countdownPanel.SetActive(true);
            isPaused = false;
        }
    }

    public void PopupHeart(bool state)
    {
        popupHeart.SetActive(state);
    }

    public void Close()
    {
        sfx.Play();
        if (popupHeart.activeInHierarchy)
        {
            popupHeart.SetActive(false);
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
                    starRect.localScale = new Vector3(2f, 2f, 2f);
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
                    starRect.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    starRect.localPosition = new Vector3(0, 0, 0);
                    starRect.sizeDelta = new Vector2(120, 120);
                }
            }
            alreadySetStar = true;
        }
    }

}
