using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TitleManager : MonoBehaviour
{
    private GameObject loginPopup; // 로그인 팝업창

    private TextMeshProUGUI textMessage; // 알림

    private Button loginBtn; // 로그인 버튼
    private Button signUpBtn; // 회원가입 버튼
    private Button okBtn; // 로그인 확인 버튼 
    private Button cancelBtn; // 로그인 취소 버튼

    private InputField inputFieldID; // 회원가입 ID 입력
    private InputField inputFieldPW; // 회원가입 PW 입력

    private GameObject obj;


    private void Awake()
    {
        loginPopup = GameObject.Find("LoginPopup");

        textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();

        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        signUpBtn = GameObject.Find("SignUpBtn").GetComponent<Button>();
        okBtn = GameObject.Find("OKBtn").GetComponent<Button>();
        cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();


        inputFieldID = GameObject.Find("InputFieldID").GetComponent<InputField>();
        inputFieldPW = GameObject.Find("InputFieldPW").GetComponent<InputField>();

        loginBtn.onClick.AddListener(OnClick_LoginBtn);
        okBtn.onClick.AddListener(OnClick_OKBtn);
        cancelBtn.onClick.AddListener(OnClick_CancelBtn);

    }


    // 메시지 내용 InputField 색상 초기화
    protected void ResetUI(params Image[] images)
    {
        // 초기화
        textMessage.text = string.Empty;

        for (int i = 0; i< images.Length; i++)
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
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"\"{result}\" 필드를 채워주세요.");
            return true;
        }

        return false;
    }

    public void OnClick_LoginBtn()
    {
        Debug.Log("123");
        LeanTween.scale(loginPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

    public void OnClick_OKBtn()
    {
        LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }

    public void OnClick_CancelBtn()
    {
        LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }


}
