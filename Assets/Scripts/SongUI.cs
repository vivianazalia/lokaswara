using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongUI : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    [Header("UI")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject parentInfoPanel;
    [SerializeField] private GameObject starContainer;
    [SerializeField] private Text unlockLevelText;
    public GameObject lockPanel;
    public Button playButton;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Sprite starImage;

    [Header("PlayerPrefs Key")]
    [SerializeField] private string locationHighscore;
    [SerializeField] private string locationStar;

    private Transform firstParent;
    private int totalHeart;
    private int highscore;
    private int star;
    [SerializeField] private int levelToUnlock;
    private Image[] stars = new Image[3];

    [HideInInspector] public bool isClicked;

    private void Start()
    {
        firstParent = infoPanel.transform.parent;
        highscore = PlayerPrefs.GetInt(locationHighscore);
        star = PlayerPrefs.GetInt(locationStar);

        highscoreText.text = highscore.ToString();
        unlockLevelText.text = "Terbuka saat Lv." + levelToUnlock;

        for(int i = 0; i < stars.Length; i++)
        {
            stars[i] = starContainer.transform.Find("Star" + i.ToString()).GetComponent<Image>();
        }

        SetStarImage(starImage);
    }

    public void PlayGame()
    {
        MapManager.Instance.audio.Play();
        isClicked = true;
        totalHeart = PlayerPrefs.GetInt("Heart");
        if (totalHeart > 0)
        {
            totalHeart--;
            PlayerPrefs.SetString("LastTime", System.DateTime.Now.ToString());
            MapManager.Instance.OnSceneChange();
            PlayerPrefs.SetInt("Heart", totalHeart);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            MapManager.Instance.PopupHeart(true);
        }
    }

    public void ShowInfo()
    {
        MapManager.Instance.audio.Play();
        if (!infoPanel.activeInHierarchy)
        {
            infoPanel.SetActive(true);
            infoPanel.transform.SetParent(parentInfoPanel.transform, true);
            infoPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    private void SetStarImage(Sprite img)
    {
        for(int i = 0; i < star; i++)
        {
            stars[i].sprite = img;
        }
    }

    public void Close()
    {
        MapManager.Instance.audio.Play();
        if (infoPanel.activeInHierarchy)
        {
            infoPanel.SetActive(false);
            infoPanel.transform.SetParent(firstParent, true);
        }
    }

    public int GetLevelToUnlock()
    {
        return levelToUnlock;
    }

    public void PreviewSong(AudioSource song)
    {
        if (!song.isPlaying)
        {
            song.Play();
        }
        else
        {
            song.Stop();
        }
    }
}
