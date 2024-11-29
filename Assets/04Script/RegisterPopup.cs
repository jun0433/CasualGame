using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class RegisterPopup : LoginBase
{
    [SerializeField]
    private GameObject registerPopup; // 회원가입 팝업창

    [SerializeField]
    private Button registerPopupBtn; // 회원가입 팝업창 버튼
    [SerializeField]
    private Button registerBtn; // 회원가입 확인 버튼
    [SerializeField]
    private Button registerCancelBtn; // 회원가입 취소 버튼

    [SerializeField]
    private Image imageID; // ID 필드 색상
    [SerializeField]
    private Image imagePW; // PW 필드 색상
    [SerializeField]
    private Image imagePW2; // PW 확인 필드 색상
    [SerializeField]
    private Image imageEmail; // Email 필드 색상

    [SerializeField]
    private TMP_InputField registerID; // 회원가입 ID 입력
    [SerializeField]
    private TMP_InputField registerPW; // 회원가입 PW 입력
    [SerializeField]
    private TMP_InputField registerPW2; // 회원가입 PW 재입력
    [SerializeField]
    private TMP_InputField registerEmail; // 회원가입 Email 입력

    private void Awake()
    {
        registerBtn.onClick.AddListener(OnClick_RegisterBtn);
        registerPopupBtn.onClick.AddListener(OnClick_RegisterPopupBtn);
        registerCancelBtn.onClick.AddListener(OnClick_RegisterCancelBtn);
    }

    /// <summary>
    /// 서버 계정 생성 시도 후 전달받은 message를 기반으로 로직 처리
    /// </summary>
    private void CustomSignUp()
    {

    }

    // 회원가입 버튼
    public void OnClick_RegisterBtn()
    {
        ResetUI(imageID, imagePW, imagePW2, imageEmail);

        if(IsFieldDataEmpty(imageID, registerID.text, "아이디"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW, registerPW.text, "아이디"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW2, registerPW2.text, "아이디"))
        {
            return;
        }
        if (IsFieldDataEmpty(imageEmail, registerEmail.text, "아이디"))
        {
            return;
        }
    }

    // 회원가입 팝업창 켜기
    public void OnClick_RegisterPopupBtn()
    {
        LeanTween.scale(registerPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }
    

    // 회원가입 팝업창 끄기
    public void OnClick_RegisterCancelBtn()
    {
        LeanTween.scale(registerPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }
}
