using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] nodes;
    public Transform[] points;
    public float beat = (60/130)*2;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > beat)
        {
            GameObject node = Instantiate(nodes[Random.Range(0, 2)], points[Random.Range(0, 4)]);
            node.transform.localPosition = Vector3.zero;
            node.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
    
}
