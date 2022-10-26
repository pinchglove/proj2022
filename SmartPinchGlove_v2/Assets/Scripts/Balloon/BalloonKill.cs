using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonKill : MonoBehaviour
{
    #region 변수선언
    private GameObject[] BabyballPre; //프리팹 배열
    private GameObject PreBall;  //프리팹 풍선 가지고있는 부모 오브젝트
    private int popcount = 0; // 파티클 이벤트용 인덱스 10되면 0으로 바꿔주기
    private int ballidx = 0; //풍선 Clone 배열용 인덱스 0으로 고정(Destroy 때문)
    private GameObject POP; //파티클 시스템 부모 오브젝트
    private GameObject[] Pop;  //파티클 시스템 배열
    private float idle = 0f; //리스트에 탭 사이 간격 값 넣기용
    private GameObject Panel; //패널 배열 부모오브젝트
    private Text[] PlayTimeText; //플레이타임 패널 텍스트(몇초 남았는지, 태핑수)
    private Text[] EndPanelText; //엔드 패널 텍스트
    private Text countdownText; //스타트 패널 카운트다운
    
    public float PlayingTime = 15f;

    public GameObject[] Panels;
    public float avar; //평균
    public float mean; //표준편차
    public List<float> idleTime = new List<float>(); // 탭 사이 간격 체크용 시간 변수 저장용 배열
    public GameObject[] BabyballIns; //Instanciate된 Clone 배열
    public bool isKilling = false;  //풍선 터트리기 코루틴 체크용 불 변수
    public bool isPlaying = false; //게임 실행 확인용 bool 변수
    public float tapCount = 0;   //터지는 풍선 개수 세주는 count변수
    public float SecTime = 0f; //전체 게임 시간 체크용 시간 변수
    bool isDBInserted = false;
    #endregion

    #region 텍스트 배열 자동 받아오기 getText
    public Text[] getText(GameObject parent)
    {
        Text[] children = new Text[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).GetComponent<Text>();
        }
        return children;
    }
    #endregion

    #region 패널 배열 자동 받아오기 getPanels
    public GameObject[] getPanels(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    #endregion

    #region 풍선 프리팹 배열 자동 설정 함수 getPreBall
    public GameObject[] getPreBall(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    #endregion

    #region Clone 배열 자동 받아오기 getInsBall
    public GameObject[] getInsBall(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount-5];
        for(int i= 5; i < parent.transform.childCount; i++)
        {
            
            children[i-5] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    #endregion

    #region 터지는 파티클 배열 자동 받아오기 getPop
    public GameObject[] getPop(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    #endregion

    #region 태핑하면 풍선 없애기 함수 killball
    IEnumerator killball(int count)
    {
        isKilling = true;
        Pop[popcount].GetComponent<ParticleSystem>().Play();   //풍선없어질때 터지는 파티클 플레이
        Destroy(BabyballIns[count]);  //풍선없어지기
       
        while (true)
        {
            if(PlayingTime <= 0f || SecTime >=14.999f) 
            {
                isPlaying = false;
                Panels[2].SetActive(true);  //결과패널 활성화
                Panels[1].SetActive(false);  //플레이타임패널 비활성화
                if (!isDBInserted)
                {
                    //쿼리 입력
                    string query = "INSERT INTO measurement (date,userID,gameID,tapcount,frequency,interval) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "02M" + "','" + Data.instance.tapCount  + "','" + Data.instance.tapHertz + "','" + Data.instance.avarIdle + "')";
                    DB.DatabaseInsert(query);
                    isDBInserted = true;
                }
            }

            if (SelectFinger.GetInputData() < 30)  // 탭 뗄 때!
            {
                tapCount++; //카운트 업
                popcount++; //카운트 업
                isKilling = false;
                if(popcount >= 10)
                {
                    popcount = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(BabyballPre[i], new Vector3(transform.position.x + 1.5f * i, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f)).transform.parent = PreBall.transform; //상단 5개 풍선
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(BabyballPre[i], new Vector3(transform.position.x + 1.5f * i, transform.position.y, transform.position.z), Quaternion.Euler(90f, 0f, 0f)).transform.parent = PreBall.transform; //하단 5개 풍선
                    }
                }
                

                break;
            }
            yield return null;
        }
        yield return null;
    }
    #endregion

    #region 표준편차 함수
    private float Mean(List<float> idle)
    {
        float mean = 0f;
        for (int i = 0; i < idle.Count; i++)
        {
            mean += Mathf.Pow((idle[i] - Avrg(idle)), 2);
        }

        return (Mathf.Sqrt(mean / idle.Count));
    }
    #endregion

    #region 평균 함수
    private float Avrg(List<float> idle)
    {
        float Aver = 0f;
        for (int i = 0; i < idle.Count; i++)
        {
            Aver += idle[i];
        }
        return (Aver / idle.Count);
    }
    #endregion

    void Start()
    {
        isDBInserted = false;
        PlayingTime = 15f; // 15초부터 SecTime을 실시간으로 빼줘서 0초까지 카운트다운
        SecTime = 0f; //초세기용
        isPlaying = false;
        POP = GameObject.FindWithTag("POP");
        Pop = getPop(POP);
        Panel = GameObject.FindWithTag("Panels"); //패널 부모 오브젝트 찾기
        Panels = getPanels(Panel); //[0] StartPanels [1] PlayTime [2] End
        PreBall = GameObject.FindWithTag("BalloonPrefab"); //풍선 프리팹 부모 오브젝트 찾기
        BabyballPre = getPreBall(PreBall); // 풍선 프리팹 배열 설정
        countdownText = Panels[0].transform.Find("countdownText").gameObject.GetComponent<Text>();
        PlayTimeText = getText(Panels[1]);
        EndPanelText = getText(Panels[2]);
        Panels[1].SetActive(false); //플레이타임패널 끈 상태로 시작
        Panels[2].SetActive(false); //엔드 패널 끈 상태로 시작

        //윗줄 풍선 다섯개
        for (int i = 0; i < 5; i++)  {
            Instantiate(BabyballPre[i], new Vector3(transform.position.x + 1.5f * i, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(90f, 0f, 0f)).transform.parent = PreBall.transform; //상단 5개 풍선
        }
        //아랫줄 풍선 다섯개
        for (int i = 0; i < 5; i++)
        {
            Instantiate(BabyballPre[i], new Vector3(transform.position.x + 1.5f * i, transform.position.y, transform.position.z), Quaternion.Euler(90f, 0f, 0f)).transform.parent = PreBall.transform; //하단 5개 풍선
        }
    }

    void Update()
    {
        //DB 데이터
        Data.instance.tapCount = tapCount;
        Data.instance.tapHertz = tapCount / 15f;
        Data.instance.avarIdle = avar;
        Data.instance.meanIdle = mean;

        BabyballIns = getInsBall(PreBall);
        avar = Avrg(idleTime); //탭사이간격 평균값
        mean = Mean(idleTime); //탭사이간격 표준편차값
        EndPanelText[0].text = tapCount.ToString() + " 개";
        EndPanelText[1].text = (tapCount / 15).ToString("N2") + " Hz";
        EndPanelText[2].text = avar.ToString("N2") + " 초";
        EndPanelText[3].text = mean.ToString("N2") + " 초";
        
        

            if (isPlaying)
            {  
            SecTime += Time.deltaTime;
            PlayingTime = (15f - SecTime);
            PlayTimeText[0].text = tapCount.ToString(); // tapCount알려주기
            PlayTimeText[1].text = PlayingTime.ToString("N1"); //15초부터 카운트다운해주기
            idle += Time.deltaTime;

            if (SelectFinger.GetInputData() >= 30 && !isKilling)
            //if (Input.GetKeyDown(KeyCode.B) && !isKilling)
            {
                StartCoroutine(killball(ballidx));
                idleTime.Add(idle);
                idle = 0;
                //audioSource.PlayOneShot(popSound);
            }
            
        }
    }

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
        yield return new WaitForSecondsRealtime(1f);
        countdownText.gameObject.SetActive(false);
        Panels[0].transform.Find("Button").gameObject.SetActive(true);
        Panels[0].gameObject.SetActive(false);
        Panels[1].gameObject.SetActive(true);
        isPlaying = true;
    }
    #endregion
}
