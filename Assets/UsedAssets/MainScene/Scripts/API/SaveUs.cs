using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SaveUs : MonoBehaviour
{
    public GameObject sendVoiceButton;

    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingLengthSec = 15;
    private int _recordingHZ = 22050;
    private string url = "https://naveropenapi.apigw.ntruss.com/recog/v1/stt?lang=Kor";

    public TextMeshProUGUI inputText;

    private void Start()
    {
        _microphoneID = Microphone.devices[0];

    }

    // 버튼을 OnPointerDown 할 때 호출
    public void startRecording()
    {
        _recording = Microphone.Start(_microphoneID, false, _recordingLengthSec, _recordingHZ);

        StartCoroutine("RecordingTextUI");
    }
    // 버튼을 OnPointerUp 할 때 호출
    public void stopRecording()
    {
        StopCoroutine("RecordingTextUI");
        StartCoroutine("RecordingResultTextUI");

        if (Microphone.IsRecording(_microphoneID))
        {
            Microphone.End(_microphoneID);
            
            if (_recording == null)
            {
                StopCoroutine("RecordingResultTextUI");
                inputText.text = "인식 실패";
                return;
            }
            // audio clip to byte array
            byte[] byteData = getByteFromAudioClip(_recording);

            // 녹음된 audioclip api 서버로 보냄
            StartCoroutine(PostVoice(url, byteData));
        }
        return;
    }
    private byte[] getByteFromAudioClip(AudioClip audioClip)
    {
        MemoryStream stream = new MemoryStream();
        const int headerSize = 44;
        ushort bitDepth = 16;

        int fileSize = audioClip.samples * WavUtility.BlockSize_16Bit + headerSize;

        // audio clip의 정보들을 file stream에 추가(링크 참고 함수 선언)
        WavUtility.WriteFileHeader(ref stream, fileSize);
        WavUtility.WriteFileFormat(ref stream, audioClip.channels, audioClip.frequency, bitDepth);
        WavUtility.WriteFileData(ref stream, audioClip, bitDepth);

        // stream을 array형태로 바꿈
        byte[] bytes = stream.ToArray();

        return bytes;
    }
    [Serializable]
    public class VoiceRecognize
    {
        public string text;
    }

    // 사용할 언어(Kor)를 맨 뒤에 붙임
    //

    private IEnumerator PostVoice(string url, byte[] data)
    {
        // request 생성
        WWWForm form = new WWWForm();
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // 요청 헤더 설정은 공개되면 안되는 내용이 있으므로 생략
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", "42rbyso4wi");// YOUR_CLIENT_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", "skgsG3UamS3XWRF1fQSJ9FHcD8yI5YndNdpPDtwT");// YOUR_CLIENT_SECRET);
        request.SetRequestHeader("Content-Type", "application/octet-stream");

        // 바디에 처리과정을 거친 Audio Clip data를 실어줌
        request.uploadHandler = new UploadHandlerRaw(data);

        StopCoroutine("RecordingResultTextUI");

        // 요청을 보낸 후 response를 받을 때까지 대기
        yield return request.SendWebRequest();

        // 만약 response가 비어있다면 error
        if (request == null)
        {
            inputText.text = "오류";
        }
        else
        {
            // json 형태로 받음 {"text":"인식결과"}
            string message = request.downloadHandler.text;
            VoiceRecognize voiceRecognize = JsonUtility.FromJson<VoiceRecognize>(message);

            //Debug.Log("Voice Server responded: " + voiceRecognize.text); //<= Input String => 채팅창 전달 => 채팅전달받은걸

            inputText.text = "";
            inputText.text = voiceRecognize.text;
            // Voice Server responded: 인식결과

            sendVoiceButton.SetActive(true);
        }
    }

    IEnumerator RecordingTextUI()
    {
        while (true)
        {
            inputText.text = "녹음중.";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "녹음중..";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "녹음중...";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RecordingResultTextUI()
    {
        while (true)
        {
            inputText.text = "텍스트 추출중.";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "텍스트 추출중..";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "텍스트 추출중...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
