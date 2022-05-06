using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public int maxPower_average;    // 최대 힘 변수
    public float tappingHZ;     // 태핑 빈도수 변수
    
    

    public string userID;
    public string userName;
    public string date;


    public static Data instance = null;

    //singletone
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
