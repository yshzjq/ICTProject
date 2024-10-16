using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviourPunCallbacks
{
    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Camera playerCamera;

    public PhotonView PV;
    public ActionBasedContinuousMoveProvider ABC;
    public ActionBasedSnapTurnProvider ABSTP;
    public ActionBasedContinuousTurnProvider ABCTP;

    public List<ActionBasedController> xrcontroller;
    public List<XRRayInteractor> xrRay;

    public XROrigin xrorigin;

    public Transform headConstraint;
    public Vector3 headBodyOffset;
    public float turnSmoothness = 3f;

    private void Start()
    {
        if (PV.IsMine)
        {
            headBodyOffset = transform.position - headConstraint.position;
        }
        else
        {
            playerCamera.enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
            ABSTP.enabled = false;
            ABCTP.enabled = false;
            ABC.enabled = false;
            xrorigin.enabled = false;

            for (int i = 0; i < xrcontroller.Count;i++)
            {
                xrcontroller[i].enabled = false;
                xrRay[i].enabled = false;
            }
        }

        //XRRig rig = FindObjectOfType<XRRig>();

        //head.vrTarget = GameObject.Find("Camer a Offset").transform.Find("Main Camera");
        //leftHand.vrTarget = GameObject.Find("Camera Offset").transform.Find("Left Controller");
        //rightHand.vrTarget = GameObject.Find("Camera Offset").transform.Find("Right Controller");
    }

    private void Update()
    {
        if(PV.IsMine)
        {
            transform.position = headConstraint.position + headBodyOffset;
            transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

            head.Map();
            leftHand.Map();
            rightHand.Map();
        }

    }
}