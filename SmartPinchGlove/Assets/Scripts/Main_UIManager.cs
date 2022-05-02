using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_UIManager : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject mainPanel;
    public GameObject testPanel;
    public GameObject practicePanel;
    public GameObject signUpPanel;
    public GameObject pausePanel;
    public GameObject firstPagePanel;
    public GameObject turnOffPanel;
    
    //회원가입 게임오브젝트 가져오기 추가함
    public GameObject SignupManager;

    void Start() 
    {   
        signUpPanel.SetActive(false);
        loginPanel.SetActive(false);
        mainPanel.SetActive(false);
        testPanel.SetActive(false);
        practicePanel.SetActive(false);
        pausePanel.SetActive(false);
        turnOffPanel.SetActive(false);
        if (Data.instance.isLogedin == false)
        {
            firstPagePanel.SetActive(true);
        }
        else
        {
            mainPanel.SetActive(true);
        }
    }


    // 메인 페이지 ->  첫 페이지
    public void MainToFirst()
    {
        SignupManager.GetComponent<SignUp>().InitializeLoginText();
        Data.instance.isLogedin = false;
        mainPanel.SetActive(false);
        firstPagePanel.SetActive(true);
    }

    // 메인 페이지 -> 측정 페이지
    public void TestPage()
    {
        mainPanel.SetActive(false);
        testPanel.SetActive(true);
    }

    // 메인 페이지 -> 훈련 페이지
    public void PracticePage()
    {
        mainPanel.SetActive(false);
        practicePanel.SetActive(true);
    }

    // 로그인 페이지 -> 메인 페이지 
    public void LoginToMain()
    {
        loginPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 측정 페이지 -> 메인 페이지 
    public void TestToMain()
    {
        testPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 훈련 페이지 -> 메인 페이지 
    public void PracticeToMain()
    {
        practicePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 측정 페이지 -> 측정 컨텐츠 페이지 
    public void SceneChange_PinchStrength()
    {
        //testPanel.SetActive(false);
        SceneManager.LoadScene("Pinch_Strength");
    }
    public void SceneChange_PinchFrequency()
    {
        //testPanel.SetActive(false);
        SceneManager.LoadScene("Pinch_Frequency");
    }

    // 훈련 페이지 -> 훈련 컨텐츠 페이지 
    public void PracticeContent()
    {
        practicePanel.SetActive(false);
        // 활쏘기 씬 불러오기!!

    }

    // 첫 페이지 -> 회원가입 페이지 
    public void FirstToSignUp()
    {
        SignupManager.GetComponent<SignUp>().InitializeSignUpText();
        firstPagePanel.SetActive(false);
        signUpPanel.SetActive(true);
    }

    // 첫 페이지 -> 로그인 페이지 
    public void FirstToLogin()
    {
        SignupManager.GetComponent<SignUp>().InitializeLoginText();
        firstPagePanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    // 로그인 페이지 -> 첫 페이지 
    public void LoginToFirst()
    {
        loginPanel.SetActive(false);
        firstPagePanel.SetActive(true);
    }

    // 회원가입 페이지 -> 첫 페이지 
    public void SignUpToFirst()
    {
        signUpPanel.SetActive(false);
        firstPagePanel.SetActive(true);
    }

    // 회원가입 페이지 -> 로그인 페이지 
    public void SignUpToLogin()
    {
        SignupManager.GetComponent<SignUp>().InitializeLoginText();
        signUpPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 회원가입 페이지 -> 메인 페이지 
    public void SignUpToMain()
    {
        signUpPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 일시정지 패널 켜기
    public void OnPauseBtn()
    {
        pausePanel.SetActive(true);
    }

    // 일시정지 패널 끄기
    public void OffPauseBtn()
    {
        pausePanel.SetActive(false);
    }

    // 일시정지 패널 -> 메인 페이지
    public void PauseToMain()
    {
        pausePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    // 종료 패널 켜기
    public void OnTurnOffBtn()
    {
        turnOffPanel.SetActive(true);
    }

    // 종료 패널 끄기
    public void OffTurnOffBtn()
    {
        turnOffPanel.SetActive(false);
    }

    // 게임 종료 
    public void Quit()
    {
        Application.Quit();
    }
}
