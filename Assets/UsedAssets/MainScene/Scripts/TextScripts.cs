using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScripts : MonoBehaviour
{
    public int index;

    chattingBoxScripts cbs;

    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();    
        cbs = FindObjectOfType<chattingBoxScripts>();
    }

    private void FixedUpdate()
    {
        setText();
    }

    public void setText()
    {
        text.text = cbs.chatScripts[index];
    }
}
