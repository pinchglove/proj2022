﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongManager : MonoBehaviour
{
    public AudioSource music;
    public AudioClip clip;

    public string songName;
    public bool isGameFin;
    public bool isInputESC;

    public int difficulty = 0;              //난이도 0일때 쉬움, 1일때 보통, 2일때 어려움
    public float gameSpeed = 5f;            //게임 스피드(노드 떨어지는 속도)

    int previewTime;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //songName = "Christmas Village";
    }

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        previewTime = 30;
    }

    // 게임 시작시 (5초 지나고 재생)
    public void PlayAudioForPlayScene()
    {
        // 시작점 원위치
        music.timeSamples = 0;
        music.PlayDelayed(5.0f);
    }

    // 게임 완료시 결과창
    public void FinishSong()
    {
        isGameFin = music.isPlaying;

        // 노래가 끝이나고 ESC를 누르지 않아야함
        if (isGameFin.Equals(false) && isInputESC.Equals(false))
            SceneManager.LoadScene("PlayResult");
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
