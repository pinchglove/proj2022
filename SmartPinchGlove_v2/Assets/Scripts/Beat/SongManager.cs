using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip clip;

    public string songName;
    public bool isGameFin;
    public bool isInputESC;
    bool isDBInserted = false;

    public int difficulty = 1;              //난이도 0일때 쉬움, 1일때 보통, 2일때 어려움
    public float gameSpeed = 5f;            //게임 스피드(노드 떨어지는 속도)

    int previewTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        previewTime = 30;
        isDBInserted = false;
    }

    // 게임 시작시 (5초 지나고 재생)
    public void PlayAudioForPlayScene()
    {
        // 시작점 원위치
        music.timeSamples = 0;
        music.PlayDelayed(5.0f);
    }

    // 게임 완료시 결과창
    public void FinishSong(GameObject result)
    {
        isGameFin = music.isPlaying;

        if (isGameFin.Equals(false))
        {
               //비율 계산 ( 맞춘 개수 / 전체 노드 수 )* 100
            result.transform.GetChild(0).gameObject.SetActive(false);
            result.transform.GetChild(1).gameObject.SetActive(true);
            result.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Data.instance.rhythmAccuracy.ToString("F1") +" 점";
            Data.instance.rhythmAccuracy = (Data.instance.noteCount / Data.instance.numberOfNotes) * 100;
            if (GameObject.Find("UIManager_BeatPinch").GetComponent<UIManager_BeatPinch>().Mode == "measurement" && !isDBInserted)
            {
                string query = "INSERT INTO measurement (date,userID,gameID,accuracy) VALUES ('" + DateTime.Now.ToString("yyyy년 MM-dd일 HH시 mm분 ss초") + "','" + Data.instance.userID + "','" + "03M" + "','" +  Data.instance.rhythmAccuracy + "')";                
                DB.DatabaseInsert(query);
                isDBInserted = true;
            }
        }
    }

    // 플레이씬으로 넘어갈때
    public void SelectSong(string songName)
    {
        this.songName = songName;
        music.Stop();
    }

    // 플레이도중 ESC를 눌렀을때
    public void StopSong(bool isInputESC)
    {
        this.isInputESC = isInputESC;

        music.Stop();

    }

    // 곡선택
    public void MusicSelect(string songName)
    {
        clip = Resources.Load(songName+"/"+songName) as AudioClip;
        music.clip = clip;
        //프리뷰 타임 원위치
        music.timeSamples = 0;
        //프리뷰 타임 조정
        music.timeSamples += music.clip.frequency * previewTime;
    }

    public void SetDifficulty(int diff)
    {
        difficulty = diff;
    }
    public void SetGameSpeed(int speed)
    {
        gameSpeed = speed;
    }
}
