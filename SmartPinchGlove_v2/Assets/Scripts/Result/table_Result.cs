using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class table_Result : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();
    public List<float> data = new List<float>();
    public List<string> querys = new List<string>() { "maxPower","risingTime","releaseTime","frequency","interval","boxCount","rmse","accuracy" };
    void Start()
    {
        for(int i = 0; i < querys.Count(); i++)
        {
            if (querys[i] == "maxPower")
            {
                panels[i].transform.Find("data").GetComponent<Text>().text = (GetPersonalData(querys[i]) / 1000f).ToString("F2");
                panels[i].transform.Find("persent").GetComponent<Text>().text = NormalizeData(querys[i], GetPersonalData(querys[i])).ToString("F2");
            }
            else
            {
                panels[i].transform.Find("data").GetComponent<Text>().text = GetPersonalData(querys[i]).ToString("F2");
                panels[i].transform.Find("persent").GetComponent<Text>().text = NormalizeData(querys[i], GetPersonalData(querys[i])).ToString("F2");
             //   Debug.Log(GetPersonalData(querys[i]));
            }
        }        
    }
    public void GetUserInfo()
    {

    }
    public float GetTotalAverageData(string data)   //평균 데이터 가져오기
    {
        int count = 0;
        List<float> tmp = new List<float>();
        string query = "SELECT " + data + " FROM measurement WHERE " + data + " IS NOT NULL ORDER BY date DESC";
        DB.DataBaseRead(query);
        while (DB.dataReader.Read() && count <= 1000)
        {
            count++;
            tmp.Add(DB.dataReader.GetFloat(0));
        }
        //Debug.Log("Average : "+tmp.Average());
        return tmp.Average();
    }

    public float GetPersonalData(string data)  //개인 최근 데이터 가져오기
    {
        float result = 0f;
        string query = "SELECT " + data + " FROM measurement WHERE " + data + " IS NOT NULL AND userID='" + Data.instance.userID + "' ORDER BY date DESC";
        DB.DataBaseRead(query);
        DB.dataReader.Read();
        result = DB.dataReader.GetFloat(0);
        return result;
    }

    public float NormalizeData(string column, float data)
    {
        float result = 0f;
        switch (column)
        {
            case "maxPower":
                result = data / 2000f * 100f;
                break;
            case "risingTime":
                result = data / 200f * 100f;
                break;
            case "releaseTime":
                result = data / 200f * 100f;
                break;
            case "frequency":
                result = data / 4f * 100f;
                break;
            case "interval":
                result = data / 4f * 100f;
                break;
            case "accuracy":
                result = data / 100f * 100f;
                break;
            case "rmse":
                result = data / 4f * 100f;
                break;
            case "boxCount":
                result = data / 80f * 100f;
                break;
            default:
                break;
        }
        return result;
    }
}
