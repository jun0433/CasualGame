using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;

public class FindID : LoginBase
{

    [SerializeField]
    private GameObject findIDPopup; // ���̵� ã�� �˾�â

    [SerializeField]
    private Image imageEmail; // �̸��� �Է�â

    [SerializeField]
    private TMP_InputField inputFieldEmail; // �̸��� �Է��ʵ�

    [SerializeField]
    private Button findIDBtn; // ���̵� ã�� ��ư
    [SerializeField]
    private Button cancelBtn; // ���̵� ã�� �˾�â �ݱ� ��ư


    private void Awake()
    {
        cancelBtn.onClick.AddListener(OnClick_FindIDCancelBtn);
        findIDBtn.onClick.AddListener(OnClick_FindID);
    }

    public void OnClick_FindID()
    {
        // �Ű�����,UI ���� �ʱ�ȭ
        ResetUI(imageEmail);

        // ���� ����ִ��� üũ
        if(IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "���� �ּ�"))
        {
            return;
        }

        // ���� ���� �˻�
        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.");
            return;
        }

        // ���̵� ã�� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
        findIDBtn.interactable = false;
        SetMessage("���� �߼����Դϴ�..");

        // ���� ���̵� ã�� �õ�
        FindCustomID();

    }


    private void FindCustomID()
    {
        // ���̵� ������ �̸��Ϸ� �߼�
        Backend.BMember.FindCustomID(inputFieldEmail.text, callback =>
        {
            // ���̵� ã�� ��ư Ȱ��ȭ
            findIDBtn.interactable = true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} �ּҷ� ������ �߼��Ͽ����ϴ�.");
            }
            // ���� �߼� ����
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404: // �ش� �̸����� ���̸Ӱ� �������� ����
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429: // 24�ð� �̳��� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ���� ã�� �õ�
                        message = "24�ð� �̳��� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��߽��ϴ�.";
                        break;
                    default:
                        // statusCode 400 => ������Ʈ �� Ư�����ڰ� �߰��� ���(�ȳ� ���� �̹߼� �� ���� �߻�
                        message = callback.GetMessage();
                        break;
                }

                // ���̵� ã�⿡ ������ ��� ���� ����
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

    // ���̵� ã�� �˾�â �ѱ�
    public void OnClick_FindIDPopupBtn()
    {
        LeanTween.scale(findIDPopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }


    // ���̵� ã�� �˾�â ����
    public void OnClick_FindIDCancelBtn()
    {
        //InputField UI�� ����� �Ű����� �ʱ�ȭ
        ResetUI(imageEmail);
        LeanTween.scale(findIDPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
