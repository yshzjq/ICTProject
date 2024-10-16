using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetName : MonoBehaviour
{
    public Text nameText;

    SetName name;
    PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        if(PV.IsMine)
        {
            nameText.text = PV.Owner.NickName;
            nameText.enabled = false;
        }
        else
        {
            name = GetComponent<SetName>();
            PV = GetComponent<PhotonView>();
            name.nameText.text = name.PV.Owner.NickName;
        }
        
    }
}
