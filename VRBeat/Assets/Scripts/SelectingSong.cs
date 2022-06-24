using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectingSong : MonoBehaviour
{
    public SongManager songManager;
    UIManager_BeatPinch uiManager;

    Text text;
    private void Start()
    {
        songManager = GameObject.Find("SongSelect").GetComponent<SongManager>();
        uiManager = GameObject.Find("UIManager_BeatPinch").GetComponent<UIManager_BeatPinch>(); ;
        text = this.gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    public void Click()
    {
        songManager.songName = text.text;
        songManager.MusicSelect(songManager.songName);
        uiManager.SelectSong_Panel.SetActive(false);
        uiManager.Difficulty_Panel.SetActive(true);
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("Beat");
    }
}
