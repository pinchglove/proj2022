using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public static InputField WIT;
    public InputField weightText;
    private TouchScreenKeyboard overlayKeyboard;
    public string inputText = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InputText(Text text)
    {
        WIT.text = weightText.text;
        text.text = weightText.text;
    }
}
