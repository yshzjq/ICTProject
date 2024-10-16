using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectScenefadeInOutScripts : MonoBehaviour
{
    MeshRenderer mr;

    public float fadeInDelayTime;
    public float fadeOutDelayTime;
    public void Start()
    {
        mr = GetComponent<MeshRenderer>();

        StartCoroutine(fadeInStartGame());
    }
    IEnumerator fadeInStartGame() // 게임 시작시 검은색 화면에서 서서히 투명해짐
    {
        for (int i = 255; i >= 0; i--)
        {
            mr.material.color = new Color(0f, 0f, 0f, i / 255f);
            yield return new WaitForSeconds(fadeInDelayTime);

        }
    }

    IEnumerator fadeOutStartGame() // 게임 시작시 화면이 천천히 검게됨
    {
        for (int i = 0; i <= 255; i++)
        {
            mr.material.color = new Color(0f, 0f, 0f, i / 255f);
            yield return new WaitForSeconds(fadeOutDelayTime);
        }
    }
}
