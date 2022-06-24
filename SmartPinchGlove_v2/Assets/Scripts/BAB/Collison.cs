using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collison : MonoBehaviour
{
    public int count = 0; //블록 옮긴 갯수
    public List<Transform> TargetTransform ; //블록 위치 받아올 리스트
    private GameObject Block; //블록 위치 고정용 부모 오브젝트
    private GameObject[] BabyBlock; // 블록 위치 고정용 배열
    public GameObject BlockIns; //블록 복제용 부모 오브젝트
    public GameObject[] BabyBlockIns; //블록 복제용 배열

    #region Singleton
    private static Collison Instance;

    public static Collison _Instance
    {
        get { return Instance; }
    }
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public void GameStartBlock()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(BabyBlock[i], TargetTransform[i].position, TargetTransform[i].rotation).transform.parent = BlockIns.transform;
        }
    }

    #region 블록 프리팹 배열 자동 설정 함수 getPreBlock
    public GameObject[] getPreBlock(GameObject parent)
    {
        GameObject[] children = new GameObject[parent.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
    #endregion
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("block"))
        {

            count++;

            StartCoroutine(SpawnBlock(count));
            Destroy(collision.gameObject);//블록이 쌓이면 카운트가 안먹을 때가 있어서 일단 destroy로 함 fadeout효과있나?
            Debug.Log("count : "+count);
            Debug.Log("count%9 : " + count%9);
        }

    }

    IEnumerator SpawnBlock(int count)
    {
        while (true) {
            if (count < 150) {
                if (count%9 == 0)
                {
                    for (int i = 0; i < 9; i++) {
                        Instantiate(BabyBlock[i], TargetTransform[i].position, TargetTransform[i].rotation).transform.parent = BlockIns.transform;
                    }
                }
            }
            break;
        }
        yield return null;
    }
    
    void Start()
    {
        Block = GameObject.FindWithTag("Block"); // 첫 위치 고정용 블록 부모 오브젝트
        BlockIns = GameObject.FindWithTag("Blockins"); // 블록 복제용 부모 오브젝트
        BabyBlock = getPreBlock(Block); //첫 위치 고정용 블록 자식 오브젝트(9개)
        Block.SetActive(false); // 첫 위치 고정용 블록 숨기기

        for (int i = 0; i < 9; i++)
        {
            Instantiate(BabyBlock[i], BabyBlock[i].transform.position, BabyBlock[i].transform.rotation).transform.parent = BlockIns.transform;
            TargetTransform.Add(BabyBlock[i].transform); //첫 9개 위치 고정
        }

        BabyBlockIns = getPreBlock(BlockIns);



    }

    void Update()
    {
        
        

        
    }
}
