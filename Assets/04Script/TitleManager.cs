using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TitleManager : MonoBehaviour
{
    private GameObject createPopup; // 회원가입 팝업창

    private Button signInBtn; // 로그인 버튼
    private Button signUpBtn; // 회원가입 버튼
    private Button OKBtn; // 회원가입 확인 버튼 
    private Button cancelBtn; // 회원가입 취소 버튼

    private InputField inputFieldID; // 회원가입 ID 입력
    private InputField inputFieldPW; // 회원가입 PW 입력

    private GameObject obj;

    private void Awake()
    {
        createPopup = GameObject.Find("CreatePopup");
        
        signInBtn = GameObject.Find("SignInBtn").GetComponent<Button>();
        signUpBtn = GameObject.Find("SignUpBtn").GetComponent<Button>();
        OKBtn = GameObject.Find("OKBtn").GetComponent<Button>();
        cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();


        inputFieldID = GameObject.Find("InputFieldID").GetComponent<InputField>();
        inputFieldPW = GameObject.Find("InputFieldPW").GetComponent<InputField>();

        signInBtn.onClick.AddListener(OnClick_SingUpBtn);
        OKBtn.onClick.AddListener(OnClick_OKBtn);
        cancelBtn.onClick.AddListener(OnClick_CancelBtn);

    }

    public void OnClick_SingUpBtn()
    {
        Debug.Log("123");
        LeanTween.scale(createPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

    public void OnClick_OKBtn()
    {
        LeanTween.scale(createPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }

    public void OnClick_CancelBtn()
    {
        LeanTween.scale(createPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }


}
