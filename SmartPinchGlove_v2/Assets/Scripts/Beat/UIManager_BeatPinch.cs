using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager_BeatPinch : MonoBehaviour
{
    public GameObject Mode_Panel;
    public GameObject Song_Panel;
    public GameObject content;

    bool isDifficultySet = false;
    bool isGameSpeedSet = false;
    public bool isSongSet = false;

    public string Mode = "";
    public GameObject toggle;
    public List<string> SongNames = new List<string>();

    Text hitText;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Mode_Panel = GameObject.Find("Mode_Panel");
        Song_Panel = GameObject.Find("Song_Panel");
        content = GameObject.Find("Content");

        //디렉토리 이름 가져오기
        string path = Application.dataPath + "/Resources/";
        DirectoryInfo di = new DirectoryInfo(path);
        foreach(DirectoryInfo d in di.GetDirectories()) 
        {
            SongNames.Add(d.Name);
        }

        //디렉토리 이름으로 노래 선택 버튼 생성해주기
        Text text = toggle.transform.GetChild(1).GetComponent<Text>();
        Image image = toggle.transform.GetChild(0).GetComponent<Image>();
        foreach (string song in SongNames) 
        {
            text.text = song;
            toggle.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
            image.sprite = Resources.Load<Sprite>(song + "/" + song + "_Img");
            Instantiate(toggle, content.transform);
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
        Song_Panel.SetActive(false);
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
        if (isDifficultySet && isGameSpeedSet && isSongSet)
        {
            isDifficultySet = false;
            isGameSpeedSet = false;
            isSongSet = false;
            SceneChange();
        }
    }
    //게임 플레이 씬 로드
    public void SceneChange()
    {
        SceneManager.LoadScene("Beat");
    }

    public void SceneChangeToMain()
    {
        //dondestory했던 애들 파괴 ???
        Destroy(GameObject.Find("UIManager_BeatPinch").gameObject);
        Destroy(GameObject.Find("SongSelect").gameObject);
        SceneManager.LoadScene("Main");
    }
}
