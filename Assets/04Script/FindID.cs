using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class FindID : LoginBase
{

    [SerializeField]
    private GameObject findIDPopup; // 아이디 찾기 팝업창

    [SerializeField]
    private Image imageEmail; // 이메일 입력창

    [SerializeField]
    private TMP_InputField inputFieldEmail; // 이메일 입력필드

    [SerializeField]
    private Button findIDBtn; // 아이디 찾기 버튼
    [SerializeField]
    private Button cancelBtn; // 아이디 찾기 팝업창 닫기 버튼


    private void Awake()
    {
        cancelBtn.onClick.AddListener(OnClick_FindIDCancelBtn);
        findIDBtn.onClick.AddListener(OnClick_FindID);
    }

    public void OnClick_FindID()
    {
        // 매개변수,UI 색상 초기화
        ResetUI(imageEmail);

        // 값이 비어있는지 체크
        if(IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "메일 주소"))
        {
            return;
        }

        // 메일 형식 검사
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.");
            return;
        }

        // 아이디 찾기 버튼 상호작용 비활성화
        findIDBtn.interactable = false;
        SetMessage("메일 발송중입니다..");

        // 서버 아이디 찾기 시도
        FindCustomID();

    }


    private void FindCustomID()
    {
        // 아이디 정보를 이메일로 발송
        Backend.BMember.FindCustomID(inputFieldEmail.text, callback =>
        {
            // 아이디 찾기 버튼 활성화
            findIDBtn.interactable = true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} 주소로 메일을 발송하였습니다.");
            }
            // 메일 발송 실패
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404: // 해당 이메일의 게이머가 존재하지 않음
                        message = "해당 이메일을 사용하는 사용자가 없습니다.";
                        break;
                    case 429: // 24시간 이내에 5회 이상 같은 이메일 정보로 아이디/비밀번호 정보 찾기 시도
                        message = "24시간 이내에 5회 이상 아이디/비밀번호 찾기를 시도했습니다.";
                        break;
                    default:
                        // statusCode 400 => 프로젝트 명에 특수문자가 추가된 경우(안내 메일 미발송 및 에러 발생
                        message = callback.GetMessage();
                        break;
                }

                // 아이디 찾기에 실패할 경우 실패 사유
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

    // 아이디 찾기 팝업창 켜기
    public void OnClick_FindIDPopupBtn()
    {
        LeanTween.scale(findIDPopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }


    // 아이디 찾기 팝업창 끄기
    public void OnClick_FindIDCancelBtn()
    {
        //InputField UI의 색상과 매개변수 초기화
        ResetUI(imageEmail);
        LeanTween.scale(findIDPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
