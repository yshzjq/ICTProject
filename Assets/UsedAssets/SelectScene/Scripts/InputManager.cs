using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public SenarioProcessReadyScript sprs;

    public GameObject ERRORNOTICE;
    public GameObject CHECKNOTICE;

    public Text nameInfo;
    public Text ageInfo;
    bool selectMale;
    
    string name;
    int age;
    bool male;

    bool isMessage;


    // Start is called before the first frame update
    void Start()
    {
        selectMale = false;
        isMessage = false;
        age = 0;
        name = "";
    }

    public void SetMale()
    {
        selectMale=true;
        male = true;
    }

    public void SetFemale()
    {
        selectMale = true;
        male = false;
    }

    public void yesEvent()
    {
        sprs.getInfo(name, age, male);
    }

    public void InputUserInfomation()
    {
        if(isMessage == false)
        {
            isMessage = true;

            
            name = nameInfo.text;
            if(ageInfo.text != "") age = int.Parse(ageInfo.text);

            if (name == "" || age <= -1 || selectMale == false)
            {
                ERRORNOTICE.SetActive(true);
            }
            else
            {
                CHECKNOTICE.SetActive(true);
            }
        }
    }

    public void SetIsMessageFalse()
    {
        isMessage = false;
    }
}
