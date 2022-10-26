using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public ParticleSystem thinStars;
    Material[] machine_Renderer;
    // Start is called before the first frame update
    void Start()
    {
        machine_Renderer = transform.GetChild(0).GetComponent<MeshRenderer>().materials;
        if (machine_Renderer[1])
        {
            machine_Renderer[1].SetFloat("_Position", 0f);
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            Debug.Log("콜리젼");
            thinStars.Play();
            StartCoroutine(showPowerScore());
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            //Debug.Log("트리거");
            thinStars.Play();
            StartCoroutine(showPowerScore());
        }
    }

    IEnumerator showPowerScore()
    {
        if (machine_Renderer[1])
        {
            yield return new WaitForSeconds(0.5f);
            float tmp = 0f;
            machine_Renderer[1].SetFloat("_Position", 0f);
            while ( tmp <= PinchStrength.pinch_Max / 6000f)
            {
                tmp += Time.deltaTime; 
                machine_Renderer[1].SetFloat("_Position", tmp);
                yield return null;
            }
        }
        yield return new WaitForSeconds(5f);
        machine_Renderer[1].SetFloat("_Position", 0f);
    }
}
