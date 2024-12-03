using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using UnityEditor.VersionControl;
using static BackEnd.Quobject.SocketIoClientDotNet.Parser.Parser.Encoder;

public class FindPW : LoginBase
{
    [SerializeField]
    private GameObject findPWPopup; // 비밀번호 찾기 팝업창

    [SerializeField]
    private Image imageEmail; // 이메일 입력창
    [SerializeField]
    private Image imageID; // 아이디 입력창

    [SerializeField]
    private TMP_InputField inputFieldEmail; // 이메일 입력필드
    [SerializeField]
    private TMP_InputField inputFieldID; // 아이디 입력필드

    [SerializeField]
    private Button findPWBtn; // 비밀번호 찾기 버튼
    [SerializeField]
    private Button cancelBtn; // 비밀번호 찾기 팝업창 닫기 버튼


    private void Awake()
    {
        findPWBtn.onClick.AddListener(OnClick_FindPWBtn);
        cancelBtn.onClick.AddListener(OnClick_FindPWCancelBtn);
    }

    public void OnClick_FindPWBtn()
    {
        // 매개변수로 입력한 InputField의 색상과 message 초기화
        ResetUI(imageID, imageEmail);

        // 필드 값이 비어있는지 체크
        if(IsFieldDataEmpty(imageID, inputFieldID.text, "아이디"))
        {
            return;
        }
        if(IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "이메일"))
        {
            return;
        }

        // 메일 형식 검사
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.");
            return;
        }

        // 중복을 피하기 위한 버튼 비활성화
        findPWBtn.interactable = false;
        SetMessage("메일 발송중입니다.");

        // 서버 비밀번호 찾기 시도
        FindCustomPW();
    }

    private void FindCustomPW()
    {
        // 리셋된 비밀번호를 이메일로 발송
        Backend.BMember.ResetPassword(inputFieldID.text, inputFieldEmail.text, callback =>
        {
            // 비밀번호 찾기 버튼 활성화
            findPWBtn.interactable = true;

            // 메일 발송 성공
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} 주소로 임시 비밀번호를 발송하였습니다.");
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404: // 해당 이메일의 유저가 없는 경우
                        message = "해당 이메일을 사용하는 사용자가 없습니다.";
                        break;
                    case 429: // 24시간 이내에 5회 이상 같은 이메일 정보로 아이디/비밀번호 정보 찾기 시도
                        break;
                    default:
                        // statusCode 400 => 프로젝트 명에 특수문자가 추가된 경우(안내 메일 미발송 및 에러 발생
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("이메일"))
                {
                    GuideForIncorrectlyEnteredData(imageEmail, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });

    }

    // 비밀번호 찾기 팝업창 켜기
    public void OnClick_FindPWPopupBtn()
    {
        LeanTween.scale(findPWPopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }


    // 비밀번호 찾기 팝업창 끄기
    public void OnClick_FindPWCancelBtn()
    {
        //InputField UI의 색상과 매개변수 초기화
        ResetUI(imageEmail);
        LeanTween.scale(findPWPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
