using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int countdownTime = 3;
    public float playTime = 3f;
    public GameObject time_GameObject;
    Text count_Text;
    Text remainingTIme_Text;
    

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("GetMaxPower실행");
            StartCoroutine(CountdownToStart(countdownTime, playTime));
        }
    }

    IEnumerator CountdownToStart(int countdownTime, float playTime)
    {
        // 게임 시작 전 카운트다운
        time_GameObject.transform.Find("countdown").gameObject.SetActive(true); // 카운트 다운 패널 활성화
        time_GameObject.transform.Find("remainingTime").gameObject.SetActive(true); //남은시간 Text 활성화
        count_Text = time_GameObject.transform.Find("countdown").Find("count").GetComponent<Text>(); //카운트 다운 Text 받아오기
        remainingTIme_Text = time_GameObject.transform.Find("remainingTime").GetComponent<Text>();  //남은 시간 Text 받아오기

        while (countdownTime > 0)
        {
            count_Text.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        count_Text.text = "시작!";
        yield return new WaitForSecondsRealtime(1f);
        time_GameObject.transform.Find("countdown").gameObject.SetActive(false);

        //카운트 다운 후 남은시간 표시
        float remainingTime = playTime;
        while (remainingTime > 0)
        {
            remainingTIme_Text.text = "남은시간:" + remainingTime.ToString("F1") + "초";
            remainingTime -= Time.deltaTime;
            yield return null;
        }
    }
}
