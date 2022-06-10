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
        scoreText.text = "Score: " + GameManager.score.ToString();
        pauseWeightText.text = "Weight: " + GameManager.force.ToString();
        pauseITimeText.text = "Initial Time: " + GameManager.iTime.ToString("N2");
        //pauseRTimeText.text = "Remaining Time: " + GameManager.rTime.ToString("N2");
        pauseScoreText.text = scoreText.text;
        resultWeightText.text = pauseWeightText.text;
        resultITimeText.text = pauseITimeText.text;
        resultScoreText.text = pauseScoreText.text;
    }
    public void ShowMenu()
    {
        GameManager.paused = true;
        if(menuPanel.activeSelf == false)
            menuPanel.SetActive(true);
        if (pausePanel.activeSelf == true)
            pausePanel.SetActive(false);
        if (resultPanel.activeSelf == true)
            resultPanel.SetActive(false);
            //GameManager.paused = true;
    }
    public void GameStart()
    {
        
        GameManager.force = (int)ShowSliderValueToText.WeightSliderValue;
        if (GameManager.iTime == 0 || (GameManager.iTime != ShowSliderValueToText.timeSliderValue))
        {
            GameManager.iTime = ShowSliderValueToText.timeSliderValue;
            GameManager.rTime = GameManager.iTime;
        }
        else if(GameManager.iTime != 0 && (GameManager.iTime != GameManager.rTime))
        {

        }
        
        Time.timeScale = 1f;
        GameManager.paused = false;
        menuPanel.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0.0f;
        //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.DecimalPad);
        if(pausePanel.activeSelf ==false)
        { 
            pausePanel.SetActive(true);
            GameManager.paused = true;
        }
    }
    public void Resume()
    {

        //Serial.SerialSendingStart();
        //BNG.Grabber.gotForce = (int)ShowSliderValueToText.WeightSliderValue;
        //overlayKeyboard = null;
        Time.timeScale = 1f;
        GameManager.paused = false;
        pausePanel.SetActive(false);
    }
    public void Restart()
    {
        GameManager.rTime = 0;
        GameManager.iTime = 0;
        GameManager.score = 0;
        SceneManager.LoadScene("ArrowVR_Scene");
        //Application.LoadLevel(Application.loadedLevel);
    }
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        GameManager.paused = true;
        resultPanel.SetActive(true);
    }
}
