using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Photon;
using Photon.Pun;
public class SenarioProcessReadyScript : MonoBehaviour
{

    //Public �ܺ� ������Ʈ
    public GameObject robot;

    public GameObject InputInfo;
    public GameObject FadeInOutObj;

    public float fadeInDelayTime;
    public float fadeOutDelayTime;

    public float fadeInInfoDelayTime;
    public float fadeOutInfoDelayTime;

    public MeshRenderer mr;

    public CanvasGroup InputInfoAlpha;


    public GameObject LoadingWindow;
    public TextMeshProUGUI LoadingWindowNotice;

    public NetworkManager NetworkManager;

    bool joinRoom;

    public Animator RobotAni;

    public GameObject camera;

    public float cameraSpeed;

    public float cameraMoveDelayTime;

    public GameObject lastfadeout;

    string name;
    int age;
    bool male;

    public void getInfo(string _name,int _age,bool _male)
    {
        name = _name;
        age = _age;
        male = _male;
    }

    public void joinRoomtrue()
    {
        joinRoom = true;
    }


    //Private ���� ������Ʈ


    // Public ������ 0�̶� ����Ѵ�.
    public List<float> SenarioDelayTimes;


    // ����
    private void Awake()
    {
        // SetActive(false)

        // SetActive(true)
        FadeInOutObj.SetActive(true);

        InputInfoAlpha.alpha = 0f;
        joinRoom = false;

    }


    //�ó����� ����
    public void Start()
    {
        StartCoroutine(SenarioCouroutine());
    }

    public void NextSenarioCoroutin2Action()
    {
        StartCoroutine(SenarioCouroutine2());
    }

    // �ڷ�ƾ ������
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
    IEnumerator fadeInInputInfo()
    {
        for (int i = 0; i <= 100; i++)
        {
            InputInfoAlpha.alpha = i / 100f;
            yield return new WaitForSeconds(fadeInInfoDelayTime);
        }
    }
    // �ڷ�ƾ ������

    IEnumerator SenarioCouroutine() // �Ѱ� ��Ǫƾ �ó�����(�ڷ�ƾ) ������ ����
    {
        yield return new WaitForSeconds(SenarioDelayTimes[0]);
        yield return StartCoroutine(fadeInStartGame());         // 1. ���� ���۽� ������ ȭ�鿡�� ������ ��������
        yield return new WaitForSeconds(SenarioDelayTimes[1]);
        yield return StartCoroutine(fadeInInputInfo());         // 2. ���� �Է� â�� ������ ����
    }
    

    IEnumerator gotoCamera()
    {
        for(int i = 0;i<=100;i++)
        {
            camera.transform.position = camera.transform.position + camera.transform.forward * cameraSpeed;
            yield return new WaitForSeconds(cameraMoveDelayTime);
        }
    }
    IEnumerator SenarioCouroutine2()
    {
        LoadingWindow.SetActive(true);

        NetworkManager.ServerConnect();

        while (true)
        {
            if (joinRoom == true) break;
            LoadingWindowNotice.text = "�ε���.";
            yield return new WaitForSeconds(0.3f);
            LoadingWindowNotice.text = "�ε���..";
            yield return new WaitForSeconds(0.3f);
            LoadingWindowNotice.text = "�ε���...";
            yield return new WaitForSeconds(0.3f);
        }
        PhotonNetwork.NickName = name;
       

        LoadingWindowNotice.text = "���� �Ϸ�";
        yield return new WaitForSeconds(1f);
        LoadingWindow.SetActive (false);
        
        StartCoroutine(gotoCamera());
        yield return new WaitForSeconds(1f);
        RobotAni.SetBool("bootingRobot", true);
        yield return new WaitForSeconds(3f);

        robot.SetActive(false);
        lastfadeout.SetActive(true);
        yield return new WaitForSeconds(1f);
        NetworkManager.CreateRoom("MainScene");
    }
}
