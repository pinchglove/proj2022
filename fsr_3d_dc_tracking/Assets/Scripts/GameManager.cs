using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int selectedFinger = 1;  //기본 검지
    public void setFinger()  //측정할 손가락 선택
    {
        Dropdown dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();   //이름 바꾸기 FingerDropdown
        switch (dropdown.value)
        {
            case 0 :
                selectedFinger = 0; // 검지
                break;
            case 1 :
                selectedFinger = 1; //중지
                break;
            case 2:
                selectedFinger = 2; //약지
                break;
            case 3:
                selectedFinger = 3; //소지
                break;
            default:
                break;
        }
    }

    public int GetInputData() // 선택된 손가락의 데이터 리턴
    {
        switch (selectedFinger)
        {
            case 0:
                return Inputdata.index_F; // 검지
            case 1:
                return Inputdata.mid_F; //중지
            case 2:
                return Inputdata.ring_F; //약지
            case 3:
                return Inputdata.little_F; //소지
            default:
                return 0;
        }
    }

    #region singleton
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion
    public void Pause(bool isPaused)
    {
        if (!isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void ReStartScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}
