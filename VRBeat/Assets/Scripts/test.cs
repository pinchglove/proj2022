using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BNG {
    public class test : MonoBehaviour
    {
        public bool istest;

        public static test instance = null;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                    Destroy(this.gameObject);
            }
        }


        private void Start()
        {
            istest = true;
        }
        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("C 눌렸음");
                InputBridge.Instance.RightGrip = 1;
                istest = true;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("B 눌렸음");
                InputBridge.Instance.RightGrip = 0;
                istest = false;
            }
            */
            if ((Input.GetKey(KeyCode.C)||Inputdata.index_F>200) && InputBridge.Instance.RightGrip < 1)
            {
                InputBridge.Instance.RightGrip += Time.deltaTime*5;
            }
            else if(InputBridge.Instance.RightGrip > 0)
            {
                InputBridge.Instance.RightGrip -= Time.deltaTime * 5;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                Serial.instance.SerialSendingStart();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                Serial.instance.SerialSendingStop();
            }
        }
    }
}
