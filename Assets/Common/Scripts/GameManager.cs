﻿using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int timeLimit;
    public TextMeshProUGUI timer;
    public static bool gameIsPaused = false;
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {

        while (timeLimit > 0)
        {
            string minutes = Mathf.Floor(timeLimit / 60).ToString("00");
            string seconds = (timeLimit % 60).ToString("00");

            timer.text = minutes + ":" + seconds + " Until door's closed.";
            timeLimit--;
            yield return new WaitForSeconds(1f);
        }
        //activate the lose menu
    }

    public void PauseGame()
    {
        if (!gameIsPaused)
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            gameIsPaused = false;
        }
    }
}
