using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System;

public class PinchData
{
    public float pinch { get; set; }
    public float time { get; set; }

    public PinchData(float p, float t) {
        pinch = p;
        time = t;
    }
}

public class PinchStrength : MonoBehaviour
{
    public static int pinch_Max = 0;
    public int playNumber = 0;
    public int[] results;
    private float maxPowerTimer = 0f;

    List <PinchData> pinchDatas = new List<PinchData>();
    List<float> risingTimeList = new List<float>();
    List<float> releaseTimeList = new List<float>();

    Animator animator;
    AudioSource beep;

    private void Start()
    {
        beep = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        results = new int[5];
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("GetMaxPower실행");
            StartCoroutine(PlaySanta(3f));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Serial.instance.Active();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Serial.instance.End();
        }
    }

    public void startSanta()
    {
        StartCoroutine(PlaySanta(6f));
    }

    //n초 동안 실행하는 코루틴, InvokeRepeating 사용하는 방법도 있는데 안해봄
    IEnumerator PlaySanta(float duration) // duration의 값(초) 동안 실행하는 코루틴 https://wergia.tistory.com/24 
    {
        yield return new WaitForSecondsRealtime(4f);
        bool beepcheck = true;  //한번만 울리기 위한 bool
        beep.Play();
        Debug.Log("코루틴 시작");
        pinchDatas.Clear();
        maxPowerTimer = 0;  //시간 변수 0으로 초기화
        animator.SetBool("bool_smash1", true);

        while (maxPowerTimer < duration)    //while문 ***안에 yield return null; 들어가야함
        {
            maxPowerTimer += Time.deltaTime;    //시간 변수에 Time.deltatime 더해주기
            pinchDatas.Add(new PinchData(SelectFinger.GetInputData(), maxPowerTimer));
            
            if(maxPowerTimer >= 3f)
            {
                if (beepcheck)
                {
                    beep.Play();
                    beepcheck = false;
                }
                Strength_UIManager.Instance.guide_Text.gameObject.SetActive(true);
                Strength_UIManager.Instance.guide_Text.text = "손가락을 떼주세요";
                if (SelectFinger.GetInputData() < 10) //(Input.GetKeyDown(KeyCode.K)) 
                {
                    Strength_UIManager.Instance.guide_Text.gameObject.SetActive(false);
                    animator.SetBool("bool_smash2", true);
                    animator.SetBool("bool_smash1", false);
                    yield return new WaitForSecondsRealtime(1f);
                    animator.SetBool("bool_smash2", false);
                    break;
                }
            }

            yield return null;  // 코루틴 안에 while문 들어가려면 이게 필수
        }
        //데이터 처리부분 (max값, risingtime, releasetime)
        pinch_Max = Convert.ToInt32(pinchDatas.Max(x => x.pinch));  //최대 값

        var risingStart = Convert.ToInt32(pinchDatas.Max(x => x.pinch) * 0.1); //시작 후 최초 10퍼 값
        var risingEnd = Convert.ToInt32(pinchDatas.Max(x => x.pinch) * 0.9);   //시작 후 최초 90퍼 값

        var risingStartTime = pinchDatas.Where(x => x.pinch >= risingStart).Min(x => x.time);    //10퍼 도달 시간
        var risingEndTime = pinchDatas.Where(x => x.pinch >= risingEnd).Min(x => x.time);        //90퍼 도달 시간
        //Data.instance.risingTime = risingEndTime - risingStartTime;
        Debug.Log("RisingTime: " + (risingEndTime - risingStartTime));
        risingTimeList.Add(risingEndTime - risingStartTime);
        
        var tmpList = pinchDatas.Where(x => x.time >= 3).ToList();    // 2초 이후 리스트 tmp
        var releaseStart = Convert.ToInt32(tmpList.Max(x => x.pinch) * 0.8); //시작 2초 후 90퍼 값
        var releaseEnd = Convert.ToInt32(pinchDatas.Max(x => x.pinch) * 0.1); //시작 2초 후 10퍼 값

        var releaseStartTime = tmpList.Where(x => x.pinch >= releaseStart).Min(x => x.time);    //90퍼 도달 시간
        var releaseEndTime = tmpList.Where(x => x.pinch <= releaseEnd).Min(x => x.time);        //10퍼 도달 시간
        //Data.instance.releaseTime = releaseEndTime - releaseStartTime;
        Debug.Log("RisingTime: " + (releaseEndTime - releaseStartTime));
        releaseTimeList.Add(releaseEndTime - releaseStartTime);
        

        yield return new WaitForSecondsRealtime(1f);
        Strength_UIManager.Instance.result_Text.text = (playNumber+1).ToString("F0") + "번 결과: " + pinch_Max.ToString("F0") + "점";
        results[playNumber] = Convert.ToInt32((double)pinchDatas.Max(x => x.pinch)); //배열에 잘 들어가는지 확인해야함
        if (playNumber < 2)
        {
            Strength_UIManager.Instance.ResetPanels();
            playNumber++;
        }
        else
        {
            Strength_UIManager.Instance.panelSetting_forend();
            int tmp = 0;
            for (int i = 0; i<3; i++)
            {
                tmp += results[i];
            }
            Data.instance.maxPower_average = tmp / 3f;
            Data.instance.risingTime = risingTimeList.Average();
            Data.instance.releaseTime = releaseTimeList.Average();
            //DB 넣는 구간 추가
            string query = "INSERT INTO measurement (date,userID,gameID,maxPower,risingTime,releaseTime) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "01M" + "','" + Data.instance.maxPower_average + "','" + Data.instance.risingTime + "','" + Data.instance.releaseTime + "')";
            DB.DatabaseInsert(query);
            //Strength_UIManager.Instance.showEndPanel();
        }
    }
}