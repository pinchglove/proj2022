using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSliderValueToText : MonoBehaviour
{
    public Slider wSliderUI;
    public Slider tSliderUI;
    private Text weightSliderText;
    private Text timeSliderText;
    public static float WeightSliderValue;
    public static float timeSliderValue;
    public static float time;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        weightSliderText = GetComponent<Text>();
        timeSliderText = GetComponent<Text>();
        ShowWeightSliderValue();
        ShowTimeSliderValue();
    }

    // Update is called once per frame
    public void ShowWeightSliderValue()
    {
        string sliderWeightMessage = "Weight = " + wSliderUI.value;
        WeightSliderValue = wSliderUI.value;
        weightSliderText.text = sliderWeightMessage;
    }
    public void ShowTimeSliderValue()
    {
        string sliderTimeMessage = "Time = " + tSliderUI.value;
        timeSliderValue = tSliderUI.value;
        timeSliderText.text = sliderTimeMessage;
    }
}
