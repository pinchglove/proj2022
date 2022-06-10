using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RMSE : MonoBehaviour
{
    // Start is called before the first frame update
    public float first;
    public float value;
    public int number;
    public float average;
    public float last;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RMSE = (1/n(sigma)((실제값-예측값)^2)) ^ 1/2 ->y 좌표를 가지고
        //RMSEInit();
    }
    public void RMSEInit()
    {
        first = transform.position.y;
        value = Mathf.Pow((PlayerBehaviour.yCoord - first), 2);
        number++; //n
        //doy.Add(value); //until sigma
        //average = doy.Average();
        last = Mathf.Sqrt(average);
    }
}
