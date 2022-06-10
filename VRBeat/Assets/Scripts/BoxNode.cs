using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxNode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * transform.forward * 2;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grabber") && Inputdata.index_F > 10)
        {
            Debug.Log("팡");
            Destroy(this.gameObject);
        }
       /* if (other.gameObject.CompareTag("Out"))
        {
            Destroy(this.gameObject);
            Debug.Log("실점");
        }*/
    }
}
