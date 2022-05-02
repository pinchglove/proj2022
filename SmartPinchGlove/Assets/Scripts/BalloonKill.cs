using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BalloonKill : MonoBehaviour
{
    #region 변수선언
    public GameObject Balloons; // 풍선 부모 오브젝트
    public GameObject[] babyballs; // 풍선 각 객체 배열
    public float[] idleTime; // 탭 사이 간격 체크용 시간 변수 저장용 배열
    public float playTime = 0f; // 전체 게임 시간 체크용 시간 변수
    public bool isKilling = false;  //풍선 터트리기 코루틴 체크용 불 변수
    public int ballcount = 0;   //풍선 배열 인덱스
    public GameObject POP; // 파티클 시스템 부모 오브젝트
    public GameObject[] Pop;  // 파티클 시스템 배열
    public bool isPlaying;  // 게임 실행여부 확인용 불 변수
    public AudioClip popSound;  // 효과음
    public AudioSource audioSource; //오디오 플레이어
    public GameObject EndPanel; // 결과패널
    public GameObject PlaytimePanel; // 수행시간 패널
    public GameObject StartPanel;   //시작버튼 + (카운트다운)패널
    public Text[] text;  //결과 텍스트 배열
    public float avar; //평균
    public float mean; //표준편차
    public Text[] playtext; 
    public Text countdown_Text; //카운트다운 텍스트
    #endregion

    #region 표준편차 함수
    private float Mean(float[] idle)
    {
        float mean = 0f;
        for (int i = 0; i < idle.Length; i++)
        {
            mean += Mathf.Pow((idle[i] - Avrg(idle)), 2);
        }

        return (Mathf.Sqrt(mean / idle.Length));
    }
    #endregion

    #region 평균 함수
    private float Avrg(float[] idle)
    {
        float Aver = 0f;
        for (int i = 0; i < idle.Length; i++)
        {
            Aver += idle[i];
        }
        return (Aver / idle.Length);
    }
    #endregion

    #region 텍스트 배열 자동 받아오기
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

    #region 풍선 배열 자동 받아오기
    public GameObject[] getBalls(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    #endregion

    #region 터지는 파티클 배열 자동 받아오기
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

    #region 태핑하면 풍선 없애기 함수
    IEnumerator killball(int count)
    {
        isKilling = true;   
        Pop[count].GetComponent<ParticleSystem>().Play();   //풍선없어질때 터지는 파티클 플레이
        babyballs[count].gameObject.SetActive(false);   //풍선없어지기

        while (true)
        {
            if (count >= babyballs.Length-1) 
            {
                isPlaying = false;          //풍선 총개수보다 count가 커지면 isplaying off
                EndPanel.SetActive(true);  //결과패널 액티브
            }

            if (Inputdata.index_F < 30)  // 탭 뗄 때!
            //if (Inputdata.thumb < 1000)
            {
                ballcount++;  //카운트 업
                isKilling = false; 
                break;
            }
            yield return null;
        }
        yield return null;
    }
    #endregion

    void Start()
    {
        Balloons = GameObject.FindWithTag("Balloons"); //풍선 부모 객체
        POP = GameObject.FindWithTag("POP"); // 파티클 부모객체
        babyballs = getBalls(Balloons); // 풍선 각 객체 받아오기
        playTime = 0f; // 플레이시간 초기화
        isKilling = false; 
        ballcount = 0;  //풍선개수 세기 초기화
        idleTime = new float[babyballs.Length]; // 탭사이 유휴시간 배열 초기화
        Pop = getPop(POP); //풍선 파티클 각 객체 받아오기
        isPlaying = false; 
        audioSource = this.GetComponent<AudioSource>(); //오디오 소스 받아오기
        EndPanel = GameObject.FindWithTag("EndPanel"); //마지막 패널 받아오기
        text = getText(EndPanel); // 텍스트 배열 받아오기
        EndPanel.SetActive(false);  //End패널 꺼진상태로 시작
        PlaytimePanel = GameObject.FindWithTag("PlayTimePanel"); // 게임중 수행시간 체크용 패널
        PlaytimePanel.SetActive(false);
        playtext = getText(PlaytimePanel);
        StartPanel = GameObject.Find("StartPanel");
        countdown_Text = StartPanel.transform.Find("countdown_Text").gameObject.GetComponent<Text>();
        //start패널 액티브일 때 timeScale 0f로 시작하기 수정
    }


    public void Update()
    {
        if(ballcount >= 10)
        {
            ballcount = 0;
        }
        if (isPlaying)
        {
            playTime += Time.deltaTime;
            idleTime[ballcount] += Time.deltaTime;
        }

        if (isPlaying)
        {
            if (Inputdata.index_F >= 30 && !isKilling)
            //if (Input.GetKeyDown(KeyCode.B) && !isKilling)
            //if (Inputdata.thumb >= 2000 && !isKilling)
            {
                StartCoroutine(killball(ballcount));
                audioSource.PlayOneShot(popSound);
            }
        }
        text[0].text = playTime.ToString("N2") + " 초";
        text[1].text = (10 / playTime).ToString("N2")+" Hz";
        avar = Avrg(idleTime);
        mean = Mean(idleTime);
        text[2].text = avar.ToString("N2") + " 초";
        text[3].text = mean.ToString("N2") + " 초";
        playtext[0].text = playTime.ToString("N2") + "초";
    }
    #region 카운트다운 코루틴과 함수
    public void startCount()
    {
        StartCoroutine(CountdownToStart(3));
    }

    IEnumerator CountdownToStart(int countdownTime)
    {
        // 게임 시작 전 카운트다운
        countdown_Text.gameObject.SetActive(true);

        while (countdownTime > 0)
        {
            countdown_Text.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdown_Text.text = "시작!";
        yield return new WaitForSecondsRealtime(1f);
        countdown_Text.gameObject.SetActive(false);
        StartPanel.transform.Find("Button").gameObject.SetActive(true);
        StartPanel.gameObject.SetActive(false);
        PlaytimePanel.gameObject.SetActive(true);
        isPlaying = true;
    }
    #endregion
}
//탭간격(o), (10/전체시간)Hz, 파티클(O)
