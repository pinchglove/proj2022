using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    GameObject Red;
    GameObject Blue;
    GameObject Green;
    GameObject Yellow;

    // Start is called before the first frame update
    void Start()
    {
        Red = GameObject.Find("Red_IndexF").gameObject;
        Red.SetActive(false);
        Blue = GameObject.Find("Blue_IndexF").gameObject;
        Blue.SetActive(false);
        Green = GameObject.Find("Green_IndexF").gameObject;
        Green.SetActive(false);
        Yellow = GameObject.Find("Yellow_IndexF").gameObject;
        Yellow.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Inputdata.index_F > 10)
        {
            Red.SetActive(true);
        }
        else
        {
            Red.SetActive(false);
        }

        if (Inputdata.mid_F > 10)
        {
            Blue.SetActive(true);
        }
        else
        {
            Blue.SetActive(false);
        }

        if (Inputdata.ring_F > 10)
        {
            Green.SetActive(true);
        }
        else
        {
            Green.SetActive(false);
        }

        if (Inputdata.little_F > 10)
        {
            Yellow.SetActive(true);
        }
        else
        {
            Yellow.SetActive(false);
        }
    }
    IEnumerator ActiveButton(GameObject gameObject)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
        yield return null;
    }

}
