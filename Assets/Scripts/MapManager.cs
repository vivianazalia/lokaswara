using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private GameObject jatimPanel;

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

    [SerializeField] private List<Sprite> heartImages = new List<Sprite>();

    private static MapManager instance = null;
    public static MapManager Instance { get { return instance; } }

    void Start()
    {
        if (!PlayerPrefs.HasKey("AppFirstRun"))
        {
            currentLevel = 1;
            currentExp = 0;
            PlayerPrefs.SetInt("Level", currentLevel);
            PlayerPrefs.SetInt("Exp Point", currentExp);
            PlayerPrefs.SetInt("AppFirstRun", 1);
        }

        currentExp = PlayerPrefs.GetInt("Exp Point");
        currentExpText.text = currentExp.ToString();
        totalExpText.text = totalExp.ToString();
        currentLevel = PlayerPrefs.GetInt("Level");
        levelText.text = currentLevel.ToString();

        #region Song A
        songAHighscore = PlayerPrefs.GetInt("Song A");
        songAHighscoreText.text = songAHighscore.ToString();
        #endregion
        
    }

    void Update()
    {
        LevelUp();
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
        infoSongA.SetActive(true);
    }

    public void PlaySongA()
    {
        SceneManager.LoadScene("Song A");
    }

    public void Close()
    {
        if (jatimPanel.activeSelf)
        {
            jatimPanel.SetActive(false);
        }
    }

    public void CloseInfo()
    {
        if (infoSongA.activeSelf)
        {
            infoSongA.SetActive(false);
        }
    }
}
