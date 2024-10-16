using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.UI;
using System;
using TMPro;


public class VRChat : MonoBehaviour
{
    chattingBoxScripts cbs;
    public Image backUI;
    public TextScripts[] ts;

    public TextMeshProUGUI chat;

    public Text name;

    PhotonView pv;

    // Start is called before the first frame update
    void Awake()
    {
        pv = GetComponent<PhotonView>();

        cbs = FindObjectOfType<chattingBoxScripts>();
    }

    public void SendTalk()
    {

        string str = pv.Owner.NickName + ": " + chat.text;
        pv.RPC("AddTalkRPC", RpcTarget.All, str);
        chat.text = "";

    }

    [PunRPC]
    public void AddTalkRPC(string str)
    {
        while (cbs.chatScripts.Count >= 5)
        {
            cbs.chatScripts.RemoveAt(0);
        }
        cbs.chatScripts.Add(str);
    }
}
