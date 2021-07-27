using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Profil")]
    [SerializeField] private Text currentExpText;
    [SerializeField] private Text totalExpText;
    [SerializeField] private Text levelText;
    private int currentExp;
    private int totalExp;
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
        totalHeart = PlayerPrefs.GetInt("Heart");
        totalExp = PlayerPrefs.GetInt("TotalExp");
        timerCountDown = PlayerPrefs.GetInt("TimerCountDown");
        currentExp = PlayerPrefs.GetInt("Exp Point");
        currentLevel = PlayerPrefs.GetInt("Level");

        CheckForFirstRun();

        currentExpText.text = currentExp.ToString();
        totalExpText.text = totalExp.ToString();
        levelText.text = currentLevel.ToString();
        heartText.text = totalHeart.ToString();
        PlayerPrefs.SetInt("Heart", totalHeart);
    }

    void Update()
    {
        LevelUp();
        CountDown();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void CheckForFirstRun()
    {
        if(PlayerPrefs.HasKey("AppFirstRun"))
        {
            if (TimeManager.Instance.DifferenceSeconds() > timerCountDown)
            {
                totalHeart += 1;
                int remaining = (int)(TimeManager.Instance.DifferenceSeconds() - (int)timerCountDown);
                if (remaining > timerMax)
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
        }
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
            totalExp = (totalExp * 2);
            PlayerPrefs.SetInt("TotalExp", totalExp);
            totalExpText.text = totalExp.ToString();
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
                PlayerPrefs.SetInt("Heart", totalHeart);
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

    public void OnSceneChange()
    {
        PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
    }

    public void Close()
    {
        if (settingPanel.activeInHierarchy)
        {
            settingPanel.SetActive(false);
        } 
        else if (creditPanel.activeInHierarchy)
        {
            creditPanel.SetActive(false);
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

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
        PlayerPrefs.SetInt("Heart", totalHeart);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
            PlayerPrefs.SetInt("Heart", totalHeart);
        }
        else
        {
            timerCountDown = PlayerPrefs.GetInt("TimerCountDown");
            totalHeart = PlayerPrefs.GetInt("Heart");
        }
    }
}
