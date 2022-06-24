using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausePanel, resultPanel;
    public Text remainingTimeText;
    public Text averageRMSE;
    public Text maxPower;
    public Text frequency;
    public Text pauseRT;
    public Text pauseAR;
    public Text pauseMP;
    public Text pauseFR;
    public Text resultAR;
    public Text resultMP;
    public Text resultFR;
    bool isDBInserted = false;  //DB 한번만 입력하기
    
    #region instance
    public static GameSceneUI instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    
    void Start()
    {
        //Time.timeScale = 1f;
        if(pausePanel.activeSelf == true)
        {
            pausePanel.SetActive(false);
        }
        if(resultPanel.activeSelf == true)
        {
            resultPanel.SetActive(false);
        }
        isDBInserted = false;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTimeText.text = "남은 시간: " + Manager_Tracking.globalTimer.ToString("N2") + "초";
        maxPower.text = "설정 최대 힘: " + PlayerBehaviour.mf.ToString();
        averageRMSE.text = "평균 오차: " + Manager_Tracking.rmse.ToString("N2");
        frequency.text = "측정 빈도: " + HitItem.fq.ToString("N3");
        pauseAR.text = averageRMSE.text;
        pauseMP.text = maxPower.text;
        pauseFR.text = frequency.text;
        pauseRT.text = remainingTimeText.text;
        resultAR.text = averageRMSE.text;
        resultMP.text = maxPower.text;
        resultFR.text = frequency.text;
    }
    public void Pause() // 게임창에서 일시정지 버튼을 눌렀을때
    {;
        Manager_Tracking.paused_Tracking = true;//Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    // 게임 재개
    public void Resume() // 계속하기 버튼을 눌렀을때
    {
        Manager_Tracking.paused_Tracking = false;//Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    // 게임 결과 화면
    public void GameOver()
    {
        Manager_Tracking.paused_Tracking = true;//Time.timeScale = 0f;
        resultPanel.SetActive(true);
        resultAR.text = averageRMSE.text;
        resultMP.text = maxPower.text;

        if (!isDBInserted)
        {
            isDBInserted = true;            
            Data.instance.rmseValue = Manager_Tracking.rmse;
            //쿼리문
            string query = "INSERT INTO measurement (date,userID,gameID,rmse) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "04M" + "','" + Data.instance.rmseValue + "')";
            DB.DatabaseInsert(query);
        }
        //StopCoroutine(AddScore());
    }

    // 게임 재시작
    public void Restart()
    {
        Manager_Tracking.rmse = 0;
        Manager_Tracking.doy.Clear();
        Manager_Tracking.number = 0;
        SceneManager.LoadScene("Tracking");
        //GameSceneUI._Instance.GameStart();
    }

    // 게임 종료
    public void Quit()
    {
        Application.Quit();
    }

    public void ToHome()
    {
        SceneManager.LoadScene("Main_Tracking");
        //SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }
}
