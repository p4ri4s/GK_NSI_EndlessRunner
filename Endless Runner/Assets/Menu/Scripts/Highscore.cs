using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public GameObject highScoreTable;
    public Text highScore;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void openWindow()
    {
        highScoreTable.SetActive(true);
    }

    public void closeWindow()
    {
        highScoreTable.SetActive(false);
    }
}
