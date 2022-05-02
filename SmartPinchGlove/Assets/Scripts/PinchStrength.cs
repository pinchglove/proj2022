using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class PinchStrength : MonoBehaviour
{
    public static int pinch_Max = 0;
    public int playNumber = 0;
    public int[] results;
    private float maxPowerTimer = 0f;
    private List<int> inputdata_list = new List<int>();
    
    Animator animator;

    private void Start()
    {
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
        StartCoroutine(PlaySanta(3f));
    }

    //n초 동안 실행하는 코루틴, InvokeRepeating 사용하는 방법도 있는데 안해봄
    IEnumerator PlaySanta(float duration) // duration의 값(초) 동안 실행하는 코루틴 https://wergia.tistory.com/24 
    {
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("코루틴 시작");
        inputdata_list.Clear();  //데이터 받는 리스트 비우기
        maxPowerTimer = 0;  //시간 변수 0으로 초기화
        animator.SetBool("bool_smash1", true);

        while (maxPowerTimer < duration)    //while문 ***안에 yield return null; 들어가야함
        {
            maxPowerTimer += Time.deltaTime;    //시간 변수에 Time.deltatime 더해주기
            inputdata_list.Add(Inputdata.index_F);   //리스트에 값 추가 반복
            yield return null;  // 코루틴 안에 while문 들어가려면 이게 필수
        }
        //반복문 끝나고 실행 할 거 해주면 됨
        Debug.Log("Max :"+inputdata_list.Max());
        pinch_Max = inputdata_list.Max();
        Debug.Log(inputdata_list.Count);


        //bool checkPinchOut = true;
        while (true)
        {
            Strength_UIManager.Instance.guide_Text.gameObject.SetActive(true);
            Strength_UIManager.Instance.guide_Text.text = "손가락을 떼주세요";
            if (Inputdata.index_F < 10) //(Input.GetKeyDown(KeyCode.K)) 
            {
                Strength_UIManager.Instance.guide_Text.gameObject.SetActive(false);
                animator.SetBool("bool_smash2", true);
                animator.SetBool("bool_smash1", false);
                yield return new WaitForSecondsRealtime(1f);
                animator.SetBool("bool_smash2", false);
                break;
            }
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        Strength_UIManager.Instance.result_Text.text = (playNumber+1).ToString("F0") + "번 결과: " + pinch_Max.ToString() + "점";
        results[playNumber] = inputdata_list.Max(); //배열에 잘 들어가는지 확인해야함
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
            Data.instance.maxPower_average = tmp / 3;
            //Strength_UIManager.Instance.showEndPanel();
        }
    }

}