using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScripts : MonoBehaviour
{
    public Animator Door;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Door.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Door.SetBool("IsOpen", false);
        }
    }
}
