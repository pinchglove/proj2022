using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_BeatPinch : MonoBehaviour
{
    UIManager_BeatPinch uiManager;
    SongManager songManager;
    GeneratorNote generatorNote;
    GameObject _Mode;
    GameObject _3D;
    GameObject _VR;
    GameObject canvas;

    private void Awake()
    {
        uiManager = GameObject.Find("UIManager_BeatPinch").GetComponent<UIManager_BeatPinch>();
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
        generatorNote = GameObject.Find("GeneratorNote").GetComponent<GeneratorNote>();

        _Mode = GameObject.Find("Mode").gameObject;
        _3D = _Mode.transform.GetChild(0).gameObject;
        _VR = _Mode.transform.GetChild(1).gameObject;

        _3D.SetActive(false);
        _VR.SetActive(false);

        if(uiManager.Mode == "measurement")
        {
            _3D.SetActive(true);
            generatorNote.myoffset = 1.6f;
        }
        else if(uiManager.Mode == "training")
        {
            _VR.SetActive(true);
            generatorNote.myoffset = 0.55f;
        }
        //노트카운터 초기화
        Data.instance.noteCount = 0;

        canvas = GameObject.Find("Canvas");     //캔버스 찾기
        canvas.transform.GetChild(1).gameObject.SetActive(false);   //결과패널 처음에 끄기
    }
    private void FixedUpdate()
    {
        songManager.FinishSong(canvas);
    }

    public void SceneChangeToMain_Beat()
    {
        //dondestory했던 애들 파괴 ???
        Destroy(uiManager.gameObject);
        Destroy(songManager.gameObject);
        SceneManager.LoadScene("Main_Beat");
    }
    public void SceneChangeToMain()
    {
        //dondestory했던 애들 파괴 ???
        Destroy(GameObject.Find("UIManager_BeatPinch").gameObject);
        Destroy(GameObject.Find("SongSelect").gameObject);
        SceneManager.LoadScene("Main");
    }
    public void Pause_Beat(bool isPaused)
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            songManager.GetComponent<AudioSource>().Pause();
        }
        else
        {
            songManager.GetComponent<AudioSource>().UnPause();
            Time.timeScale = 1f;
        }
    }
}
