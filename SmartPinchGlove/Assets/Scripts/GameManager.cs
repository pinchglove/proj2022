using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
