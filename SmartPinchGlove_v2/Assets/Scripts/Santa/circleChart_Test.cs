using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class circleChart_Test : MonoBehaviour
{
    public bool b = true;
    public Image image;
    public Text progress;
    
    // Update is called once per frame
    void Update()
    {
        if (b)
        {
            image.fillAmount = SelectFinger.GetInputData() / 2000f;
            
            if (progress)
            {
                progress.text = (int)(image.fillAmount * 2000f) + "";
            }
        }
    }
}
