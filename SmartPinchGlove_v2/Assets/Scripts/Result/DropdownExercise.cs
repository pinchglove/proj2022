using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownExercise : MonoBehaviour
{
    int i = 0;
    
    public Dropdown dropdown;
    public GameObject result_Panel;
    public GameObject strength;
    public GameObject Inter_tap_interval;
    public GameObject box_count;
    public GameObject tap_frequency;
    public GameObject tracking_error;
    public GameObject tapping_accuracy;

    public void Start()
    {
        result_Panel = GameObject.Find("Result_Panel");
        strength = GameObject.Find("StrengthG");
        Inter_tap_interval= GameObject.Find("IntertapG");
        box_count = GameObject.Find("boxcountG");
        tap_frequency= GameObject.Find("tapFreG");
        tracking_error = GameObject.Find("trackingErrG");
        tapping_accuracy = GameObject.Find("tapAccurG");

        result_Panel.SetActive(true);
        strength.SetActive(false);
        Inter_tap_interval.SetActive(false);
        box_count.SetActive(false);
        tap_frequency.SetActive(false);
        tracking_error.SetActive(false);
        tapping_accuracy.SetActive(false);
    }

    public void OnDropdownEvent(int index)
    {
        i = index;

        switch (i)
        {
            case 0: //통합결과
                //dropdown.value = 0;
                result_Panel.SetActive(true);
                strength.SetActive(false);
                Inter_tap_interval.SetActive(false);
                box_count.SetActive(false);
                tap_frequency.SetActive(false);
                tracking_error.SetActive(false);
                tapping_accuracy.SetActive(false);

                break;
            case 1: //최대힘
                //dropdown.value = 1;
                result_Panel.SetActive(false);
                strength.SetActive(true);
                Inter_tap_interval.SetActive(false);
                box_count.SetActive(false);
                tap_frequency.SetActive(false);
                tracking_error.SetActive(false);
                tapping_accuracy.SetActive(false);

                break;
            case 2: //태핑간 간격
                //dropdown.value = 2;
                result_Panel.SetActive(false);
                strength.SetActive(false);
                Inter_tap_interval.SetActive(true);
                box_count.SetActive(false);
                tap_frequency.SetActive(false);
                tracking_error.SetActive(false);
                tapping_accuracy.SetActive(false);

                break;
            case 3: //박스개수
                //dropdown.value = 3;
                result_Panel.SetActive(false);
                strength.SetActive(false);
                Inter_tap_interval.SetActive(false);
                box_count.SetActive(true);
                tap_frequency.SetActive(false);
                tracking_error.SetActive(false);
                tapping_accuracy.SetActive(false);

                break;
            case 4: //태핑빈도
                //dropdown.value = 4;
                result_Panel.SetActive(false);
                strength.SetActive(false);
                Inter_tap_interval.SetActive(false);
                box_count.SetActive(false);
                tap_frequency.SetActive(true);
                tracking_error.SetActive(false);
                tapping_accuracy.SetActive(false);

                break;
            case 5: //트래킹 오차율
                //dropdown.value = 5;
                result_Panel.SetActive(false);
                strength.SetActive(false);
                Inter_tap_interval.SetActive(true);
                box_count.SetActive(false);
                tap_frequency.SetActive(false);
                tracking_error.SetActive(true);
                tapping_accuracy.SetActive(false);

                break;
            case 6: //태핑 정확도
                //dropdown.value = 6;
                result_Panel.SetActive(false);
                strength.SetActive(false);
                Inter_tap_interval.SetActive(false);
                box_count.SetActive(false);
                tap_frequency.SetActive(false);
                tracking_error.SetActive(false);
                tapping_accuracy.SetActive(true);

                break;
            
        }
    }
}
