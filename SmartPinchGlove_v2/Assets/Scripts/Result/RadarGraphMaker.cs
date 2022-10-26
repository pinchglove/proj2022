using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using System.Linq;

public class RadarGraphMaker : MonoBehaviour
{
    public RadarChart radarChart;
    public List<string> columnList;
    public Dictionary<string, float> personalDatas = new Dictionary<string, float>();
    public Dictionary<string, float> overallDatas = new Dictionary<string, float>();
    // Start is called before the first frame update
    void Start()
    {
        columnList = new List<string>() { "maxPower", "risingTime", "frequency", "interval", "rmse", "accuracy" };
        foreach(string item in columnList)
        {
            personalDatas.Add(item, NomalizeData(item, GetPersonalData(item)));
            overallDatas.Add(item, NomalizeData(item, GetTotalAverageData(item)));
        }

        radarChart.DataSource.SetValue("User", "Strength", personalDatas["maxPower"]);
        radarChart.DataSource.SetValue("User", "RisingTime", personalDatas["risingTime"]);
        radarChart.DataSource.SetValue("User", "Frequency", personalDatas["frequency"]);
        radarChart.DataSource.SetValue("User", "Interval", personalDatas["interval"]);
        radarChart.DataSource.SetValue("User", "RMSE", personalDatas["rmse"]);
        radarChart.DataSource.SetValue("User", "Accuracy", personalDatas["accuracy"]);

        radarChart.DataSource.SetValue("OverallAverage", "Strength", overallDatas["maxPower"]);
        radarChart.DataSource.SetValue("OverallAverage", "RisingTime", overallDatas["risingTime"]);
        radarChart.DataSource.SetValue("OverallAverage", "Frequency", overallDatas["frequency"]);
        radarChart.DataSource.SetValue("OverallAverage", "Interval", overallDatas["interval"]);
        radarChart.DataSource.SetValue("OverallAverage", "RMSE", overallDatas["rmse"]);
        radarChart.DataSource.SetValue("OverallAverage", "Accuracy", overallDatas["accuracy"]);
    }

    float NomalizeData(string column, float data)
    {
        float min = 0f;
        //float max = 100f;
        float result = 0f;

        switch (column)
        {
            case "maxPower":    // 6000g을 최대 값으로 
                result = data / 6000f * 100f;
                return result;
            case "risingTime":  // 2초를 최대로 하고 최대 값으로 빼서 값 뒤집음
                result = (1 - data / 2f) * 100f;
                if (result <= 0) 
                { 
                    return 0f; 
                }
                else
                {
                    return result;
                }
            case "frequency":   //최대 4hz , 
                result = data * 25f;
                return result;
            case "interval":    //1초를 최대로 하고 최대 값으로 빼서 값 뒤집음
                result = (1 - data) * 100f;
                if (result <= 0)
                {
                    return 0f;
                }
                else
                {
                    return result;
                }

            case "rmse":    //최대가 100이고 낮을수록 좋기 때문에 뒤집어 줌
                result = 100 - data;
                return result;

            case "accuracy":
                result = data;
                return result;

            default:
                return 0f;
        }

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
}
