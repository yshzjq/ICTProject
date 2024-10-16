using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setColorButtons : MonoBehaviour
{
    public Image image;
    Color existingColor;

    private void Start()
    {
        existingColor  = image.color;
    }

    public void setExitColor()
    {
        image.color = existingColor;
    }

    public void setGrayColor()
    {
        image.color = new Color(0.173f, 0.173f, 0.173f, 0.882f);
    }
}
