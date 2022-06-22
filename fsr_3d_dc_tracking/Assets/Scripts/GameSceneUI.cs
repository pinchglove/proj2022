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
        if (pausePanel.activeSelf == true)
        {
            pausePanel.SetActive(false);
        }
        if (resultPanel.activeSelf == true)
        {
            resultPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        remainingTimeText.text = "남은 시간: " + Manager.globalTimer.ToString("N2") + "초";
        maxPower.text = "설정 최대 힘: " + PlayerBehaviour.mf.ToString();
        averageRMSE.text = "평균 오차: " + Manager.rmse.ToString("N2");
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
        Manager.paused = true;//Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }
    // 게임 재개
    public void Resume() // 계속하기 버튼을 눌렀을때
    {
        Manager.paused = false;//Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    // 게임 결과 화면
    public void GameOver()
    {
        Manager.paused = true;//Time.timeScale = 0f;
        resultPanel.SetActive(true);
        resultAR.text = averageRMSE.text;
        resultMP.text = maxPower.text;
        Data.instance.trackingFreq = HitItem.fq;
        Data.instance.trackMaxForce = PlayerBehaviour.mf;
        Data.instance.rmseValue = Manager.rmse;
        //StopCoroutine(AddScore());
    }

    // 게임 재시작
    public void Restart()
    {
        Manager.rmse = 0;
        Manager.doy.Clear();
        Manager.number = 0;
        SceneManager.LoadScene("SampleScene");
        //GameSceneUI._Instance.GameStart();
    }
    /*
    public void SaveRestart()
    {
        Data.instance.trackingFreq = HitItem.fq;
        Data.instance.trackMaxForce = PlayerBehaviour.mf;
        Data.instance.rmseValue = Manager.rmse;
        Manager.rmse = 0;
        Manager.doy.Clear();
        Manager.number = 0;
        SceneManager.LoadScene("SampleScene");
    }
    */
    // 게임 종료
    public void Quit()
    {
        Application.Quit();
    }

    public void ToHome()
    {
        SceneManager.LoadScene("StartScene");
        //SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }
}
