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
        string inputText  = inputField.text; //<= STT => ä��â => ä��â �� string <=
        // ä�� �Է�ĭ�� ���ڿ� �Է�
        int findIndex = -1;
        for(int index = 0; index < checkStringList.Count; index++)
        {
            if(inputText.Contains(checkStringList[index])== true)
            {// ����Ʈ�ȿ��ִ� �ܾ �Է��� �ؽ�Ʈ�� ���ԵǾ� �ִٸ�?
                findIndex = index; // �ش� �ε��� ����
                break;
            }
        }

        if(findIndex != -1)
        {   //�ε����� -1 �� �ƴϸ�
            emotiImage.sprite = emotSpriteList[findIndex];
            // ã�� �ε��� ������ ����Ʈ�� �־� �̹����� ��ȯ�� �̹��� ����
        }

        Debug.Log(inputText);
    }
    public void InputSTT(string inputSTTValue)// ��Ҹ��� ���� ����� ���ڿ�
    {
        string inputText = inputSTTValue; //<= STT => ä��â => ä��â �� string <=
        int findIndex = -1;
        for (int index = 0; index < checkStringList.Count; index++)
        {// ����Ʈ���� �ܾ���� ��Ҹ��� �Է¹��� string�� ���Ե� ���
            if (inputText.Contains(checkStringList[index]) == true)
            {
                findIndex = index; // list�� �ε����� ����
                break; 
            }
        }

        if (findIndex != -1)
        {
            emotiImage.sprite = emotSpriteList[findIndex];//����� �ε����� �̹��� ����
        }

        Debug.Log(inputText);
    }
}
