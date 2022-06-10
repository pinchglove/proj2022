using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SphereBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float pivot;
    public float first;

    //List<float> doy = new List<float>();
    void Start()
    {
        pivot = -6.87f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= pivot)
        {
            first = transform.position.y; //sp
            if(Manager.number == 1)
            {
                Manager.globalTimer = 80;
            }
            Manager.value = Mathf.Pow((PlayerBehaviour.yCoord - first), 2); //sp
            Manager.number++; //n sp x
            Manager.doy.Add(Manager.value); //until sigma sp x
            Manager.average = Manager.doy.Average(); //sp x
            Manager.rmse = Mathf.Sqrt(Manager.average); //sp x
            Destroy(gameObject);
        }
        else transform.Translate(-1 * Time.deltaTime, 0, 0);
    }
}
