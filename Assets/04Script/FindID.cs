using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class FindID : LoginBase
{

    [SerializeField]
    private GameObject findIDPopup;

    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;

    [SerializeField]
    private Button findIDBtn;

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
        Backend.BMember.FindCustomID(inputFieldEmail.text, callbck =>
        {
            // 아이디 찾기 버튼 활성화
            findIDBtn.interactable = true;

            if (callbck.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} 주소로 메일을 발송하였습니다.");
            }
            // 메일 발송 실패
            else
            {
                string message = string.Empty;

                switch (int.Parse(callbck.GetStatusCode()))
                {
                    case 404: // 해당 이메일의 게이머가 존재하지 않음
                        message = "해당 이메일을 사용하는 사용자가 없습니다.";
                        break;
                    case 429: // 24시간 이내에 5회 이상 같은 이메일 정보로 아이디/비밀번호 정보 찾기 시도
                        message = "24시간 이내에 5회 이상 아이디/비밀번호 찾기를 시도했습니다.";
                        break;
                    default:
                        // statusCode 400 => 프로젝트 명에 특수문자가 추가된 경우(안내 메일 미발송 및 에러 발생
                        message = callbck.GetMessage();
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
}
