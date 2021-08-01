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
    [SerializeField] private GameObject popupEmptyHeart;

    private const float timerMax = 120;
    private float timerCountDown;
    private int totalHeart;
    private const int maxHeart = 5;

    private int seconds;
    private int minutes;

    public AudioSource audio;
    public AudioSource bgm;

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
        totalExp = PlayerPrefs.GetInt("TotalExp");
        currentExp = PlayerPrefs.GetInt("Exp Point");
        currentLevel = PlayerPrefs.GetInt("Level");

        CheckForFirstRun();

        currentExpText.text = currentExp.ToString();
        totalExpText.text = totalExp.ToString();
        levelText.text = currentLevel.ToString();
        audio.volume = PlayerPrefs.GetFloat("SfxVolume");
        bgm.volume = PlayerPrefs.GetFloat("BgmVolume");
    }

    void Update()
    {
        LevelUp();
        CountDown();

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerPrefs.SetString("LastTime", System.DateTime.Now.ToString());
                PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
                PlayerPrefs.SetInt("Heart", totalHeart);
                SceneManager.LoadScene(0);
            }
        }
    }

    void CheckForFirstRun()
    {
        totalHeart = PlayerPrefs.GetInt("Heart");
        timerCountDown = PlayerPrefs.GetInt("TimerCountDown");

        if (PlayerPrefs.HasKey("AppFirstRun"))
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

                PlayerPrefs.SetInt("Heart", totalHeart);
            }
            else
            {
                timerCountDown -= TimeManager.Instance.DifferenceSeconds();
            }
        }

        heartText.text = totalHeart.ToString();
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
            totalExp += (350 * currentLevel);
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

    public void PopupHeart(bool state)
    {
        popupEmptyHeart.SetActive(state);
    }

    public void Close()
    {
        audio.Play();
        if (popupEmptyHeart.activeInHierarchy)
        {
            popupEmptyHeart.SetActive(false);
        }
    }

    public void OnSceneChange()
    {
        PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
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
            timerCountDown = PlayerPrefs.GetInt("TimerCountDown");
            totalHeart = PlayerPrefs.GetInt("Heart");
            CheckForFirstRun();
        }
    
        if (!focus)
        {
            PlayerPrefs.SetString("LastTime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
            PlayerPrefs.SetInt("Heart", totalHeart);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            timerCountDown = PlayerPrefs.GetInt("TimerCountDown");
            totalHeart = PlayerPrefs.GetInt("Heart");
            CheckForFirstRun();
        }
    
        if (pause)
        {
            PlayerPrefs.SetString("LastTime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("TimerCountDown", (int)timerCountDown);
            PlayerPrefs.SetInt("Heart", totalHeart);
        }
    }
}
