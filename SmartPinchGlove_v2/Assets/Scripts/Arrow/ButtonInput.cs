using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BNG {
    public class ButtonInput : MonoBehaviour
    {
        public bool istest = false;

        public static ButtonInput instance = null;
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
            istest = false ;

        }


        private void Start()
        {
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
            /*
            if (Input.GetKey(KeyCode.K)){
                istest = true;

            }
            */
            if (( SelectFinger.GetInputData() > Manager.force)) // && InputBridge.Instance.RightGrip < 1 || Input.GetKey(KeyCode.C) ||
            {
                InputBridge.Instance.RightGrip += Time.deltaTime*5;
               //InputBridge.Instance.RightGrip = 0.99f;
            }
            else if (SelectFinger.GetInputData() <= Manager.force) //&& InputBridge.Instance.RightGrip > 0
            {
                InputBridge.Instance.RightGrip -= Time.deltaTime * 5;
                //InputBridge.Instance.RightGrip = 0;
            }
            else
                InputBridge.Instance.RightGrip = 0;
            /*
            if (Input.GetKeyDown(KeyCode.B))
            {
                //Serial.instance.SerialSendingStart();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                //Serial.instance.SerialSendingStop();
            }
            */
            if (InputBridge.Instance.XButton)
            {
                GameMenu._Instance.Pause();
            }
        }
    }
}
