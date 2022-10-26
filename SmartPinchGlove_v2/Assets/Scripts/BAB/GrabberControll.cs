using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG
{
    public class GrabberControll : MonoBehaviour
    {

        public static bool isPinch; //VR컨트롤러 오른손 그립 잠그기 

        public static GrabberControll instance = null;
        private void Awake()
        {
            if (instance == null)
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
        }
        // Update is called once per frame
        void Update()
        {
            if ((Input.GetKey(KeyCode.C) || SelectFinger.GetInputData() > 10) && InputBridge.Instance.RightGrip < 1)
            {
                InputBridge.Instance.RightGrip += Time.deltaTime * 5;
            }
            else if (InputBridge.Instance.RightGrip > 0)
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