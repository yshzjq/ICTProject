using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraNearController : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionProperty Move;
    Camera camera1;

    void Awake()
    {
        camera1 = GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 value = Move.action.ReadValue<Vector2>();

        if (value != Vector2.zero)
        {
            Invoke("upNear", 0.1f);
        }
        else
        {
            downNear();
        }
    }

    void upNear()
    {
        camera1.nearClipPlane = 0.8f;
    }

    void downNear()
    {
        camera1.nearClipPlane = 0.1f;
    }
}
