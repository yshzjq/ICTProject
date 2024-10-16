using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartManagerScripts : MonoBehaviour
{
    // ������Ʈ�� ������Ʈ��
    AudioSource backGroundAudio;
    AudioSource messageAudio;



    // �ܺ��� ������Ʈ�� ������Ʈ��\

    public MeshRenderer meshRenderer;

    public AudioSource effectsAudio;

    public AudioClip notificationSound;
    public AudioClip typingTextSound;

    public CanvasGroup canvasGroup1;
    public GameObject spacestation;
    public Transform playerTransform;


    // public ����
    public float fadeInDelayTime;
    public float fadeOutDelayTime;
    public float backGroundAudioStartDelayTime;
    public float canvas1AppearDelayTime;
    public float canvas1StayTime;
    public float canvas1DisAppearDelayTime;

    public TextMeshProUGUI apperedMessge;
    public List<string> canvas2Messages;

    public float noticeMessageSoundDelayTime;
    public float typingTextDelayTime;
    public float typingTextDeleteDelayTime;
    public float NextTypingTextDelayTime;

    //public ���� Senario1
    void Awake() // �ʱ� ����
    {
        canvasGroup1.alpha = 0f;

        backGroundAudio = GetComponent<AudioSource>();
        messageAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(fadeInStartGame(AppearCanvas1()));
        StartCoroutine(StartBackGroundAudio());
    }

    IEnumerator StartBackGroundAudio()
    {
        yield return new WaitForSeconds(backGroundAudioStartDelayTime);
        backGroundAudio.Play();
    } // ���� ���۽� �ణ�� �ð��� ������ ��� ������ Ʋ���ش�.

    IEnumerator fadeInStartGame(IEnumerator coroutine = null) // ���� ���۽� ������ ȭ�鿡�� ������ ��������
    {
        
        for (int i = 255; i >= 0; i--)
        {
            meshRenderer.material.color = new Color(0f, 0f, 0f, i / 255f);
            yield return new WaitForSeconds(fadeInDelayTime);
            
        }
        if (coroutine != null) StartCoroutine(coroutine);

    }

    IEnumerator fadeOutStartGame() // ���� ���۽� ȭ���� õõ�� �˰Ե�
    {
        for (int i = 0; i <= 255; i++)
        {
            meshRenderer.material.color = new Color(0f, 0f, 0f, i / 255f);
            yield return new WaitForSeconds(fadeOutDelayTime);
        }
    }

    IEnumerator noticeMessageSound() // "�� Text Message" ��� �ؽ�Ʈ�� ���ðŶ�� �޼��� �˸�
    {
        apperedMessge.text = "�� �޼��� ���� ��...";
        for (int i = 0; i < 2; i++)
        {
            messageAudio.PlayOneShot(notificationSound);
            apperedMessge.text = "�� �޼��� ���� ��...";
            yield return new WaitForSeconds(noticeMessageSoundDelayTime);
            apperedMessge.text = "";
            yield return new WaitForSeconds(noticeMessageSoundDelayTime);
        }
    }

    IEnumerator typingText(List<string> messages)
    {
        apperedMessge.text = "";

        foreach(string message in messages)
        {
            foreach (char word in message)
            {

                effectsAudio.PlayOneShot(typingTextSound);
                if(word == '/')
                {
                    apperedMessge.text += '\n';
                    continue;
                }
                apperedMessge.text += word;
                yield return new WaitForSeconds(typingTextDelayTime);
            }

            yield return new WaitForSeconds(NextTypingTextDelayTime);

            for (int i = message.Length - 1; i >= 0; i--)
            {
                apperedMessge.text = apperedMessge.text.Remove(i);
                yield return new WaitForSeconds(typingTextDeleteDelayTime);
            }
        }

        

    } // �޼��� ���

    IEnumerator AppearCanvas1() // ĵ���� ��Ÿ���� �ڷ�ƾ �׸��� ���� �� �غ�
    {
        for (int i = 0; i <= 100; i++)
        {
            canvasGroup1.alpha = i / 100f;
            yield return new WaitForSeconds(canvas1AppearDelayTime);
        }

        yield return new WaitForSeconds(canvas1StayTime);

        for (int i = 100; i >= 0; i--)
        {
            canvasGroup1.alpha = i / 100f;
            yield return new WaitForSeconds(canvas1DisAppearDelayTime);
        }

        yield return StartCoroutine(fadeOutStartGame());

        StartCoroutine(Senario1Process());
    }

    IEnumerator fadeOutSkyBox()
    {
        for(int i = 100;i>=0;i--)
        {
 
            yield return new WaitForSeconds(fadeInDelayTime);
        }
    }

    IEnumerator Senario1Process()
    {
        spacestation.SetActive(true);
        playerTransform.Rotate(0f, 180f, 0f);

        yield return null;
        yield return StartCoroutine(fadeInStartGame());
        yield return StartCoroutine(noticeMessageSound());
        yield return StartCoroutine(typingText(canvas2Messages));
        StartCoroutine(fadeOutStartGame());
        StartCoroutine(fadeOutSkyBox());

        SceneManager.LoadScene("SelectScene");
    }


}
