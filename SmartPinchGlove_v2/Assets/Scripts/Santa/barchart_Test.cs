using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class barchart_Test : MonoBehaviour
{
    public bool b = true;
    public Image image;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = SelectFinger.GetInputData() / 2000f;
    }
}
