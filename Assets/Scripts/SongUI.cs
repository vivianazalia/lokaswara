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
    [SerializeField] private int levelToUnlocked;
    private Image[] stars = new Image[3];

    private void Start()
    {
        firstParent = infoPanel.transform.parent;
        highscore = PlayerPrefs.GetInt(locationHighscore);
        star = PlayerPrefs.GetInt(locationStar);

        highscoreText.text = highscore.ToString();

        for(int i = 0; i < stars.Length; i++)
        {
            stars[i] = starContainer.transform.Find("Star" + i.ToString()).GetComponent<Image>();
        }

        SetStarImage(starImage);
    }

    public void PlayGame()
    {
        totalHeart = PlayerPrefs.GetInt("Heart");
        if (totalHeart > 0)
        {
            totalHeart--;
            PlayerPrefs.SetString("LastTime", System.DateTime.Now.ToString());
            MapManager.Instance.OnSceneChange();
            PlayerPrefs.SetInt("Heart", totalHeart);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void ShowInfo()
    {
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
        if (infoPanel.activeInHierarchy)
        {
            infoPanel.SetActive(false);
            infoPanel.transform.SetParent(firstParent, true);
        }
    }

    public int GetLevelToUnlock()
    {
        return levelToUnlocked;
    }
}
