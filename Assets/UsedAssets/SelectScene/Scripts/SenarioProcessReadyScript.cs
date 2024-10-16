using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Photon;
using Photon.Pun;
public class SenarioProcessReadyScript : MonoBehaviour
{

    //Public 외부 컴포넌트
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


    //Private 내부 컴포넌트


    // Public 변수들 0이라도 써야한다.
    public List<float> SenarioDelayTimes;


    // 세팅
    private void Awake()
    {
        // SetActive(false)

        // SetActive(true)
        FadeInOutObj.SetActive(true);

        InputInfoAlpha.alpha = 0f;
        joinRoom = false;

    }


    //시나리오 시작
    public void Start()
    {
        StartCoroutine(SenarioCouroutine());
    }

    public void NextSenarioCoroutin2Action()
    {
        StartCoroutine(SenarioCouroutine2());
    }

    // 코루틴 모음들
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
    IEnumerator fadeInInputInfo()
    {
        for (int i = 0; i <= 100; i++)
        {
            InputInfoAlpha.alpha = i / 100f;
            yield return new WaitForSeconds(fadeInInfoDelayTime);
        }
    }
    // 코루틴 모음들

    IEnumerator SenarioCouroutine() // 총괄 코푸틴 시나리오(코루틴) 순서를 결정
    {
        yield return new WaitForSeconds(SenarioDelayTimes[0]);
        yield return StartCoroutine(fadeInStartGame());         // 1. 게임 시작시 검은색 화면에서 서서히 투명해짐
        yield return new WaitForSeconds(SenarioDelayTimes[1]);
        yield return StartCoroutine(fadeInInputInfo());         // 2. 정보 입력 창이 서서히 떠짐
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
            LoadingWindowNotice.text = "로딩중.";
            yield return new WaitForSeconds(0.3f);
            LoadingWindowNotice.text = "로딩중..";
            yield return new WaitForSeconds(0.3f);
            LoadingWindowNotice.text = "로딩중...";
            yield return new WaitForSeconds(0.3f);
        }
        PhotonNetwork.NickName = name;
       

        LoadingWindowNotice.text = "접속 완료";
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
