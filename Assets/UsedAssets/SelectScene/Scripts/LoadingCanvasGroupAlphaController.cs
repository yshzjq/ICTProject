using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvasGroupAlphaController : MonoBehaviour
{
    CanvasGroup cg;

    public float fadeInInfoDelayTime;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0f;
    }

    private void OnEnable()
    {
        StartCoroutine(fadeInalphaControll());
    }

    IEnumerator fadeInalphaControll()
    {
        for (int i = 0; i <= 100; i++)
        {
            cg.alpha = i / 100f;
            yield return new WaitForSeconds(fadeInInfoDelayTime);
        }
    }
}
