using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopup : LoginBase
{
    [SerializeField]
    private GameObject loginPopup; // 로그인 팝업창

    [SerializeField]
    private Button loginPopupBtn; // 로그인 버튼
    [SerializeField]
    private Button loginBtn; // 로그인 확인 버튼
    [SerializeField]
    private Button loginCancelBtn; // 로그인 취소 버튼

    [SerializeField]
    private Image imageID; // ID 필드 색상
    [SerializeField]
    private Image imagePW; // PW 필드 색상

    [SerializeField]
    private TMP_InputField inputFieldID; // 회원가입 ID 입력
    [SerializeField]
    private TMP_InputField inputFieldPW; // 회원가입 PW 입력

    private GameObject obj;


    private void Awake()
    {
        loginPopupBtn.onClick.AddListener(OnClick_LoginPopupBtn);
        loginBtn.onClick.AddListener(OnClick_LoginBtn);
        loginCancelBtn.onClick.AddListener(OnClick_LoginCancelBtn);

    }

    /// <summary>
    /// 로그인 시도 버튼 
    /// </summary>
    public void OnClick_LoginBtn()
    {
        // ID,PW 색상과 내용 초기화
        ResetUI(imageID, imagePW);

        // 필드값 체크
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호"))
        {
            return;
        }

        // 연타하지 못하도록 비활성화
        loginBtn.interactable = false;

        // 서버에 로그인을 요청하는 동안 화면에 출력
        StartCoroutine("LoginProcess");

        // 서버에 로그인 시도
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);

        // LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }



    /// <summary>
    /// 로그인 시도 후 서버로 전달받은 message 기반으로 로직 처리
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    private void ResponseToLogin(string ID, string PW)
    {
        // 서버에 로그인 요청(비동기)
        // 호출동안 loginprocess 계속 실행
        // 호출 완료시 coroutine 중단, callback 함수 실행
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine("LoginProcess");

            // 로그인 성공
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldID.text}님 환영합니다.");
            }
            // 로그인 실패
            else
            {
                // 로그인 버튼 재활성화(다시 시도해야함)
                loginBtn.interactable = true;

                string message = string.Empty;

                // 에러 번호 정보를 불러오고 출력
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:   // 존재하지 않는 아이디, 잘못된 비밀번호
                                // 서버에서 오류 메시지를 가져와 포함된 단어를 검색
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디입니다." : "잘못된 비밀번호 입니다.";
                        break;
                    case 403:   // 유저||디바이스 차단
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다.";
                        break;
                    case 410:   // 탈퇴 진행중
                        message = "탈퇴가 진행중입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;

                }

                // Error:401(비밀번호)
                if (message.Contains("비밀번호"))
                {
                    GuideForIncorrectlyEnteredData(imagePW, message);
                }
                else
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
            }
        });
    }

    private IEnumerator LoginProcess()
    {
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;

            SetMessage($"로그인 중입니다... {time:F1}");


            yield return null;
        }
    }

    // 로그인 창 열기
    public void OnClick_LoginPopupBtn()
    {
        LeanTween.scale(loginPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

    // 로그인 창 닫기
    public void OnClick_LoginCancelBtn()
    {
        LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }



}
