using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject menuPanel;
    public GameObject resultPanel;
    public Text scoreText;
    public Text pauseWeightText;
    public Text pauseRTimeText;
    public Text pauseITimeText;
    public Text pauseScoreText;
    public Text resultWeightText;
    //public Text resultRTimeText;
    public Text resultITimeText;
    public Text resultScoreText;
    #region Singleton
    private static GameMenu Instance;
    public static GameMenu _Instance
    {
        get { return Instance; }
    }
    #endregion

    public void Awake()
    {
        /*
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Instance = GetComponent<GameMenu>();
        */
        Instance = GetComponent<GameMenu>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GrabberControll.isPinch = true;
        //if (pausePanel.activeSelf)
            //pausePanel.SetActive(false);
        if (!menuPanel.activeSelf)
        {
            ShowMenu();
            //weightText.ActivateInputField();
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Manager.score.ToString();
        pauseWeightText.text = "Weight: " + Manager.force.ToString();
        pauseITimeText.text = "Initial Time: " + Manager.iTime.ToString("N2");
        //pauseRTimeText.text = "Remaining Time: " + Manager.rTime.ToString("N2");
        pauseScoreText.text = scoreText.text;
        resultWeightText.text = pauseWeightText.text;
        resultITimeText.text = pauseITimeText.text;
        resultScoreText.text = pauseScoreText.text;
    }
    public void ShowMenu()
    {
        Manager.paused = true;
        if(menuPanel.activeSelf == false)
            menuPanel.SetActive(true);
        if (pausePanel.activeSelf == true)
            pausePanel.SetActive(false);
        if (resultPanel.activeSelf == true)
            resultPanel.SetActive(false);
            //Manager.paused = true;
    }
    public void GameStart()
    {
        
        Manager.force = (int)ShowSliderValueToText.WeightSliderValue;
        if (Manager.iTime == 0 || (Manager.iTime != ShowSliderValueToText.timeSliderValue))
        {
            Manager.iTime = ShowSliderValueToText.timeSliderValue;
            Manager.rTime = Manager.iTime;
        }
        else if(Manager.iTime != 0 && (Manager.iTime != Manager.rTime))
        {

        }
        
        Time.timeScale = 1f;
        Manager.paused = false;
        menuPanel.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
        //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.DecimalPad);
        if(pausePanel.activeSelf ==false)
        { 
            pausePanel.SetActive(true);
            Manager.paused = true;
        }
    }
    public void Resume()
    {

        //Serial.SerialSendingStart();
        //BNG.Grabber.gotForce = (int)ShowSliderValueToText.WeightSliderValue;
        //overlayKeyboard = null;
        Time.timeScale = 1f;
        Manager.paused = false;
        pausePanel.SetActive(false);
    }
    public void Restart()
    {
        Manager.rTime = 0;
        Manager.iTime = 0;
        Manager.score = 0;
        SceneManager.LoadScene("Arrow");
        //Application.LoadLevel(Application.loadedLevel);
    }
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        Manager.paused = true;
        resultPanel.SetActive(true);
        GrabberControll.isPinch = false;
    }
}
