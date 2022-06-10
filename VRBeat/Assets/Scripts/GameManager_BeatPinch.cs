using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_BeatPinch : MonoBehaviour
{
    UIManager_BeatPinch uiManager;
    GeneratorNote generatorNote;
    GameObject _3D;
    GameObject _VR;

    private void Awake()
    {
        uiManager = GameObject.Find("UIManager_BeatPinch").GetComponent<UIManager_BeatPinch>();
        generatorNote = GameObject.Find("GeneratorNote").GetComponent<GeneratorNote>();

        _3D = GameObject.Find("3D").gameObject;
        _VR = GameObject.Find("VR").gameObject;

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
    }
    private void Start()
    {
        
    }
}
