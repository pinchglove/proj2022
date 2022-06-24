using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float sliderVal)
    {
        audioMixer.SetFloat("masterVol", Mathf.Log10(sliderVal)*20);
    }

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
