using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using UnityEngine.SceneManagement;


public class SignUp : MonoBehaviour
{
    //public GameObject Title_Panel;
    //public GameObject Login_Panel;
    public GameObject Login;
    public GameObject Signup;
    //public Text LoginMessage;

    InputField LoginID_IF;
    InputField LoginPW_IF;

    InputField SignUpID_IF;
    InputField SignUpPW_IF;
    InputField Name_IF;
    InputField Birth_IF;
    Dropdown Gender_Dropdown;

    bool isIDOverlaped = true;

    private void Start()
    {
        DB.DBConnectionCheck();
        Setting_Objects();
    } 

    void Setting_Objects()
    {
        LoginID_IF = Login.transform.Find("ID_InputField").GetComponent<InputField>();
        LoginPW_IF = Login.transform.Find("Password_InputField").GetComponent<InputField>();

        SignUpID_IF = Signup.transform.Find("ID").GetComponent<InputField>();
        SignUpPW_IF = Signup.transform.Find("Password").GetComponent<InputField>();
        Name_IF = Signup.transform.Find("Name").GetComponent<InputField>();
        Birth_IF = Signup.transform.Find("Birth").GetComponent<InputField>();
        Gender_Dropdown = Signup.transform.Find("Gender").GetComponent<Dropdown>();
    }

    #region 회원가입
    //회원가입 저장
    public void SignUpSave()
    {
        string gender;
        int birth;
        bool isDataOK = CheckData();
        if (!isDataOK)
        {
            Signup.transform.Find("PopUp").gameObject.SetActive(true);
            Signup.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "입력 내용을 확인해주세요.";
            Debug.Log("회원가입 실패");
        }
        else
        {
            gender = Gender_Dropdown.captionText.text;
            birth = int.Parse(Birth_IF.text);
            string query = "INSERT INTO userInfo VALUES ('" + SignUpID_IF.text + "','" + SignUpPW_IF.text + "','" + Name_IF.text + "','" + birth + "','" + gender + "','" + DateTime.Now.ToString("yyyy년 MM dd일") + "')";
            DB.DatabaseInsert(query);
            Signup.transform.Find("Complete").gameObject.SetActive(true);
        }
    }

    //회원가입 정보 입력창 확인
    bool CheckData()
    {
        if (!isIDOverlaped
           && SignUpID_IF.text.Length > 0
           && Name_IF.text.Length > 0
           && Birth_IF.text.Length == 8
           && Gender_Dropdown.captionText.text != "성별"
           )
        {
            return true;
        }
        else
            return false;
    }

    //회원가입시 ID중복 확인
    public void IDOverlapCheck()
    {
        //string query = "SELECT * FROM userInfo WHERE userID = '" + SignUpID_IF.text + "'";
        string query = "SELECT userID FROM userInfo";
        DB.DataBaseRead(query);
        //Debug.Log(query);
        if(SignUpID_IF.text.Length > 0)
        {
            isIDOverlaped = false; //중복되지 않음을 가정함.
            while (DB.dataReader.Read())
            {
                if(SignUpID_IF.text == DB.dataReader.GetString(0))
                {
                    isIDOverlaped = true;   //중복 발견시 체크
                    Signup.transform.Find("PopUp").gameObject.SetActive(true);
                    Signup.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "중복된 아이디 입니다.";
                    break;
                }
            }
            if(isIDOverlaped == false)  //반복문 이후 중복이 발견되지 않으므로 사용가능 출력
            {
                Signup.transform.Find("PopUp").gameObject.SetActive(true);
                Signup.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "사용 가능한 아이디 입니다.";
            }
        }
        else
        {
            Signup.transform.Find("PopUp").gameObject.SetActive(true);
            Signup.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "아이디를 입력하세요.";
        }
        DB.DBClose();
    }
    #endregion

    // 회원가입 Complete 패널 닫고 -> 로그인 패널 열기
    public void CompleteToLogin()
    {
        Signup.transform.Find("Complete").gameObject.SetActive(false);
        Signup.SetActive(false);
        Login.SetActive(true);
    }

    //아이디 확인
    public void LogIN()
    {
        //bool IDPWCheck = false;
        //string query = "SELECT * FROM userInfo WHERE userID = '" + LoginID_IF.text + "'"; //쿼리로 ID가 걸러짐
        string query = "SELECT userID FROM userInfo";
        DB.DataBaseRead(query);
        Debug.Log(query);

        if(LoginID_IF.text.Length == 0)
        {
            //IDPWCheck = false;
            Login.transform.Find("PopUp").gameObject.SetActive(true);
            Login.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "아이디를 입력하세요.";
        }
        else if(LoginPW_IF.text.Length == 0)
        {
            //IDPWCheck = false;
            Login.transform.Find("PopUp").gameObject.SetActive(true);
            Login.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "비밀번호를 입력하세요.";
        }
        else
        {
            //먼저 ID 검사
            bool IDCheck = false;
            while (DB.dataReader.Read())
            {
                if (LoginID_IF.text == DB.dataReader.GetString(0))
                {
                    IDCheck = true;
                    break;
                }
            }
            if(IDCheck == false)
            {
                Login.transform.Find("PopUp").gameObject.SetActive(true);
                Login.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "잘못된 아이디 입니다.";
            }
            DB.DBClose();

            //비밀번호 검사
            query = "SELECT * FROM userInfo WHERE userID = '" + LoginID_IF.text + "'"; //쿼리로 ID가 걸러짐
            DB.DataBaseRead(query);
            while (DB.dataReader.Read())
            {
                if(LoginPW_IF.text == DB.dataReader.GetString(1))
                {
                    Data.instance.userID = LoginID_IF.text;
                    Data.instance.userName = DB.dataReader.GetString(2);
                    Data.instance.userGender = DB.dataReader.GetString(4);
                    Data.instance.userAge = DateTime.Now.Year - (DB.dataReader.GetInt32(3)/10000) + 1;
                    Login.transform.Find("Complete").gameObject.SetActive(true);
                    Data.instance.isLogedin = true;
                }
                else
                {
                    Login.transform.Find("PopUp").gameObject.SetActive(true);
                    Login.transform.Find("PopUp").transform.Find("Message").gameObject.GetComponent<Text>().text = "잘못된 비밀번호 입니다.";
                }
            }
        }
        DB.DBClose();
        //return IDPWCheck;
    }

    //로그인
    public void InitializeLoginText()
    {
        LoginID_IF.text = "";
        LoginPW_IF.text = "";
    }

    public void InitializeSignUpText()
    {
        SignUpID_IF.text = "";
        SignUpPW_IF.text = "";
        Name_IF.text = "";
        Birth_IF.text = "";
        Gender_Dropdown.captionText.text = "성별";
    }
}
