using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public static float rmse_value;
    public static List<float> doy = new List<float>();
    public static float value;
    public static int number;
    public static float average;
    public static float last;
    public static float globalTimer;
    //public static float nextPos;
    public float r;
    public float v;
    public int n;
    public float a;
    public float l;
    public float gT;
   // public float nP;
    void Start()
    {
        globalTimer = 80;  
    }

    // Update is called once per frame
    void Update()
    {
        globalTimer -= Time.deltaTime;
        r = rmse_value;
        v = value;
        n = number;
        a = average;
        l = last;
        gT = globalTimer;
        if(globalTimer < 0)
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
