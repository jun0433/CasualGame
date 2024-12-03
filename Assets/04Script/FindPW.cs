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
    private GameObject findPWPopup; // ��й�ȣ ã�� �˾�â

    [SerializeField]
    private Image imageEmail; // �̸��� �Է�â
    [SerializeField]
    private Image imageID; // ���̵� �Է�â

    [SerializeField]
    private TMP_InputField inputFieldEmail; // �̸��� �Է��ʵ�
    [SerializeField]
    private TMP_InputField inputFieldID; // ���̵� �Է��ʵ�

    [SerializeField]
    private Button findPWBtn; // ��й�ȣ ã�� ��ư
    [SerializeField]
    private Button cancelBtn; // ��й�ȣ ã�� �˾�â �ݱ� ��ư


    private void Awake()
    {
        findPWBtn.onClick.AddListener(OnClick_FindPWBtn);
        cancelBtn.onClick.AddListener(OnClick_FindPWCancelBtn);
    }

    public void OnClick_FindPWBtn()
    {
        // �Ű������� �Է��� InputField�� ����� message �ʱ�ȭ
        ResetUI(imageID, imageEmail);

        // �ʵ� ���� ����ִ��� üũ
        if(IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�"))
        {
            return;
        }
        if(IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "�̸���"))
        {
            return;
        }

        // ���� ���� �˻�
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.");
            return;
        }

        // �ߺ��� ���ϱ� ���� ��ư ��Ȱ��ȭ
        findPWBtn.interactable = false;
        SetMessage("���� �߼����Դϴ�.");

        // ���� ��й�ȣ ã�� �õ�
        FindCustomPW();
    }

    private void FindCustomPW()
    {
        // ���µ� ��й�ȣ�� �̸��Ϸ� �߼�
        Backend.BMember.ResetPassword(inputFieldID.text, inputFieldEmail.text, callback =>
        {
            // ��й�ȣ ã�� ��ư Ȱ��ȭ
            findPWBtn.interactable = true;

            // ���� �߼� ����
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} �ּҷ� �ӽ� ��й�ȣ�� �߼��Ͽ����ϴ�.");
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404: // �ش� �̸����� ������ ���� ���
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429: // 24�ð� �̳��� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ���� ã�� �õ�
                        break;
                    default:
                        // statusCode 400 => ������Ʈ �� Ư�����ڰ� �߰��� ���(�ȳ� ���� �̹߼� �� ���� �߻�
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("�̸���"))
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

    // ��й�ȣ ã�� �˾�â �ѱ�
    public void OnClick_FindPWPopupBtn()
    {
        LeanTween.scale(findPWPopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }


    // ��й�ȣ ã�� �˾�â ����
    public void OnClick_FindPWCancelBtn()
    {
        //InputField UI�� ����� �Ű����� �ʱ�ȭ
        ResetUI(imageEmail);
        LeanTween.scale(findPWPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
