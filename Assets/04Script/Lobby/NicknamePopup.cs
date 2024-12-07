using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using UnityEditor.PackageManager.Requests;

public class NicknamePopup : LoginBase
{
    [System.Serializable]
    public class NicknameEvent : UnityEngine.Events.UnityEvent { }
    public NicknameEvent onNicknameEvent = new NicknameEvent();

    [SerializeField]
    private GameObject changePopup;

    [SerializeField]
    private Image imageNickname;
    [SerializeField]
    private TMP_InputField inputFieldNickname;

    [SerializeField]
    private Button changeBtn;
    [SerializeField]
    private Button popupBtn;
    [SerializeField]
    private Button cancelBtn;


    private void Awake()
    {
        popupBtn.onClick.AddListener(OnClick_NickNamePopupBtn);
        cancelBtn.onClick.AddListener(OnClick_NickNameCancelBtn);
        changeBtn.onClick.AddListener(OnClick_ChangeBtn);
    }

    private void OnEnable()
    {
        // 닉네임 변경에 실패해 에러 메시지를 출력한 상태에서
        // 닉네임 변경 팝업을 닫았다가 열 수 있기 때문에 상태를 초기화
        ResetUI(imageNickname);
        SetMessage("닉네임을 입력하세요");

    }

    public void OnClick_ChangeBtn()
    {
        // 매개변수로 입력한 InputField UI와 Message 내용 초기화
        ResetUI(imageNickname);

        // 필드 값이 비어있는지 체크
        if(IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "닉네임"))
        {
            return;
        }

        // 닉네임 변경 버튼 비활성화
        changeBtn.interactable = false;
        SetMessage("닉네임 변경중입니다...");

        // 닉네임 변경 시도
        UpdateNickname();
    }

    private void UpdateNickname()
    {
        // 닉네임 설정
        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {
            // 닉네임 변경 버튼 활성화
            changeBtn.interactable = true;

            // 닉네임 변경 성공
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldNickname.text}(으)로 닉네임이 변경되었습니다.");

                // 닉네임 변경에 성공했을 때 onNicknameEvent에 등록되어 있는 메소드 호출
                onNicknameEvent?.Invoke();
            }
            // 닉네임 변경 실패
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400: // 빈 닉네임 혹은 string.empty, 20자 이상의 닉네임, 닉네임 앞/뒤에 공백이 있는 경우 
                        message = "닉네임이 비어있거나, 앞/뒤에 공백이 있습니다.";
                        break;
                    case 409: // 이미 중복된 닉네임이 있는 경우
                        message = "이미 존재하는 닉네임입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                
                GuideForIncorrectlyEnteredData(imageNickname, message);
            }
            
        });
    }

    // 닉네임 변경 팝업창 켜기
    public void OnClick_NickNamePopupBtn()
    {
        //InputField UI의 색상과 매개변수 초기화
        OnEnable();
        LeanTween.scale(changePopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }


    // 닉네임 변경 팝업창 끄기
    public void OnClick_NickNameCancelBtn()
    {
        LeanTween.scale(changePopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
