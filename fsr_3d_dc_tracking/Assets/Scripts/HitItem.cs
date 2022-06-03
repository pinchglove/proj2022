using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitItem : MonoBehaviour
{
    public float sendTimer = 1;
    public float frequency = 2;
    public float position;
    public GameObject myItem;
    public GameObject mainCharacter;
    //bool ascending = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if(sendTimer <= 0 && Manager.globalTimer > 0)
        {
            if(Manager.globalTimer>60) {
                position = 3.0f;//-1.84f;//Random.Range(0.57f, 5.72f);
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
                position = Mathf.Sin(Manager.globalTimer) * 3.5f + 4.0f;
            }
            transform.position = new Vector3(10, position, 0);
            Instantiate(myItem, transform.position, transform.rotation);
            sendTimer = frequency;
;        }
        if (mainCharacter != null) Time.timeScale = 1;
        else Time.timeScale = 0;
    }
}
