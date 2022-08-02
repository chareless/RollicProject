﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool restart;
    public static bool overLevel;
    public static bool gameStarted;
    public static int winOrLose = 0;
    public static int level = 1;
    public int chosenLevel;
    public Text levelText;
    public GameObject tapText;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject[] levels;
    public int overLevels;
    
    void Start()
    {
        if (PlayerPrefs.GetInt("Level") > 1)
        {
            level = PlayerPrefs.GetInt("Level");
        }
        if (chosenLevel > 0 && !overLevel)
        {
            level = chosenLevel;
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.Save();
        }
        LevelController();
    }

    void TouchController()
    {
        if (!restart)
        {
            if (Input.touchCount > 0 && !gameStarted)
            {
                gameStarted = true;
            }

            if (Input.GetMouseButtonDown(0) && !gameStarted)
            {
                gameStarted = true;
            }
        }

        if(gameStarted)
        {
            tapText.SetActive(false);
        }
    }

    void PanelControl()
    {
        if(winOrLose == -1)
        {
            winPanel.SetActive(false);
            losePanel.SetActive(true);
        }
        else if(winOrLose == 1)
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
        }
        else
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }
    }

    void LevelController()
    {
        for(int i=0;i<levels.Length;i++)
        {
            levels[i].SetActive(false);
        }

        if (level <= levels.Length)
        {
            levels[level - 1].SetActive(true);
        }
        else
        {
            overLevels = level;
            if (PlayerPrefs.GetString(overLevels.ToString()) != "")
            {
                int newLevel = System.Convert.ToInt32(PlayerPrefs.GetString(overLevels.ToString()));
                levels[newLevel].SetActive(true);
            }
            else
            {
                int random = Random.Range(0, levels.Length);
                PlayerPrefs.SetString(overLevels.ToString(), random.ToString());
                levels[random].SetActive(true);
            }
           
        }
    }
    public void NextLevelButton()
    {
        level++;
        winOrLose = 0;
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
        if (level > levels.Length)
        {
            overLevel = true;
            SceneManager.LoadScene("SampleScene");
        }
        LevelController();
    }

    public void TryButton()
    {
        restart = true;
        LevelManager.miniLevel = 0;
        winOrLose = 0;
        SceneManager.LoadScene("SampleScene");
    }

    void ShowLevel()
    {
        levelText.text = "LEVEL " + level;
    }

    void Update()
    {
        TouchController();
        ShowLevel();
        PanelControl();
    }
}
