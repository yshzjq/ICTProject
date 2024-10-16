using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandAnimation : MonoBehaviour
{
    public InputActionProperty leftPinch;
    public InputActionProperty leftGrip;

    public InputActionProperty rightPinch;
    public InputActionProperty rightGrip;

    public PhotonView PV;

    public Animator animator;

    void Update()
    {

        if(PV.IsMine)
        {
            float leftTriggerValue = leftPinch.action.ReadValue<float>();
            animator.SetFloat("Left Trigger", leftTriggerValue);

            float leftGripValue = leftGrip.action.ReadValue<float>();
            animator.SetFloat("Left Grip", leftGripValue);


            float rightTriggerValue = rightPinch.action.ReadValue<float>();
            animator.SetFloat("Right Trigger", rightTriggerValue);

            float rightGripValue = rightGrip.action.ReadValue<float>();
            animator.SetFloat("Right Grip", rightGripValue);
        }
        

    }
}
