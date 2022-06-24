using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItem : MonoBehaviour
{
    public static float fq;
    public float sendTimer = 0;
    public float frequency = 2;
    public float position;
    public static float timer = 0;
    public GameObject myItem;
    public GameObject mainCharacter;
    //bool ascending = true;
    void Start()
    {
        sendTimer = 0;
        frequency = fq;
    }

    // Update is called once per frame
    void Update()
    {
        sendTimer -= Time.deltaTime;
        timer += Time.deltaTime;
        if (sendTimer <= 0 && Manager_Tracking.globalTimer > 0)
        {
            if(Manager_Tracking.globalTimer>77 && Manager_Tracking.number > 0) {
                position = 3.0f;//-1.84f;//Random.Range(0.57f, 5.72f);
                timer = -0.3f;
            } 
            else if(Manager_Tracking.number == 0)
            {
                position = 3.0f;
                timer = -0.3f;
            }
            else
            {
                /*
                if(ascending == true)
                {
                    position += 0.05f;
                }
                else
                {
                    position -= 0.05f;
                }
                if (position > 9)
                {
                    ascending = false;
                }
                else if (position < -5.47)
                {
                    ascending = true;
                }
                */                
                position = Mathf.Sin(timer) * 3.5f + 4.0f;
            }
            transform.position = new Vector3(10, position, 0);
            Instantiate(myItem, transform.position, transform.rotation);
            sendTimer = frequency;
;        }
        if (mainCharacter != null) Time.timeScale = 1;
        else Time.timeScale = 0;
    }
}
