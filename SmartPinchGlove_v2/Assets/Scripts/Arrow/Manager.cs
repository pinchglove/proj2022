using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WindowsInput;

public class Manager : MonoBehaviour
{
    public static int score;
    public int sc;
    public TextMesh scoreText;
    public static int force; //weight
    public int difficulty;
    public TextMesh TimeText;
    public static float rTime; //remaining time
    public static float iTime; //init time
    public static bool paused;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        sc = 0;
        //rTime = 300;
        //difficulty = 1000;
        //force = difficulty;
        iTime = 0; 
        //Time.timeScale = 1f;
    }



    // Update is called once per frame
    void Update()
    {
        difficulty = force;
        if (Inputdata.index_F > Manager.force)
        {
            //KeyPress_Sim();
        }
        if(rTime > 0 && paused == false)
        {
            rTime -= Time.deltaTime;
        }
        else if(rTime < 0 && paused == false) { 
            GameMenu._Instance.GameOver();
        }
    }

    private void OnGUI()
    {
        if (!scoreText)
        {
            try
            {
                scoreText = GameObject.Find("ScoreText").GetComponent<TextMesh>();
            }
            catch (System.Exception ex)
            {
                Debug.Log("Couldn't find ScoreText ->" + ex.StackTrace.ToString());
            }
        }
        

        if (!TimeText)
        {
            try
            {
                TimeText = GameObject.Find("TimeText").GetComponent<TextMesh>();
            }
            catch (System.Exception ex)
            {
                Debug.Log("Couldn't find TimeText ->" + ex.StackTrace.ToString());
            }
        }

        scoreText.text = "Score: " + score.ToString();
        TimeText.text = "Time: " + rTime.ToString("N2") + "s";
    }
    /*
    private void KeyPress_Sim()
    {
        InputSimulator inputSimulator = new InputSimulator();
        //inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_X);
        Debug.Log("Key Pressed");
        //inputSimulator.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_Z);
    }
    */
}

