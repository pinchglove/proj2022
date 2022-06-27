using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking
{
    public string userID { get; set; }
    public float data { get; set; }

    public Ranking(string id, float f)
    {
        userID = id;
        data = f;
    }
}


public class table_Result : MonoBehaviour
{
    List<Ranking> rank = new List<Ranking>();
    public List<GameObject> panels = new List<GameObject>();
    public List<float> data = new List<float>();
    public List<string> querys = new List<string>() { "maxPower","risingTime","releaseTime","tapcount","interval","boxCount","rmse","accuracy" };
    List<float> datasToProcess;

    void Start()
    {
        datasToProcess = new List<float>() { 0.01f, 100f, 100f, 1f, 100f, 1f, 100f, 1f }; // 0:1000g(힘), 1:0.45초(상승시간), 2:0.45초(하강시간) , 3:2hz(빈도), 4:0.45초(간격), 5:80%(정확도), 6:rmse(rmse), 7: 40개(상자)
        for (int i = 0; i < querys.Count(); i++)
        { 
            panels[i].transform.Find("data").GetComponent<Text>().text = GetPersonalData(querys[i]).ToString("F2");
            panels[i].transform.Find("persent").GetComponent<Text>().text = GetRank(querys[i]).ToString("F2");
                //   Debug.Log(GetPersonalData(querys[i]));
        }        
    }

    public float GetRank(string data)        // 개인 순위 / 전체 이용자 수 * 100
    {
        rank.Clear();
        string query = "";
        float result;
        switch (data)   //값이 높으면 좋은지 낮은게 좋은지에 따라서 오름차순 내림차순 차이 
        {
            case "maxPower":
            case "tapcount":
            case "accuracy":
            case "boxCount":
                query = "SELECT userID," + data + "  FROM measurement WHERE " + data + " IS NOT NULL GROUP BY userID ORDER BY " + data + " DESC";
                break;
            case "risingTime":
            case "releaseTime":
            case "interval":
            case "rmse":
                query = "SELECT userID," + data + "  FROM measurement WHERE " + data + " IS NOT NULL GROUP BY userID ORDER BY " + data + " ASC";
                break;
        }

        DB.DataBaseRead(query);
        while (DB.dataReader.Read())
        {
            rank.Add(new Ranking(DB.dataReader.GetString(0), DB.dataReader.GetFloat(1)));
        }
        var userRank = rank.IndexOf(rank.Find(x=>x.userID.Contains(Data.instance.userID))) + 1;
        Debug.Log(userRank);
        result = ((float)userRank / (float)rank.Count) * 100f;  // 개인 순위 / 전체 이용자 수 * 100

        return result;
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

        switch (data)
        {
            case "maxPower":
                result = DB.dataReader.GetFloat(0) * datasToProcess[0];
                break;
            case "risingTime":
                result = DB.dataReader.GetFloat(0) * datasToProcess[1];
                break;
            case "releaseTime":
                result = DB.dataReader.GetFloat(0) * datasToProcess[2];
                break;
            case "frequency":
                result = DB.dataReader.GetFloat(0) * datasToProcess[3];
                break;
            case "interval":
                result = DB.dataReader.GetFloat(0) * datasToProcess[4];
                break;
            case "accuracy":
                result = DB.dataReader.GetFloat(0) * datasToProcess[5];
                break;
            case "rmse":
                result = DB.dataReader.GetFloat(0) * datasToProcess[6];
                break;
            case "boxCount":
                result = DB.dataReader.GetFloat(0) * datasToProcess[7];
                break;
            default:
                break;
        }

                return result;
    }

   /* public float NormalizeData(string column, float data)
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
            case "tapcount":
                result = data/100f;
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
    }*/
}
