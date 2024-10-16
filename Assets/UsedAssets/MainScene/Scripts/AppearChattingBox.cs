using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;


public class AppearChattingBox : MonoBehaviour
{
    public XRBaseController xrcon;

    public GameObject chattingBox;

    bool sw = false; // 하는 모드
    bool fix = false;

    void FixedUpdate()
    {
        if(xrcon.uiPressInteractionState.value >= 1f && fix == false && sw == false)
        {
            fix = true;
        }
        else if(xrcon.uiPressInteractionState.value <=0f && fix == true && sw == false)
        {
            sw = true;
        }
        else if(xrcon.uiPressInteractionState.value >= 1f && fix == true && sw == true)
        {
            fix = false;
        }
        else if (xrcon.uiPressInteractionState.value <= 0f && fix == false && sw == true)
        {
            sw = false;
        }

        


        chattingBox.SetActive(fix);
    }
}
