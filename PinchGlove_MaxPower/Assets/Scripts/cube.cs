using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TMPro;


public class cube : MonoBehaviour
{
    // Serial serial;
    // Start is called before the first frame update
    //public GameObject cube1;
    public TextMeshProUGUI guide_text;
    public static int pinch_Max = 0;

    private float maxPowerTimer = 0f;
    private List<int> inputdata_list = new List<int>();

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {  /*
        cube1.transform.Translate(new Vector3(Inputdata.index_F/1000, 0, 0)/50);
        cube1.transform.Translate(new Vector3((-Inputdata.mid_F / 1000), 0, 0) / 50);
        cube1.transform.Translate(new Vector3(0, 0, (Inputdata.ring_F / 1000)) / 50);
        cube1.transform.Translate(new Vector3(0, 0, (-Inputdata.little_F / 1000)) / 50);
        //cube1.transform.rotation = Quaternion.Euler(Inputdata., 0, 0);
        
        if (Inputdata.index_F > 100)
        {
            cube1.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }
        if (Inputdata.mid_F > 100)
        {
            cube1.transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        if (Inputdata.ring_F > 100)
        {
            cube1.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
        if (Inputdata.little_F > 100)
        {
            cube1.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
        }
        */
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("GetMaxPower실행");
            StartCoroutine(count3second(3f));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Serial.instance.SerialSendingStart();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Serial.instance.SerialSendingStop();
        }
    }

    //n초 동안 실행하는 코루틴, InvokeRepeating 사용하는 방법도 있는데 안해봄
    IEnumerator count3second(float duration) // duration의 값(초) 동안 실행하는 코루틴 https://wergia.tistory.com/24 
    {
        guide_text.gameObject.SetActive(true);
        guide_text.text = "시작 후 3초동안 힘을 최대로 눌러주세요!";
        yield return new WaitForSecondsRealtime(4f);
        guide_text.gameObject.SetActive(false);

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
        Debug.Log("배열 수: " + inputdata_list.Count);
        Debug.Log("Max :"+inputdata_list.Max());
        pinch_Max = inputdata_list.Max();


        //bool checkPinchOut = true;
        while (true)
        {
            guide_text.gameObject.SetActive(true);
            guide_text.text = "손가락을 떼주세요!";
            //Debug.Log("손가락을 떼세요 (k누르기)");
            if (Inputdata.index_F < 10)//(Input.GetKeyDown(KeyCode.K))
            {
                guide_text.gameObject.SetActive(false);
                animator.SetBool("bool_smash2", true);
                animator.SetBool("bool_smash1", false);
                yield return new WaitForSecondsRealtime(1f);
                animator.SetBool("bool_smash2", false);
                break;
            }
            yield return null;
        }
    }

}