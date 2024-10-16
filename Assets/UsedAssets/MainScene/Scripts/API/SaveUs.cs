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

    // ��ư�� OnPointerDown �� �� ȣ��
    public void startRecording()
    {
        _recording = Microphone.Start(_microphoneID, false, _recordingLengthSec, _recordingHZ);

        StartCoroutine("RecordingTextUI");
    }
    // ��ư�� OnPointerUp �� �� ȣ��
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
                inputText.text = "�ν� ����";
                return;
            }
            // audio clip to byte array
            byte[] byteData = getByteFromAudioClip(_recording);

            // ������ audioclip api ������ ����
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

        // audio clip�� �������� file stream�� �߰�(��ũ ���� �Լ� ����)
        WavUtility.WriteFileHeader(ref stream, fileSize);
        WavUtility.WriteFileFormat(ref stream, audioClip.channels, audioClip.frequency, bitDepth);
        WavUtility.WriteFileData(ref stream, audioClip, bitDepth);

        // stream�� array���·� �ٲ�
        byte[] bytes = stream.ToArray();

        return bytes;
    }
    [Serializable]
    public class VoiceRecognize
    {
        public string text;
    }

    // ����� ���(Kor)�� �� �ڿ� ����
    //

    private IEnumerator PostVoice(string url, byte[] data)
    {
        // request ����
        WWWForm form = new WWWForm();
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // ��û ��� ������ �����Ǹ� �ȵǴ� ������ �����Ƿ� ����
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", "42rbyso4wi");// YOUR_CLIENT_ID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", "skgsG3UamS3XWRF1fQSJ9FHcD8yI5YndNdpPDtwT");// YOUR_CLIENT_SECRET);
        request.SetRequestHeader("Content-Type", "application/octet-stream");

        // �ٵ� ó�������� ��ģ Audio Clip data�� �Ǿ���
        request.uploadHandler = new UploadHandlerRaw(data);

        StopCoroutine("RecordingResultTextUI");

        // ��û�� ���� �� response�� ���� ������ ���
        yield return request.SendWebRequest();

        // ���� response�� ����ִٸ� error
        if (request == null)
        {
            inputText.text = "����";
        }
        else
        {
            // json ���·� ���� {"text":"�νİ��"}
            string message = request.downloadHandler.text;
            VoiceRecognize voiceRecognize = JsonUtility.FromJson<VoiceRecognize>(message);

            //Debug.Log("Voice Server responded: " + voiceRecognize.text); //<= Input String => ä��â ���� => ä�����޹�����

            inputText.text = "";
            inputText.text = voiceRecognize.text;
            // Voice Server responded: �νİ��

            sendVoiceButton.SetActive(true);
        }
    }

    IEnumerator RecordingTextUI()
    {
        while (true)
        {
            inputText.text = "������.";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "������..";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "������...";
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator RecordingResultTextUI()
    {
        while (true)
        {
            inputText.text = "�ؽ�Ʈ ������.";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "�ؽ�Ʈ ������..";
            yield return new WaitForSeconds(0.5f);
            inputText.text = "�ؽ�Ʈ ������...";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
