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
    IEnumerator fadeInStartGame() // ���� ���۽� ������ ȭ�鿡�� ������ ��������
    {
        for (int i = 255; i >= 0; i--)
        {
            mr.material.color = new Color(0f, 0f, 0f, i / 255f);
            yield return new WaitForSeconds(fadeInDelayTime);

        }
    }

    IEnumerator fadeOutStartGame() // ���� ���۽� ȭ���� õõ�� �˰Ե�
    {
        for (int i = 0; i <= 255; i++)
        {
            mr.material.color = new Color(0f, 0f, 0f, i / 255f);
            yield return new WaitForSeconds(fadeOutDelayTime);
        }
    }
}
