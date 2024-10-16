using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private List<string> checkStringList = default;
    [SerializeField] private Image emotiImage;
    [SerializeField] private List<Sprite> emotSpriteList;
    [SerializeField] private InputField inputField;

    public void OnClickInputText()
    {
        string inputText  = inputField.text; //<= STT => 채팅창 => 채팅창 들어갈 string <=
        // 채팅 입력칸에 문자열 입력
        int findIndex = -1;
        for(int index = 0; index < checkStringList.Count; index++)
        {
            if(inputText.Contains(checkStringList[index])== true)
            {// 리스트안에있는 단어가 입력한 텍스트에 포함되어 있다면?
                findIndex = index; // 해당 인덱스 저장
                break;
            }
        }

        if(findIndex != -1)
        {   //인덱스가 -1 이 아니면
            emotiImage.sprite = emotSpriteList[findIndex];
            // 찾은 인덱스 값으로 리스트에 넣어 이미지로 반환후 이미지 변경
        }

        Debug.Log(inputText);
    }
    public void InputSTT(string inputSTTValue)// 목소리로 부터 추출된 문자열
    {
        string inputText = inputSTTValue; //<= STT => 채팅창 => 채팅창 들어갈 string <=
        int findIndex = -1;
        for (int index = 0; index < checkStringList.Count; index++)
        {// 리스트들의 단어들이 목소리로 입력받은 string에 포함될 경우
            if (inputText.Contains(checkStringList[index]) == true)
            {
                findIndex = index; // list의 인덱스를 저장
                break; 
            }
        }

        if (findIndex != -1)
        {
            emotiImage.sprite = emotSpriteList[findIndex];//저장된 인덱스로 이미지 변경
        }

        Debug.Log(inputText);
    }
}
