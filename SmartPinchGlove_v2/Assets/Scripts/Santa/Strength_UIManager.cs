using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Strength_UIManager : MonoBehaviour
{
    public int countdownTime = 3;
    public float playTime = 3f;
    public GameObject start_Panel;
    public GameObject play_Panel;
    public GameObject end_Panel;
    Button start_Button;
    Button jumpToStart_Button;
    Button showEnd_Button;
    Text count_Text;
    Text remainingTIme_Text;
    Text restTime_Text;
    public Text result_Text;
    public Text guide_Text;
    bool isJumpButtonClicked = false;

    #region 싱글톤
    private static Strength_UIManager instance;
    public static Strength_UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion
    void Start()
    {
        guide_Text = play_Panel.transform.Find("Guide_Text").GetComponent<Text>();
        count_Text = start_Panel.transform.Find("count_Text").GetComponent<Text>(); //카운트 다운 Text 
        remainingTIme_Text = play_Panel.transform.Find("remainingTime").GetComponent<Text>();  //남은 시간 Text 
        start_Button = start_Panel.transform.Find("start_Button").GetComponent<Button>();
        restTime_Text = start_Panel.transform.Find("restTime_Text").GetComponent<Text>();
        result_Text = start_Panel.transform.Find("result_Text").GetComponent<Text>();
        jumpToStart_Button = start_Panel.transform.Find("jumpToStart_Button").GetComponent<Button>();
        showEnd_Button = start_Panel.transform.Find("showEnd_Button").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(CountdownToStart(countdownTime));
        }
    }


    public void startCountdown()
    {
        StartCoroutine(CountdownToStart(countdownTime));
    }

    IEnumerator CountdownToStart(int countdownTime)
    {
        // 게임 시작 전 카운트다운
        guide_Text.gameObject.SetActive(true);
        guide_Text.text = "시작 후 3초 동안 최대로 눌러주세요!";
        count_Text.gameObject.SetActive(true); // 카운트 다운 패널 활성화

        while (countdownTime > 0)
        {
            count_Text.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        count_Text.text = "시작!";
        yield return new WaitForSecondsRealtime(1f);
        guide_Text.gameObject.SetActive(false);
        count_Text.gameObject.SetActive(false);
        start_Panel.SetActive(false);

        StartCoroutine(RemainingTimeCount(playTime));
    }

    IEnumerator RemainingTimeCount(float playTime)        //남은시간 표시
    {
        remainingTIme_Text.gameObject.SetActive(true); //남은시간 Text 활성화
        float remainingTime = playTime;

        while (remainingTime > 0)
        {
            remainingTIme_Text.text = "남은시간:" + remainingTime.ToString("F1") + "초";
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        remainingTIme_Text.gameObject.SetActive(false);
    }

    //다시 시작을 위한 패널 초기화
    public void ResetPanels()
    {
        start_Panel.SetActive(true);
        start_Button.gameObject.SetActive(false);
        jumpToStart_Button.gameObject.SetActive(true);
        guide_Text.gameObject.SetActive(true);
        guide_Text.text = "30초 휴식 후 진행 해주세요";
        result_Text.gameObject.SetActive(true);
        StartCoroutine(Countdown30s());
    }

    IEnumerator Countdown30s()
    {
        restTime_Text.gameObject.SetActive(true);
        float time = 30f;
        while (time > 0)
        {
            if (isJumpButtonClicked == true)
            {
                isJumpButtonClicked = false;
                break;
            }
            restTime_Text.text = time.ToString("F0") + "초";
            time -= Time.deltaTime;
            yield return null;
        }
        restTime_Text.gameObject.SetActive(false);
        result_Text.gameObject.SetActive(false);
        jumpToStart_Button.gameObject.SetActive(false);
        start_Button.gameObject.SetActive(true);
    }

    public void setBoolJumpButton()
    {
        if (!isJumpButtonClicked)
        {
            isJumpButtonClicked = true;
        }
        else
        {
            isJumpButtonClicked = false;
        }
    }

    public void panelSetting_forend()
    {
        start_Panel.SetActive(true);
        start_Button.gameObject.SetActive(false);
        showEnd_Button.gameObject.SetActive(true);
    }

    public void showEndPanel()
    {
        start_Panel.SetActive(false);
        end_Panel.SetActive(true);
        end_Panel.transform.Find("averageScore_Text").GetComponent<Text>().text = "최종 점수: " + Data.instance.maxPower_average.ToString() + "점";
        end_Panel.transform.GetChild(1).GetComponent<Text>().text = "상승 시간: " + Data.instance.risingTime.ToString("F2");
        end_Panel.transform.GetChild(2).GetComponent<Text>().text = "하강 시간: " + Data.instance.releaseTime.ToString("F2");
    }
}

//함수들? 일시정지, 
