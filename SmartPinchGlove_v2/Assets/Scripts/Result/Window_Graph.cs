using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{

    [SerializeField] private Sprite circleSprite; //보라색 동그라미 이미지 인스펙터에서 설정
    [SerializeField] private Sprite circleSprite_red; // 빨간색 동그라미
    [SerializeField] private Sprite circleSprite_blue;  // 파란색 동그라미
    [SerializeField] private Sprite dotConnectionSprite; //보라색선 
    [SerializeField] private Sprite dotConnectionSprite_blue; //파란색선
    [SerializeField] private Sprite dotConnectionSprite_red; // 빨간색선
    //public GameObject point;

    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform scoreTemplate;
    private RectTransform scoreTemplate_red;
    private RectTransform scoreTemplate_blue;
    private RectTransform average_line;
    private RectTransform average_score;
    float yMaximum = 100f; //y축의 최대값 (단, 최저값은 0) 
    float y2Maximum = 200f; //y축의 최대값 (단, 최저값은 0) 
    public bool isSecond = false;
    public bool isThird = false;
    float separatorCount = 10;
    float graphHeight;
    float xSize = 130;            //x축의 간격
    List<float> datasToProcess; 
    //드롭다운으로 바뀌는 gameobject


    //public List<String> unit = {"kgf", "cs", "%", "Hz"};

    private void Awake()
    {
        datasToProcess = new List<float>() {0.001f, 100f, 100f, 1f, 100f, 1f, 100f, 1f}; // 0:1000g(힘), 1:0.45초(상승시간), 2:0.45초(하강시간) , 3:2hz(빈도), 4:0.45초(간격), 5:80%(정확도), 6:rmse(rmse), 7: 40개(상자)
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>(); //전체 틀
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>(); //x축
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>(); //y축
        scoreTemplate = graphContainer.Find("scoreTemplate").GetComponent<RectTransform>(); //데이터레이블 글자색 검정
        if (this.gameObject.name == "strength_graph")
        {
            scoreTemplate_red = graphContainer.Find("scoreTemplate_red").GetComponent<RectTransform>(); //데이터레이블 글자색 빨강
            scoreTemplate_blue = graphContainer.Find("scoreTemplate_blue").GetComponent<RectTransform>(); //데이터레이블 글자색 파랑
        }
        average_line = graphContainer.Find("average_line").GetComponent<RectTransform>(); //평균선
        average_score = graphContainer.Find("score").GetComponent<RectTransform>(); //데이터레이블
        graphHeight = graphContainer.sizeDelta.y; // 그래프 컨테이너 높이 설정
    }
    private void Start()
    {
        SortedDictionary<string, float> valueDic = new SortedDictionary<string, float>();//DB에서 데이터 불러오기
        SortedDictionary<string, float> valueDic2 = new SortedDictionary<string, float>();
        SortedDictionary<string, float> valueDic3 = new SortedDictionary<string, float>();
        //Dictionary<string, int> valueDic4 = new Dictionary<string, int>();

        switch (this.gameObject.name)
        {
            case "render_graph":

                Debug.Log("메인");
                break;
            case "strength_graph":
                yMaximum = 10f;
                //최대힘 데이터(DB)
                SetMeasurementData(valueDic, "maxPower");
                ProcessData(valueDic, "maxPower");
                ShowGraph(valueDic, GetTotalAverageData("maxPower"), (string _i) => (_i), (float _f) => "" + (_f));
                //ShowGraph 딕셔너리 Value가 float일 때는 Math.RoundToInt 빼기

                //라이징 타임 데이터(DB)
                SetMeasurementData(valueDic2, "risingTime");
                ProcessData(valueDic2, "risingTime");
                ShowGraph2(valueDic2, (string _i) => (_i), (float _f) => "" + (_f));//딕셔너리로 값 변경

                //릴리즈 타임 데이터(DB)
                SetMeasurementData(valueDic3, "releaseTime");
                ProcessData(valueDic3, "releaseTime");
                ShowGraph3(valueDic3, (string _i) => (_i), (float _f) => "" + (_f));//날짜와 수치로 변경 

                break;
            case "Intertap_graph":
                //탭사이간격데이터(DB)
                yMaximum = 100f;
                SetMeasurementData(valueDic, "interval");
                ProcessData(valueDic, "interval");
                ShowGraph(valueDic, GetTotalAverageData("interval"), (string _i) => (_i), (float _f) => "" + (_f));
                Debug.Log("탭간격");
                break;
            case "boxcount_graph":
                //박스개수 데이터(DB)
                yMaximum = 50f;
                SetMeasurementData(valueDic, "boxCount");
                ProcessData(valueDic, "boxCount");
                ShowGraph(valueDic, GetTotalAverageData("boxCount"), (string _i) => (_i), (float _f) => "" + (_f));
                Debug.Log("박스개수");

                break;
            case "tapFreq_graph":
                //태핑빈도 데이터(DB)
                yMaximum = 4f;
                SetMeasurementData(valueDic, "frequency");
                ProcessData(valueDic, "frequency");
                ShowGraph(valueDic, GetTotalAverageData("frequency"), (string _i) => (_i), (float _f) => "" + (_f));
                Debug.Log("탭빈도");
                break;
            case "trackingErr_graph":
                //트래킹오차율 데이터(DB)
                yMaximum = 100f;
                SetMeasurementData(valueDic, "rmse");
                ProcessData(valueDic, "rmse");
                ShowGraph(valueDic, GetTotalAverageData("rmse"), (string _i) => (_i), (float _f) => "" + (_f));
                Debug.Log("트래킹오차율");

                break;
            case "tapAccur_graph":
                //태핑정확도 데이터(DB)
                SetMeasurementData(valueDic, "accuracy");
                ProcessData(valueDic, "accuracy");
                ShowGraph(valueDic, GetTotalAverageData("accuracy"), (string _i) => (_i), (float _f) => "" + (_f));
                Debug.Log("태핑정확도");
                break;

        }
    }


    private void ShowGraph(SortedDictionary<string, float> valueDic, float average, Func<string, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null, Func<float, string> getAxisLabelY2 = null)
    {
        isSecond = false;
        isThird = false;
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (string _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return (_f).ToString(); };
        }


        //x축과 점들의 점수 부분
        GameObject lastCircleGameObject = null;
        int i = 0;
        float sum = 0;
        float avgPosition = 0;
        float xPosition = 0;

        foreach (var item in valueDic)
        {
            xPosition = xSize + i * xSize;
            float yPosition = 0f;

            yPosition = (item.Value / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            //x축 눈금선 찍기
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -18f);
            labelX.GetComponent<Text>().text = getAxisLabelX(item.Key);                 //getAxisLabelX의 괄호 안의 값을 변경 -> X축

            //데이터 레이블 글자 찍기
            RectTransform score = Instantiate(scoreTemplate);
            score.SetParent(graphContainer, false);
            score.gameObject.SetActive(true);
            score.anchoredPosition = new Vector2(xPosition, yPosition + 23f);
            score.GetComponent<Text>().text = getAxisLabelY(item.Value);  //getAxisLabelX의 괄호 안의 값을 변경 -> 점들의 스코어

            i++;
            sum += item.Value;
            avgPosition += yPosition;
        }


        //평균선 부분
        RectTransform average_Line = Instantiate(average_line);
        average_Line.SetParent(graphContainer, false);
        average_Line.gameObject.SetActive(true);
        average_Line.anchoredPosition = new Vector2(-12f, average / yMaximum* graphHeight);


        //평균점수 부분
        RectTransform average_Score = Instantiate(average_score);
        average_Score.SetParent(graphContainer, false);
        average_Score.gameObject.SetActive(true);
        average_Score.anchoredPosition = new Vector2(900f, average / yMaximum * graphHeight - 10f);        //평균선과 평균점수간에 위치가 잘 안맞아서 조정
        average_Score.GetComponent<Text>().text = getAxisLabelY(Mathf.Round(average*100)/100);

        //왼쪽 y축 눈금선

        if (yMaximum >= 100)
        {
            separatorCount = 10;
        }
        else if (yMaximum < 100 && yMaximum > 10)
        {
            separatorCount = 10;//yMaximum / 10;
        }
        else if (yMaximum <= 10)
        {
            separatorCount = 10;//yMaximum;
        }


        for (int j = 0; j <= separatorCount; j++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = j * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(38f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);
        }
    }

    //라이징타임 전용 그래프
    private void ShowGraph2(SortedDictionary<string, float> valueDic, Func<string, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null, Func<float, string> getAxisLabelY2 = null)
    {

        isSecond = true;
        isThird = false;
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (string _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        //x축과 점들의 점수 부분
        GameObject lastCircleGameObject = null;
        int i = 0;
        float sum = 0;
        float avgPosition = 0;
        float xPosition = 0;

        foreach (var item in valueDic)
        {

            xPosition = xSize + i * xSize;
            float yPosition = 0f;

            yPosition = (item.Value / y2Maximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            //데이터 레이블 찍기
            RectTransform score = Instantiate(scoreTemplate_red);
            score.SetParent(graphContainer, false);
            score.gameObject.SetActive(true);
            score.anchoredPosition = new Vector2(xPosition, yPosition - 15f);
            score.GetComponent<Text>().text = getAxisLabelY(item.Value);  //getAxisLabelX의 괄호 안의 값을 변경 -> 점들의 스코어

            i++;
            sum += item.Value;
            avgPosition += yPosition;
        }


        int separatorCount = 10;
        //y2축 레이블(0~200)
        if (this.gameObject.name == "strength_graph")
        {

            for (int j = 0; j <= separatorCount; j++)
            {
                RectTransform labelY = Instantiate(labelTemplateY);
                labelY.SetParent(graphContainer, false);
                labelY.gameObject.SetActive(true);
                float normalizedValue = j * 1f / separatorCount;
                labelY.anchoredPosition = new Vector2(1000f, normalizedValue * graphHeight);
                labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * y2Maximum);
            }
        }

    }

    //릴리즈타임 전용 그래프
    private void ShowGraph3(SortedDictionary<string, float> valueDic, Func<string, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null, Func<float, string> getAxisLabelY2 = null)
    {
        isThird = true;
        isSecond = true;
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (string _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        //x축과 점들의 점수 부분
        GameObject lastCircleGameObject = null;
        int i = 0;
        float sum = 0;
        float avgPosition = 0;
        float xPosition = 0;

        foreach (var item in valueDic)
        {
            xPosition = xSize + i * xSize;
            float yPosition = 0f;

            yPosition = (item.Value / y2Maximum) * graphHeight;

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform score = Instantiate(scoreTemplate_blue);
            score.SetParent(graphContainer, false);
            score.gameObject.SetActive(true);
            score.anchoredPosition = new Vector2(xPosition, yPosition - 15f);
            score.GetComponent<Text>().text = getAxisLabelY(item.Value);  //getAxisLabelX의 괄호 안의 값을 변경 -> 점들의 스코어

            i++;
            sum += item.Value;
            avgPosition += yPosition;
        }

    }

    //그래프 점대점 이어서 선그리기
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().color = new Color(1, 200, 200, 1f);        
        gameObject.GetComponent<Image>().sprite = dotConnectionSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 4f); //선너비
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI);

        if (isSecond) //라이징타임
        {
            gameObject.GetComponent<Image>().sprite = dotConnectionSprite_red;
            rectTransform.sizeDelta = new Vector2(distance, 1f); //선 너비
        }
        if (isThird) //릴리즈타임
        {
            gameObject.GetComponent<Image>().sprite = dotConnectionSprite_blue;
            rectTransform.sizeDelta = new Vector2(distance, 1f); //선 너비
        }
    }

    //그래프 점찍기
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        //GameObject gameObject = (GameObject)Instantiate(Resources.Load("Prefabs/circle"));
        //point.transform.SetParent(graphContainer, false);
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //RectTransform rectTransform = point.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(25, 25);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        if (isSecond)//라이징타임
        {
            gameObject.GetComponent<Image>().sprite = circleSprite_red;
            rectTransform.sizeDelta = new Vector2(10, 10);
        }
        if (isThird)//릴리즈타임
        {
            gameObject.GetComponent<Image>().sprite = circleSprite_blue;
            rectTransform.sizeDelta = new Vector2(10, 10);
        }
        return gameObject;
        //return point;
    }

    public void SetMeasurementData(SortedDictionary<string, float> dic, string data)
    {
        string query = "SELECT SUBSTRING(date,7,5),ROUND(" + data + ",2) FROM measurement WHERE " + data + " IS NOT NULL AND userID='" + Data.instance.userID + "' ORDER BY date DESC"; //ORDER BY date DESC
        DB.DataBaseRead(query);
        while (DB.dataReader.Read())
        {
            try
            {
                //dic.Add(DB.dataReader.GetString(0), DB.dataReader.GetFloat(1));
                dic.Add(DB.dataReader.GetString(0), DB.dataReader.GetFloat(1));
                if (dic.Count() == 7)
                {
                    /*dic = tmp.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);*/
                    break;
                }
            }
            catch
            {
                continue; //같은 날짜 중복되면 컨티뉴
            }
        }
    }

    public float GetTotalAverageData(string data)
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
        switch (data)
        {
            case "maxPower":
                return tmp.Average() * datasToProcess[0];
            case "risingTime":
                return tmp.Average() * datasToProcess[1];
            case "releaseTime":
                return tmp.Average() * datasToProcess[2];
            case "frequency":
                return tmp.Average() * datasToProcess[3];
            case "interval":
                return tmp.Average() * datasToProcess[4];
            case "accuracy":
                return tmp.Average() * datasToProcess[5];
            case "rmse":
                return tmp.Average() * datasToProcess[6];
            case "boxCount":
                return tmp.Average() * datasToProcess[7];
            default:
                break;
                //Debug.Log("Average : "+tmp.Average());
        }
                return tmp.Average();
        
    }

    public void ProcessData(SortedDictionary<string,float> dic, string name)
    {
        switch (name)
        {
            case "maxPower":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[0];
                });
                break;
            case "risingTime":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[1];
                });
                break;
            case "releaseTime":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key]  * datasToProcess[2];
                });
                break;
            case "frequency":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[3];
                });
                break;
            case "interval":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[4];
                });
                break;
            case "accuracy":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[5];
                });
                break;
            case "rmse":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[6];
                });
                break;
            case "boxCount":
                dic.Keys.ToList().ForEach(key =>
                {
                    dic[key] = dic[key] * datasToProcess[7];
                });
                break;
            default:
                break;
        }
    }
}

