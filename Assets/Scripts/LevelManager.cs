using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public SongUI[] levelButtons;
    [SerializeField] private Island[] islandLevelToUnlock;
    [SerializeField] private Sprite lockIsland;

    //[SerializeField] private AudioSource bgm;
    //[SerializeField] private AudioSource buttonClick;

    private void Start()
    {
        islandLevelToUnlock = FindObjectsOfType<Island>();
    }

    private void Update()
    {
        ConfigureButtonLevel();
    }

    private void ConfigureButtonLevel()
    {
        int levelReached = PlayerPrefs.GetInt("Level");

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i].GetLevelToUnlock() > levelReached)
            {
                levelButtons[i].lockPanel.SetActive(true);
            }
        }

        for (int i = 0; i < islandLevelToUnlock.Length; i++)
        {
            if (islandLevelToUnlock[i].GetLevelToUnlock() > levelReached)
            {
                islandLevelToUnlock[i].GetComponent<SpriteRenderer>().sprite = lockIsland;
                islandLevelToUnlock[i].IsUnlock(false);
            }
        }
    }
}
