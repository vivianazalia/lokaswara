using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private GameObject jatimPanel;
    [SerializeField] private Transform parentInfoPanel;
    private Transform firstParent;

    [Header("Song A")]
    [SerializeField] private GameObject infoSongA;
    [SerializeField] private Text songAHighscoreText;
    private int songAHighscore;

    [Header("Profil")]
    [SerializeField] private Text currentExpText;
    [SerializeField] private Text totalExpText;
    [SerializeField] private Text levelText;
    private int currentExp;
    private int totalExp = 10;
    private int currentLevel;

    [Header("Heart")]
    [SerializeField] private Text minutesText;
    [SerializeField] private Text secondsText;
    [SerializeField] private Text heartText;
    [SerializeField] private Text fullText;

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;

    [Header("Credit")]
    [SerializeField] private GameObject creditPanel;

    private const float timerMax = 180;
    private float timerCountDown;
    private int totalHeart;
    private const int maxHeart = 3;

    private int seconds;
    private int minutes;

    [SerializeField] private List<Sprite> heartImages = new List<Sprite>();

    public static MapManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            //do tutorial
            PlayerPrefs.SetInt("AppFirstRun", 1);
        }
        else
        {
            totalHeart = PlayerPrefs.GetInt("Heart");
            timerCountDown = PlayerPrefs.GetInt("TimerCountDown");

            if(TimeManager.Instance.DifferenceSeconds() > timerCountDown)
            {
                totalHeart += 1;
                int remaining = (int)(TimeManager.Instance.DifferenceSeconds() - (int)timerCountDown);
                if(remaining > timerMax)
                {
                    int divide = remaining / (int)timerMax;
                    totalHeart += divide;
                    timerCountDown = timerMax - (remaining % (int)timerMax);
                }
                else
                {
                    timerCountDown = timerMax - remaining;
                }

                if (totalHeart >= maxHeart)
                {
                    totalHeart = maxHeart;
                    timerCountDown = timerMax;
                }
            }
            else
            {
                timerCountDown -= TimeManager.Instance.DifferenceSeconds();
            }
           
            currentExp = PlayerPrefs.GetInt("Exp Point");
            currentExpText.text = currentExp.ToString();
            totalExpText.text = totalExp.ToString();
            currentLevel = PlayerPrefs.GetInt("Level");
            levelText.text = currentLevel.ToString();
        }

        #region Song A
        songAHighscore = PlayerPrefs.GetInt("Song A");
        songAHighscoreText.text = songAHighscore.ToString();
        #endregion
        
    }

    void Update()
    {
        LevelUp();
        CountDown();
    }

    void LevelUp()
    {
        if (currentExp >= totalExp)
        {
            int substractExp = currentExp - totalExp;
            currentLevel++;
            PlayerPrefs.SetInt("Level", currentLevel);
            levelText.text = currentLevel.ToString();
            currentExp = substractExp;
            PlayerPrefs.SetInt("Exp Point", currentExp);
            currentExpText.text = currentExp.ToString();
        }
    }

    void CountDown()
    {
        if(totalHeart < maxHeart)
        {
            fullText.gameObject.SetActive(false);
            minutesText.gameObject.SetActive(true);
            secondsText.gameObject.SetActive(true);

            timerCountDown -= Time.deltaTime;

            if (timerCountDown > 60)
            {
                minutes = Mathf.RoundToInt(timerCountDown) / 60;
                seconds = Mathf.RoundToInt(timerCountDown) % 60;
            }
            else
            {
                minutes = 0;
                seconds = (int)timerCountDown;
            }

            if (Mathf.RoundToInt(timerCountDown) <= 0)
            {
                totalHeart++;
                timerCountDown = timerMax;
            }
        }
        else
        {
            fullText.gameObject.SetActive(true);
            minutesText.gameObject.SetActive(false);
            secondsText.gameObject.SetActive(false);
        }

        heartText.text = totalHeart.ToString();

        if(minutes < 10)
        {
            minutesText.text = "0" + minutes.ToString();
        }
        else
        {
            minutesText.text = minutes.ToString();
        }

        if (seconds < 10)
        {
            secondsText.text = "0" + seconds.ToString();
        }
        else
        {
            secondsText.text = seconds.ToString();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
        PlayerPrefs.SetInt("Heart", totalHeart);
    }

    public void JawaTimur()
    {
        jatimPanel.SetActive(true);
    }

    public void JawaTengah()
    {

    }

    public void JawaBarat()
    {

    }

    public void SongAInfo()
    {
        firstParent = infoSongA.gameObject.transform.parent;
        infoSongA.SetActive(true);
        infoSongA.gameObject.transform.parent = parentInfoPanel;
    }

    public void PlaySongA()
    {
        if(totalHeart > 0)
        {
            totalHeart--;
            PlayerPrefs.SetString("LastTime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
            PlayerPrefs.SetInt("Heart", totalHeart);
            SceneManager.LoadScene("Song A");
        }
    }

    public void Close()
    {
        if (jatimPanel.activeSelf)
        {
            jatimPanel.SetActive(false);
        } 
        else if (settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
        } 
        else if (creditPanel.activeSelf)
        {
            creditPanel.SetActive(false);
        }
    }

    public void CloseInfo()
    {
        if (infoSongA.activeSelf)
        {
            infoSongA.SetActive(false);
            infoSongA.gameObject.transform.parent = firstParent;
        }
    }

    public void Setting()
    {
        settingPanel.SetActive(true);
    }

    public void Credit()
    {
        creditPanel.SetActive(true);
    }
}
