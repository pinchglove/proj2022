using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float ySpeed = 0.5f;
    public float yTarget = 1.04f;
    public static float yCoord;
    public float maxForce; //Y축의 최대 높이는 최대 힘의 50%에 도달 시에 도달하도록 최대높이->7.5
    public static float mf;
    Vector3 startPosition;
    //가정: 1000
    // 1000(MAXFORCE) / 2 / 7.5 -> ySpeed
    void Start()
    {
        startPosition = transform.position;
        //maxForce = 300;
        maxForce = mf;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);
        yCoord = transform.position.y;
        if (transform.position.y < startPosition.y)
            transform.position = startPosition;
        if (Input.GetKey(KeyCode.Space))
        {
            ySpeed = 0.03f;
        }
        /*
        //Serial
        transform.Translate(0, Time.deltaTime, 0);
        speed = 7.5 / maxForce;
        //transform.position.y = speed * Inputdata.index_F;
        transform.Translate(0, speed * Inputdata.index_F, 0);
        */
    }
}
