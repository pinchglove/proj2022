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
            if(Manager_Tracking.number == 1)
            {
                Manager_Tracking.globalTimer = 80;
            }
            Manager_Tracking.value = Mathf.Pow((PlayerBehaviour.yCoord - first), 2); //sp
            Manager_Tracking.number++; //n sp x
            Manager_Tracking.doy.Add(Manager_Tracking.value); //until sigma sp x
            Manager_Tracking.average = Manager_Tracking.doy.Average(); //sp x
            Manager_Tracking.rmse = Mathf.Sqrt(Manager_Tracking.average); //sp x
            Destroy(gameObject);
        }
        else transform.Translate(-1 * Time.deltaTime, 0, 0);
    }
}
