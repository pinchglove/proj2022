using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager_BeatPinch : MonoBehaviour
{
    public GameObject Mode_Panel;
    public GameObject SelectSong_Panel;
    public GameObject Difficulty_Panel;

    bool isDifficultySet = false;
    bool isGameSpeedSet = false;

    public string Mode = "";
    public GameObject button;
    public List<string> SongNames = new List<string>();

    Text hitText;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Mode_Panel = GameObject.Find("Mode");
        SelectSong_Panel = GameObject.Find("SelectSong");
        Difficulty_Panel = GameObject.Find("Difficulty");


        //디렉토리 이름 가져오기
        string path = Application.dataPath + "/Resources/";
        DirectoryInfo di = new DirectoryInfo(path);
        foreach(DirectoryInfo d in di.GetDirectories()) 
        {
            SongNames.Add(d.Name);
        }

        //디렉토리 이름으로 노래 선택 버튼 생성해주기
        Text text = button.transform.GetChild(0).GetComponent<Text>();
        foreach (string song in SongNames) 
        {
            text.text = song;
            Instantiate(button, GameObject.Find("Content").transform);
        }
        initPanels();
    }
    private void Start()
    {
       
    }

    public void HitCount()
    {
        hitText = GameObject.Find("Hit_Text").GetComponent<Text>();
        hitText.text =  Data.instance.noteCount.ToString() + " Hit";
    }

    public void initPanels()
    {
        Mode_Panel.SetActive(true);
        SelectSong_Panel.SetActive(false);
        Difficulty_Panel.SetActive(false);
    }

    public void SetModeByButton(string str)
    {
        Mode = str;
    }

    public void setDifBool()
    {
        isDifficultySet = true;
    }
    public void setSpeedBool()
    {
        isGameSpeedSet = true;
    }

    //노래 선곡 화면에서 게임 플레이 씬으로 갈때 체크
    public void ChecktoLoadScene()
    {
        if (isDifficultySet && isGameSpeedSet)
        {
            isDifficultySet = false;
            isGameSpeedSet = false;
            SceneChange();
        }
    }
    //게임 플레이 씬 로드
    public void SceneChange()
    {
        SceneManager.LoadScene("Beat");
    }

 
}
