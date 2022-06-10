using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendItem : MonoBehaviour
{
    // Start is called before the first frame update
    public float sendTimer = 0;
    public float frequency = 8f;
    public GameObject floor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sendTimer -= Time.deltaTime;
        if(sendTimer <= 0)
        {
            Instantiate(floor, new Vector3(10, -0.8f, 0), transform.rotation);
            sendTimer = frequency;
        }
    }
}
