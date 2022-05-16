using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject pausePanel;

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
        if (!pausePanel.activeSelf)
        {
            Pause();
            //weightText.ActivateInputField();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Pause()
    {
        //Time.timeScale = 0.0f;
        //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.DecimalPad);
        if(pausePanel.activeSelf ==false)
        { 
            pausePanel.SetActive(true);
            GameManager.paused = true;
        }
    }
    public void Resume()
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
        SceneManager.LoadScene("stvr_proto");
        //Application.LoadLevel(Application.loadedLevel);
    }
}
