using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{   
    //산타
    public int maxPower_average;    // 최대 힘 변수
    public double risingTime;        // 상승 시간
    public double releaseTime;       // 하강 시간

    //풍선
    public float tapCount;      //탭 개수 세주는 변수로 Hz구할 때 나누기 해줘야해서 float, DB에서 문제가 없다며 int로 해줘도됨
    public float avarIdle;      //탭사이간격 평균값
    public float meanIdle;      //탭사이간격 표춘편차값
    public float tapHertz;      //tapCount/15(스크립트 상에는 별도 변수 지정 안함)

    //트래킹
    public float trackingFreq;      //fq측정 빈도
    public float trackMaxForce;     //최대 힘
    public float rmseValue;         //제곱근 오차

    //활쏘기
    public int arrowScore;      //점수
    public int arrowWeight;     //화살 무게
    public float arrowTime;     //초기 시간

    //리듬게임
    public float numberOfNotes;        //노트 전체 개수
    public float noteCount;          //맞춘 노트 개수
    public float rhythmAccuracy;    //정확도

    //박스앤블록
    public int boxCount; // 박스 옮긴 갯수

    //회원정보
    public bool isLogedin;
    
    public string userID;
    public string userName;
    public string userGender;
    public int userAge; 
    public string date;
    

    public static Data instance = null;
    //singletone
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
