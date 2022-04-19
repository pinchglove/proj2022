using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BalloonKill : MonoBehaviour
{
    public GameObject Balloons; // 풍선 부모 오브젝트
    public GameObject[] babyballs; // 풍선 각 객체 배열
    public float[] idleTime; // 탭 사이 간격 체크용 시간 변수 저장용 배열
    public float playTime = 0f; // 전체 게임 시간 체크용 시간 변수
    public bool isKilling = false;  //풍선 터트리기 코루틴 체크용 불 변수
    public int ballcount = 0;   //풍선 배열 인덱스
    public GameObject POP; // 파티클 시스템 부모 오브젝트
    public GameObject[] Pop;  // 파티클 시스템 배열
    public bool isPlaying;
    public AudioClip popSound;
    public AudioSource audioSource;


    public GameObject[] getBalls(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;

    }
    public GameObject[] getPop(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
  

    void Start()
    {
        Balloons = GameObject.FindWithTag("Balloons");
        POP = GameObject.FindWithTag("POP");
        babyballs = getBalls(Balloons);
        playTime = 0f;
        isKilling = false;
        ballcount = 0;
        idleTime = new float[babyballs.Length];
        Pop = getPop(POP);
        isPlaying = true;
        audioSource = this.GetComponent<AudioSource>();
    }
        

    public void Update()
    {
        // 여기부터 아래 손보고있습니당~~
        if (isPlaying) {
            playTime += Time.deltaTime;
            idleTime[ballcount] += Time.deltaTime;
        }  
        

        if(Inputdata.index_F >= 50 && !isKilling)
        //if (Input.GetKeyDown(KeyCode.B) && !isKilling)
        {
            StartCoroutine(killball(ballcount));
            audioSource.PlayOneShot(popSound);
        }
        //if (ballcount < babyballs.Length - 1)
        //{
        //    idleTime[ballcount] += Time.deltaTime;
        //}

    }

    IEnumerator killball(int count)
    {
        isKilling = true;
        Pop[count].GetComponent<ParticleSystem>().Play();
        babyballs[count].gameObject.SetActive(false);

        while (true)
        {
            if (count >= babyballs.Length-1)
            {
                isPlaying = false;
            }


            if (Inputdata.index_F < 50)
            {
                ballcount++;
                isKilling = false;
                break;

            }
           
            
            yield return null;
        }
        yield return null;
    }
  
}
//탭간격(o), (10/전체시간)Hz, 파티클(O)
