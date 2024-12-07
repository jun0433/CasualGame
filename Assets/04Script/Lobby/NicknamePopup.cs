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
        // �г��� ���濡 ������ ���� �޽����� ����� ���¿���
        // �г��� ���� �˾��� �ݾҴٰ� �� �� �ֱ� ������ ���¸� �ʱ�ȭ
        ResetUI(imageNickname);
        SetMessage("�г����� �Է��ϼ���");

    }

    public void OnClick_ChangeBtn()
    {
        // �Ű������� �Է��� InputField UI�� Message ���� �ʱ�ȭ
        ResetUI(imageNickname);

        // �ʵ� ���� ����ִ��� üũ
        if(IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "�г���"))
        {
            return;
        }

        // �г��� ���� ��ư ��Ȱ��ȭ
        changeBtn.interactable = false;
        SetMessage("�г��� �������Դϴ�...");

        // �г��� ���� �õ�
        UpdateNickname();
    }

    private void UpdateNickname()
    {
        // �г��� ����
        Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
        {
            // �г��� ���� ��ư Ȱ��ȭ
            changeBtn.interactable = true;

            // �г��� ���� ����
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldNickname.text}(��)�� �г����� ����Ǿ����ϴ�.");

                // �г��� ���濡 �������� �� onNicknameEvent�� ��ϵǾ� �ִ� �޼ҵ� ȣ��
                onNicknameEvent?.Invoke();
            }
            // �г��� ���� ����
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400: // �� �г��� Ȥ�� string.empty, 20�� �̻��� �г���, �г��� ��/�ڿ� ������ �ִ� ��� 
                        message = "�г����� ����ְų�, ��/�ڿ� ������ �ֽ��ϴ�.";
                        break;
                    case 409: // �̹� �ߺ��� �г����� �ִ� ���
                        message = "�̹� �����ϴ� �г����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                
                GuideForIncorrectlyEnteredData(imageNickname, message);
            }
            
        });
    }

    // �г��� ���� �˾�â �ѱ�
    public void OnClick_NickNamePopupBtn()
    {
        //InputField UI�� ����� �Ű����� �ʱ�ȭ
        OnEnable();
        LeanTween.scale(changePopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }


    // �г��� ���� �˾�â ����
    public void OnClick_NickNameCancelBtn()
    {
        LeanTween.scale(changePopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
