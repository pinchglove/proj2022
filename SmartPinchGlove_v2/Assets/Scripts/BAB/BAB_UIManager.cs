using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BAB_UIManager : MonoBehaviour
{
    private GameObject Canvas; // 패널 받아오기위해 캔버스를 부모 오브젝트 역할로 선언
    public GameObject[] Panels; //[0]연습패널 [1]실전패널 [2]카운트다운패널 [3]초시계패널 [4]결과패널
    public Text countdownText; // 연습/실전게임 시작 전 카운트다운용 텍스트
    public Text PlayTimeText; // 초시계 패널에 게임시간 세는 용
    public Text BoxCount; // 초시계 패널에 같이 있는 박스갯수 보여주기 위한 텍스트 변수
    public Text Count_Final; // 결과패널에 있는 박스갯수용 텍스트
    public bool isReal = false; //실전게임인지 확인용
    public bool isPrac = false; // 연습게임중인가요?
    public bool isPlaying = false; //실전 게임 중인가요?
    public float PracTime = 15f; // 연습시간 15초
    public float PlayTime = 60f; // 실전게임시간 60초
    bool isDBInserted = false;

    #region Singleton
    private static BAB_UIManager Instance;

    public static BAB_UIManager _Instance
    {
        get { return Instance; }
    }
    void Awake()
    {
        Instance = this;
    }
    #endregion

    #region 연습시간 15초 끝난 후 처리하기 위한 코루틴함수
    IEnumerator PracTimeCheck()
    {

        while (true)
        {
            if (PracTime <= 0f)
            {
                isPrac = false;
                isReal = true;
                Panels[1].SetActive(true);  //실전 패널 활성화
                int nul = 0;
                for (int i = 0; i < Collison._Instance.BlockIns.transform.childCount +nul; i++)
                {
                    if (Collison._Instance.BabyBlockIns[i] == null) { nul++; } //왜인지 모르겠는데 몇개가 널로 잡혀서 널이면 추가해서 다시 파괴하기
                    Destroy(Collison._Instance.BabyBlockIns[i]); // 연습 때 썼던 블록들 다 삭제 후 버튼 클릭시 다시생성
                }
                Panels[2].SetActive(false); // 카운트다운 패널 비활성화
                Panels[3].SetActive(false);  //초시계패널 비활성화
                Collison._Instance.count = 0; // 카운트 변수 초기화
            }

            
                break;
        }
        yield return null;
    }
    #endregion
    #region 플레이시간 60초 끝난 후 처리하기 위한 코루틴함수
    IEnumerator PlayingTimeCheck()
    {

        while (true)
        {
            if (PlayTime <= 0f)
            {
                isPlaying = false;
                Panels[2].SetActive(false); // 카운트다운 패널 비활성화
                Panels[3].SetActive(false); // 초시계 패널 비활성화
                Panels[4].SetActive(true); //결과 패널 활성화

                if (!isDBInserted)
                {
                    Data.instance.boxCount = Collison._Instance.count;

                    //쿼리 입력
                    isDBInserted = true;
                    string query = "INSERT INTO measurement (date,userID,gameID,boxCount) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "05M" + "','" + Data.instance.boxCount + "')";
                    DB.DatabaseInsert(query);
                    GrabberControll.isPinch = false; // VR컨트롤러 오른손 그립 잠금 풀기
                }
            }


            break;
        }

        yield return null;
    }
    #endregion

    #region 카운트다운 코루틴과 함수
    //startCount()함수는 버튼 onClick이벤트로 설정
    public void startCount()
    {
        StartCoroutine(CountdownToStart(3));
    }

    IEnumerator CountdownToStart(int countdownTime)
    {
        
        // 게임 시작 전 카운트다운
        countdownText.gameObject.SetActive(true);

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.text = "시작!";
        GrabberControll.isPinch = true; // VR컨트롤러 오른손 그립 잠금
        yield return new WaitForSecondsRealtime(1f);
        countdownText.gameObject.SetActive(false);
        Panels[2].SetActive(false); //카운트다운 패널 비활성화
        Panels[3].SetActive(true); //초시계패널 활성화
        if (!isReal) { isPrac = true; }
        else if(isReal) { isPlaying = true; }
    }
    #endregion

    
    void Start()
    {
        Canvas = GameObject.FindWithTag("Canvas"); // 캔버스 태그로 찾아오기
        Panels = Collison._Instance.getPreBlock(Canvas); //getPreBlock함수 싱글톤으로 가져와서 캔버스 아래 패널들 배열로 받아오기
        countdownText = Panels[2].transform.Find("CountdownText").gameObject.GetComponent<Text>(); //카운트다운 패널 카운트다운 텍스트
        PlayTimeText = Panels[3].transform.Find("PlayTimeText").gameObject.GetComponent<Text>(); //초시계패널 게임시간 텍스트
        BoxCount = Panels[3].transform.Find("BoxCount").gameObject.GetComponent<Text>(); // 박수갯수 텍스트
        Count_Final = Panels[4].transform.Find("Count").gameObject.GetComponent<Text>(); // 결과화면용 박스갯수 텍스트
        Panels[1].SetActive(false); 
        Panels[2].SetActive(false);
        Panels[3].SetActive(false);
        Panels[4].SetActive(false);
        isDBInserted = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isPrac) // 연습중이면
        {
            StartCoroutine(PracTimeCheck());
            PracTime -= Time.deltaTime;
            PlayTimeText.text = PracTime.ToString("N2");
            BoxCount.text = Collison._Instance.count.ToString();
        }
        if (isPlaying) //게임중이면
        {
            StartCoroutine(PlayingTimeCheck());
            PlayTime -= Time.deltaTime;
            PlayTimeText.text = PlayTime.ToString("N2");
            BoxCount.text = Collison._Instance.count.ToString();
        }
        else
        {
            Count_Final.text = Collison._Instance.count.ToString()+" 개";//결과화면텍스트
            

        }


    }

}
