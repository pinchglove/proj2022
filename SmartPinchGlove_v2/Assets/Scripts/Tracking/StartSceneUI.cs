using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField maxForceInput;
    public InputField timeGap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        HitItem.fq = float.Parse(timeGap.text);
        PlayerBehaviour.mf = float.Parse(maxForceInput.text);
        SceneManager.LoadScene("Tracking");
        //GameSceneUI._Instance.GameStart();
    }

    // 게임 종료
    public void Quit()
    {
        Application.Quit();
    }
}
