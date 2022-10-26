using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExercise : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject result_Panel;
    public GameObject strength;
    public GameObject Inter_tap_interval;
    public GameObject box_count;
    public GameObject tap_frequency;
    public GameObject tracking_error;
    public GameObject tapping_accuracy;
    //public GameObject release_duration_time;
    //public GameObject rising_time;
    //public GameObject Inter_tap_interval;
    //public GameObject finger_contact_time;
    //public GameObject number_of_tap_or_frequency;
    //public GameObject error_of_target_force_and_observed_force;
    //public GameObject tapping_accuracy;
 

    public void Start()
    {
        result_Panel = GameObject.Find("Result_Panel");
        strength = GameObject.Find("StrengthG");
        Inter_tap_interval = GameObject.Find("IntertapG");
        box_count = GameObject.Find("boxcountG");
        tap_frequency = GameObject.Find("tapFreG");
        tracking_error = GameObject.Find("trackingErrG");
        tapping_accuracy = GameObject.Find("tapAccurG");
        SetUserInfo();
    }

    public void btnStrength()
    {
        dropdown.value = 1;
        result_Panel.SetActive(false);
        strength.SetActive(true);
    }

    public void btnInterval()
    {
        dropdown.value = 2;
        result_Panel.SetActive(false);
        Inter_tap_interval.SetActive(true);
    }


    public void btnTapFreq()
    {
        dropdown.value = 4;
        result_Panel.SetActive(false);
        tap_frequency.SetActive(true);
    }

    public void btnTracking()
    {
        dropdown.value = 5;
        result_Panel.SetActive(false);
        tracking_error.SetActive(true);
    }

    public void btnTapAccur()
    {
        dropdown.value = 6;
        result_Panel.SetActive(false);
        tapping_accuracy.SetActive(true);
    }

    public void backTo통합결과()
    {
        dropdown.value = 0;
        result_Panel.SetActive(true);
        strength.SetActive(false);
        Inter_tap_interval.SetActive(false);
        box_count.SetActive(false);
        tap_frequency.SetActive(false);
        tracking_error.SetActive(false);
        tapping_accuracy.SetActive(false);

    }
    public void SetUserInfo()
    {
        string gender = "";
        if(Data.instance.userGender == "남자")
        {
            gender = "Male";
        }
        else if (Data.instance.userGender == "여자")
        {
            gender = "Female";
        }
        result_Panel.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;
        strength.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;
        Inter_tap_interval.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;
        box_count.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;
        tap_frequency.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;
        tracking_error.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;
        tapping_accuracy.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("gender").GetComponent<Text>().text = "Gender : " + gender;

        result_Panel.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();
        strength.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();
        Inter_tap_interval.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();
        box_count.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();
        tap_frequency.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();
        tracking_error.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();
        tapping_accuracy.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("age").GetComponent<Text>().text = "Age : " + Data.instance.userAge.ToString();

        result_Panel.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
        strength.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
        Inter_tap_interval.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
        box_count.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
        tap_frequency.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
        tracking_error.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
        tapping_accuracy.transform.Find("상단바").transform.Find("Profile").transform.Find("profile").transform.Find("name").GetComponent<Text>().text = "Name : " + Data.instance.userName;
    }
}