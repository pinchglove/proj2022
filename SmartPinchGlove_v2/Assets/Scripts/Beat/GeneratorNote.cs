using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GeneratorNote : MonoBehaviour
{
    //노트 생성 스크립트
    Sync sync;
    Vector3 startPos;
    Sheet sheet;
    UIManager_BeatPinch uiManager;
    float yPositionOfNote = 0;
    public GameObject notePrefab_R;
    public GameObject notePrefab_B;
    public GameObject notePrefab_G;
    public GameObject notePrefab_Y;
    public float myoffset;
    

    // 노트간격
    float noteCorrectRate = 0.001f; // 악보 시간이 밀리세컨드 단위 - 좌표생성을 위한 보정값

    // 노트 및 바 미리 연산
    float notePosY;
    float noteStartPosY;
    float offset;

    public bool isGenFin = false;

    void Start()
    {
        uiManager = GameObject.Find("UIManager_BeatPinch").GetComponent<UIManager_BeatPinch>();
        sync = GameObject.Find("Sync").GetComponent<Sync>();
        sheet = GameObject.Find("Sheet").GetComponent<Sheet>();
        startPos = GameObject.Find("StartPos").GetComponent<Transform>().transform.position;
        notePosY = sync.scrollSpeed;
        noteStartPosY = sync.scrollSpeed * 5.0f;
        offset = sync.offset;
        //myoffset = 0f;//sync.scrollSpeed / 5.0f;
    }

    void Update()
    {
        if (isGenFin.Equals(false))
        {
            GenNote();
            isGenFin = true;
        }
    }

    // 노트생성
    void GenNote()
    {
        if(uiManager.Mode == "training")
        {
            yPositionOfNote = 0.85f;
        }
        else
        {
            yPositionOfNote = 0.3f;
        }

        foreach (float noteTime in sheet.noteList1)
        {
            Instantiate(notePrefab_Y, new Vector3(-0.4f, yPositionOfNote, myoffset + noteStartPosY + offset + notePosY * (noteTime * noteCorrectRate)), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        foreach (float noteTime in sheet.noteList2)
        {
            Instantiate(notePrefab_G, new Vector3(-0.1f, yPositionOfNote, myoffset + noteStartPosY + offset + notePosY * (noteTime * noteCorrectRate)), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        foreach (float noteTime in sheet.noteList3)
        {
            Instantiate(notePrefab_B, new Vector3(0.2f, yPositionOfNote, myoffset + noteStartPosY + offset + notePosY * (noteTime * noteCorrectRate)), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
        foreach (float noteTime in sheet.noteList4)
        {
            Instantiate(notePrefab_R, new Vector3(0.5f, yPositionOfNote, myoffset + noteStartPosY + offset + notePosY * (noteTime * noteCorrectRate)), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
}
