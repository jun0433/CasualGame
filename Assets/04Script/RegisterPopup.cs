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
    private Button registerBtn; // 회원가입 버튼
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

    // 회원가입 버튼
    public void OnClick_RegisterBtn()
    {
        //InputField UI의 색상과 매개변수 초기화
        ResetUI(imageID, imagePW, imagePW2, imageEmail);
        
        // 필드값이 비어있는지 체크
        if(IsFieldDataEmpty(imageID, registerID.text, "아이디"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW, registerPW.text, "비밀번호"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW2, registerPW2.text, "비밀번호 확인"))
        {
            return;
        }
        if (IsFieldDataEmpty(imageEmail, registerEmail.text, "메일 주소"))
        {
            return;
        }

        // 비밀번호와 비밀번호 확인이 같은지 체크
        if (!registerPW.text.Equals(registerPW2.text))
        {
            GuideForIncorrectlyEnteredData(imagePW2, "비밀번호가 일치하지 않습니다.");
            return;
        }

        // 메일 형식 검사
        if (!registerEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.");
            return;
        }

        // 계정 생성 중복 방지(버튼 비활성화)
        registerBtn.interactable = false;
        SetMessage("계정 생성중입니다...");

        // 서버 연결 계정 생성 시도
        CustomSignUp();
    }

    /// <summary>
    /// 계정 생성 시도 후 서버로 받은 message를 기반으로 로직 처리
    /// </summary>
    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(registerID.text, registerPW.text, callback =>
        {
            // "회원가입" 버튼 상호작용 활성화
            registerBtn.interactable = true;

            // 회원가입 성공
            if (callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(registerEmail.text, callback =>
                {
                    if (callback.IsSuccess())
                    {
                        SetMessage($"회원가입 성공, {registerID.text}님 환영합니다.");
                        LeanTween.scale(registerPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
                    }
                });
            }
            // 회원가입 실패
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 409: // 중복된 ID가 있는 경우
                        message = "이미 존재하는 아이디입니다.";
                        break;
                    case 403: // 차단당한 디바이스일 경우
                    case 401: // 프로젝트 상태가 점검중일 경우
                    case 400: // 디바이스 정보가 null일 경우
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("아이디"))
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }

    // 회원가입 팝업창 켜기
    public void OnClick_RegisterPopupBtn()
    {
        LeanTween.scale(registerPopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }
    

    // 회원가입 팝업창 끄기
    public void OnClick_RegisterCancelBtn()
    {
        //InputField UI의 색상과 매개변수 초기화
        ResetUI(imageID, imagePW, imagePW2, imageEmail);
        LeanTween.scale(registerPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
