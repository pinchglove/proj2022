using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SphereBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float pivot;
    public float first;

    //List<float> doy = new List<float>();
    void Start()
    {
        pivot = -6.87f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= pivot)
        {
            first = transform.position.y; //sp
            Debug.Log("y:" + first);
            if (Manager_Tracking.number == 1)
            {
                Manager_Tracking.globalTimer = 80;
            }
            Manager_Tracking.value = Mathf.Pow((PlayerBehaviour.yCoord - first) / 7.488597f, 2); //(플레이어의 y축 위치 - 오브젝트 위치)제곱 -> 제곱오차
            Debug.Log("플레이어-오브젝트/7 : " + PlayerBehaviour.yCoord);
            Debug.Log("제곱오차: " + Manager_Tracking.value);
            Debug.Log("플레이어위치:" + PlayerBehaviour.yCoord);
            Manager_Tracking.number++; //n sp x
            Manager_Tracking.doy.Add(Manager_Tracking.value); //until sigma sp x doy=빈 리스트
            Manager_Tracking.average = Manager_Tracking.doy.Average(); //sp x //평균제곱오차
            Manager_Tracking.rmse = Mathf.Sqrt(Manager_Tracking.average); //sp x //평균제곱근오차
            Debug.Log("rmse: " + Manager_Tracking.rmse);
            Destroy(gameObject);
        }
        else transform.Translate(-1 * Time.deltaTime, 0, 0);
    }
}
