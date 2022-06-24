using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Manager_Tracking : MonoBehaviour
{
    // Start is called before the first frame update
    public float rmse_value;
    public static List<float> doy = new List<float>();
    public static float value;
    public static int number;
    public static float average;
    public static float rmse;
    public static float globalTimer;
    //public static float nextPos;
    public float r;
    public float v;
    public int n;
    public float a;
    //public float l;
    public float gT;
    // public float nP;
    public static bool paused_Tracking;
    void Start()
    {
        Time.timeScale = 1f;
        globalTimer = 97;
        rmse_value = 0;
        doy.Clear();
        number = 0;
        average = 0;
        paused_Tracking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        globalTimer -= Time.deltaTime;
        
        v = value; //for debugging
        n = number; //for debugging
        a = average; //for debugging
        rmse_value = rmse; // for debugging, l = last;
        gT = globalTimer; //for debugging
        if (globalTimer < 0 && number > 0)
        //if (globalTimer < 0)
        {
            //Time.timeScale = 0f;
            GameSceneUI.instance.GameOver();
        }
        if (paused_Tracking == false)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
    /*
    public void RMSE_Calc()
    {
        first = transform.position.y;
        value = Mathf.Pow((PlayerBehaviour.yCoord - first), 2);
        number++; //n
        doy.Add(value); //until sigma
        average = doy.Average();
        last = Mathf.Sqrt(average);
    }
    */
}
