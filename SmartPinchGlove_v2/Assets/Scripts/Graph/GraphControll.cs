using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using System.Linq;
using UnityEngine.UI;
using System;


//데이터 저장할 클래스 
public class MaxPowerData
{
    public float time { get; set; }
    public float data { get; set; }

    public MaxPowerData(float t, float d)
    {
        time = t;
        data = d;
    }
}
public class TrackingData
{
    public float time { get; set; }
    public float data { get; set; }
    public float guide { get; set; }

    public TrackingData(float t, float d, float g)
    {
        time = t;
        data = d;
        guide = g;
    }
}


public class GraphControll : MonoBehaviour
{
    GameObject inputField;
    GameObject legend;
    Text title;
    Text result;
    public GraphChart chart;
    public HorizontalAxis horizontalAxis;
    public VerticalAxis verticalAxis;

    public GameObject canvas;
    public GameObject startPanel;
    public int power = 0;
    public float halfPower = 0f;
    float timer;
    float startOffset = 2f;
    string measurementIndex = "";
    List<MaxPowerData> maxPowerDatas = new List<MaxPowerData>();
    List<TrackingData> trackingDatas = new List<TrackingData>();
    public AudioSource beep;
    public AudioClip beepClip;
    float rmsetmp = 0f;
    
    void Start()
    {
        power = 0;
        halfPower = 0f;
        startOffset = 2f;
        canvas = GameObject.Find("Canvas").gameObject;
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        canvas.transform.GetChild(2).gameObject.SetActive(false);
        legend = canvas.transform.GetChild(1).GetChild(5).gameObject;
        title = canvas.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        result = canvas.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        title.text = "Measurement";
        inputField = canvas.transform.GetChild(1).GetChild(4).GetChild(0).gameObject;
        startPanel = canvas.transform.GetChild(5).gameObject;
    }

    //그래프 설정
    public void SetGraph()
    {
        if (measurementIndex == "maxPower")
        {
            //메인패널 끄고 그래프 패널 켜기
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            //레전드 세팅
            SetlLegend();
            title.text = "Max. Strength";
            inputField.SetActive(false);
            chart.DataSource.ClearCategory("GuideLine");
            chart.DataSource.ClearCategory("Data");
            chart.DataSource.ClearCategory("RisingTIme1");
            chart.DataSource.ClearCategory("RisingTIme2");
            timer = 0f;
            maxPowerDatas.Clear();
            chart.DataSource.VerticalViewSize = 10;  // 단위조정
            chart.DataSource.HorizontalViewSize = 9;
            // x,y축 설정
            verticalAxis.MainDivisions.Total = 5;
            verticalAxis.SubDivisions.Total = 2;
            horizontalAxis.MainDivisions.Total = 9;
            horizontalAxis.SubDivisions.Total = 0;
            //StartCoroutine(MeasureMaxPower());
            //StartCoroutine(ControllBeep());
        }

        else if(measurementIndex == "DCTracking")
        {
            //메인패널 끄고 그래프 패널 켜기
            canvas.transform.GetChild(0).gameObject.SetActive(false);   
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            //레전드 필요없는거 끄기
            SetlLegend();
            title.text = "Static endurance";
            inputField.SetActive(false);
            chart.DataSource.StartBatch();
            chart.DataSource.ClearCategory("GuideLine");
            chart.DataSource.ClearCategory("Data");
            timer = 0f;
            rmsetmp = 0f;
            trackingDatas.Clear();
            chart.DataSource.VerticalViewSize = power + 2; //halfPower;
            chart.DataSource.HorizontalViewSize = 20 + startOffset;
            //x,y축 설정
            verticalAxis.MainDivisions.Total = power + 2;
            verticalAxis.SubDivisions.Total = 0;
            horizontalAxis.MainDivisions.Total = 20 + (int)startOffset;
            horizontalAxis.SubDivisions.Total = 0;
            //가이드라인 그리기
            chart.DataSource.StartBatch();
            chart.DataSource.AddPointToCategory("GuideLine", 2, power * 0.5f);
            chart.DataSource.AddPointToCategory("GuideLine", 20 + startOffset, power * 0.5f);
            chart.DataSource.EndBatch();

            //StartCoroutine(MeasureDCTracking());
        }

        else if (measurementIndex == "SINTracking")
        {
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            //레전드 세팅
            SetlLegend();
            title.text = "Force control";
            inputField.SetActive(false);
            chart.DataSource.ClearCategory("GuideLine");
            chart.DataSource.ClearCategory("Data");
            chart.DataSource.ClearCategory("RisingTIme1");
            chart.DataSource.ClearCategory("RisingTIme2");
            timer = 0f;
            rmsetmp = 0f;
            trackingDatas.Clear();
            chart.DataSource.VerticalViewSize = power; //halfPower;
            chart.DataSource.HorizontalViewSize = 20 + startOffset;
            //x,y축 설정
            verticalAxis.MainDivisions.Total = power;
            verticalAxis.SubDivisions.Total = 0;
            horizontalAxis.MainDivisions.Total = 20 + (int)startOffset;
            horizontalAxis.SubDivisions.Total = 0;

            chart.DataSource.StartBatch();
            for (float i = startOffset; i < 20 + startOffset; i += 0.1f)
            {
                chart.DataSource.AddPointToCategory("GuideLine", i, Mathf.Sin((0.2f * (Mathf.PI)) * (i - startOffset) - (0.5f * (Mathf.PI))) * (power/4f) + (power / 4f));//chart.DataSource.AddPointToCategory("GuideLine",i,Mathf.Sin((0.2f * (Mathf.PI)) * i-(0.5f* (Mathf.PI))) * (halfPower / 4f) + (halfPower / 4f));
            }
            chart.DataSource.EndBatch();
            //StartCoroutine(MeasureSinTracking());
        }
    }

    public void setPower()
    {
        startPanel.SetActive(true);
        power = int.Parse(startPanel.transform.GetChild(0).GetComponent<InputField>().text);
    }

    public void StartMeasurement()
    {
        if (measurementIndex == "maxPower")
        {
            StartCoroutine(ControllBeep());
            StartCoroutine(MeasureMaxPower());
        }

        else if (measurementIndex == "DCTracking")
        {
            //power = int.Parse(inputField.GetComponent<InputField>().text);
            //halfPower = power / 2f;
            StartCoroutine(MeasureDCTracking());
        }

        else if (measurementIndex == "SINTracking")
        {
            //power = int.Parse(inputField.GetComponent<InputField>().text);
            //halfPower = power / 2f;
            StartCoroutine(MeasureSinTracking());
        }
    }
    //레전드 세팅
    void SetlLegend()
    {
        if (measurementIndex == "maxPower")
        {
            legend.transform.GetChild(0).gameObject.SetActive(false);
            legend.transform.GetChild(1).gameObject.SetActive(true);
            legend.transform.GetChild(2).gameObject.SetActive(true);
            legend.transform.GetChild(3).gameObject.SetActive(true);
        }

        else if (measurementIndex == "DCTracking")
        {
            legend.transform.GetChild(0).gameObject.SetActive(true);
            legend.transform.GetChild(1).gameObject.SetActive(true);
            legend.transform.GetChild(2).gameObject.SetActive(false);
            legend.transform.GetChild(3).gameObject.SetActive(false);
        }

        else if (measurementIndex == "SINTracking")
        {
            legend.transform.GetChild(0).gameObject.SetActive(true);
            legend.transform.GetChild(1).gameObject.SetActive(true);
            legend.transform.GetChild(2).gameObject.SetActive(false);
            legend.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    //Beep사운드 https://blog.naver.com/PostView.nhn?blogId=ckdduq2507&logNo=222113891105&parentCategoryNo=&categoryNo=74&viewDate=&isShowPopularPosts=true&from=search
    IEnumerator ControllBeep()
    {
        yield return new WaitForSeconds(2.7f);
        beep.PlayOneShot(beepClip);
        yield return new WaitForSeconds(3f);
        beep.PlayOneShot(beepClip);
        yield return null;
    }

    //최대힘 측정 함수
    IEnumerator MeasureMaxPower()
    {   
        //UserData 그래프 
        while(timer <= 9)
        {
            maxPowerDatas.Add(new MaxPowerData(timer, SelectFinger.GetInputData()));
            chart.DataSource.AddPointToCategoryRealtime("Data", timer, SelectFinger.GetInputData() / 1000f);    // 단위조정
            timer += Time.deltaTime;
            yield return null;
        }
        //Debug.Log( chart.DataSource.GetMaxValue(1,true));    
        //risingTime 그려주기
        var risingStart = maxPowerDatas.Max(x => x.data)* 0.1f;
        var risingEnd = maxPowerDatas.Max(x => x.data) * 0.9f;
        chart.DataSource.StartBatch();
        float i = 0;
        while(i < 10)
        {
            chart.DataSource.AddPointToCategory("RisingTime1", i, risingStart / 1000f);     // 단위조정
            chart.DataSource.AddPointToCategory("RisingTime2", i, risingEnd / 1000f);
            i += 0.18f;
        }

        //chart.DataSource.AddPointToCategory("RisingTime1", 0, risingStart / 1000f);     // 단위조정
        //chart.DataSource.AddPointToCategory("RisingTime1", maxPowerDatas.Where(x => x.data >= risingStart).Min(x => x.time), risingStart / 1000f);     
        //chart.DataSource.AddPointToCategory("RisingTime1", 9, risingStart / 1000f);
        //chart.DataSource.AddPointToCategory("RisingTime2", 0, risingEnd / 1000f);
        //chart.DataSource.AddPointToCategory("RisingTime2", maxPowerDatas.Where(x => x.data >= risingEnd).Min(x => x.time), risingEnd / 1000f);
        //chart.DataSource.AddPointToCategory("RisingTime2", 9, risingEnd / 1000f);
        var risingTIme = maxPowerDatas.Where(x => x.data >= risingEnd).Min(x => x.time) - maxPowerDatas.Where(x => x.data >= risingStart).Min(x => x.time);
        Debug.Log("RT = " + risingTIme);
        chart.DataSource.EndBatch();
        
        //releaseTime 계산
        var tmpList = maxPowerDatas.Where(x => x.time >= 6).ToList();
        var releaseStart = tmpList.Max(x => x.data) * 0.9f;
        var releaseEnd = tmpList.Max(x => x.data) * 0.1f;
        var releaseTime = tmpList.Where(x => x.data <= releaseEnd).Min(x => x.time) - tmpList.Where(x => x.data >= releaseStart).Min(x => x.time);
        Debug.Log("Release = " + releaseTime);

        //DB
        Data.instance.maxPower_average = maxPowerDatas.Max(x => x.data);
        Data.instance.risingTime = risingTIme;
        Data.instance.releaseTime = releaseTime;
        string query = "INSERT INTO measurement (date,userID,gameID,maxPower,risingTime,releaseTime) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "01M" + "','" + Data.instance.maxPower_average + "','" + Data.instance.risingTime + "','" + Data.instance.releaseTime + "')";
        DB.DatabaseInsert(query);

        canvas.transform.GetChild(2).gameObject.SetActive(true);
        result.text = "MaxPower : " + (maxPowerDatas.Max(x => x.data) * 0.001f).ToString("F2") + "kg\n" + "Rising Time : " + risingTIme.ToString("F2") ;
    }

    // DC 트래킹 측정 (static endurance)
    IEnumerator MeasureDCTracking()
    {
        //User Data 그래프
        while (timer >= 0 && timer < startOffset)
        {
            var fingertmp = SelectFinger.GetInputData() * 0.001f;
            chart.DataSource.AddPointToCategoryRealtime("Data", timer, fingertmp);
            timer += Time.deltaTime;
            Debug.Log("timer : " + timer);
            yield return null;
        }
        while (timer >= startOffset && timer <= 20 + startOffset)
        {
            var DCtmp = power * 0.5f;
            var fingertmp = SelectFinger.GetInputData() * 0.001f;
            var rmsePow = Mathf.Pow(DCtmp - fingertmp, 2f);

            trackingDatas.Add(new TrackingData(timer, fingertmp, DCtmp));
            chart.DataSource.AddPointToCategoryRealtime("Data", timer, fingertmp);
            Debug.Log("DC : " + DCtmp + ", finger : " + fingertmp + " RMSE : " + rmsePow);

            rmsetmp += rmsePow;
            timer += Time.deltaTime;
            yield return null;
        }

        canvas.transform.GetChild(2).gameObject.SetActive(true);    // 결과 패널
        var RMSE = Mathf.Sqrt(rmsetmp / trackingDatas.Count()) * 2f;
        result.text = "RMSE \n" + RMSE; //Mathf.Sqrt(rmsetmp / trackingDatas.Count());  
        Debug.Log("평균 : " + RMSE); //Mathf.Sqrt(rmsetmp / trackingDatas.Count()));

  /*      Data.instance.rmseValue = RMSE;
        string query = "INSERT INTO measurement (date,userID,gameID,rmse) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "04M" + "','" + Data.instance.rmseValue + "')";
        DB.DatabaseInsert(query);*/
    }

    //사인파 트래킹 측정 
    IEnumerator MeasureSinTracking()
    {
        while (timer >= 0 && timer < startOffset)
        {
            var fingertmp = SelectFinger.GetInputData() * 0.001f;
            chart.DataSource.AddPointToCategoryRealtime("Data", timer, fingertmp);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer >= startOffset && timer <= 20 + startOffset)
        {
            var sintmp = Mathf.Sin((0.2f * (Mathf.PI)) * (timer - startOffset) - (0.5f * (Mathf.PI))) * (power / 4f) + (power / 4f);//var sintmp = Mathf.Sin((0.2f * (Mathf.PI)) * timer - (0.5f * (Mathf.PI))) * (halfPower / 4f) + (halfPower / 4f);
            var fingertmp = SelectFinger.GetInputData() * 0.001f;
            var rmsePow = Mathf.Pow(sintmp - fingertmp, 2f);
            trackingDatas.Add(new TrackingData(timer, fingertmp, sintmp));
            chart.DataSource.AddPointToCategoryRealtime("Data", timer, fingertmp);
            Debug.Log("sin : " + sintmp + ", finger : " + fingertmp + " RMSE : " + rmsePow);
            rmsetmp += rmsePow;
            timer += Time.deltaTime;
            yield return null;
        }
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        var RMSE = Mathf.Sqrt(rmsetmp / trackingDatas.Count()) * 2f;
        result.text = "RMSE\n" + RMSE;
        Debug.Log( "평균 : " + RMSE);

        Data.instance.rmseValue = RMSE;
        string query = "INSERT INTO measurement (date,userID,gameID,rmse) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "04M" + "','" + Data.instance.rmseValue + "')";
        DB.DatabaseInsert(query);
    }

    //버튼으로 모드 변경
    public void SetMeasurementIndex(string s)
    {
        measurementIndex = s;
        if (s == "maxPower")
        {
            SetGraph();
        }
    }
    
    
}
