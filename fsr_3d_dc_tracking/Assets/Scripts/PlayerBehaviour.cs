using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float ySpeed = 0.5f;
    public float yTarget = 1.04f;
    public static float yCoord;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, ySpeed, 0);
        ySpeed = Mathf.Lerp(ySpeed, yTarget, 0.025f);
        yCoord = transform.position.y;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ySpeed = 0.05f;
        }
    }
}
