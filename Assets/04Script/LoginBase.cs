using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginBase : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMessage; // 알림


    private void Awake()
    {
        //textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
    }

    // 메시지 내용 InputField 색상 초기화
    protected void ResetUI(params Image[] images)
    {
        // 초기화
        textMessage.text = string.Empty;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }

    // message를 매개변수로 변경
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }

    // 입력 오류가 있는 inputfield의 색상 변경 및 오류 메시지 출력
    protected void GuideForIncorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }
    
    // 필드값이 비어있는지 확인(image:필드, field:내용, result:출력)
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        // 필드값이 비어있을 경우 GuideForIncorrectlyEnteredData 함수 호출
        // Trim을 이용하여 공백 제거
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"{result}를 입력해주세요.");
            return true;
        }

        return false;
    }
}
