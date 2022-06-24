using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float life = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0) Destroy(gameObject);
        else transform.Translate(-1 * Time.deltaTime, 0, 0);
    }
}
