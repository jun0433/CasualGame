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
        Backend.BMember.FindCustomID(inputFieldEmail.text, callbck =>
        {
            // ���̵� ã�� ��ư Ȱ��ȭ
            findIDBtn.interactable = true;

            if (callbck.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text} �ּҷ� ������ �߼��Ͽ����ϴ�.");
            }
            // ���� �߼� ����
            else
            {
                string message = string.Empty;

                switch (int.Parse(callbck.GetStatusCode()))
                {
                    case 404: // �ش� �̸����� ���̸Ӱ� �������� ����
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429: // 24�ð� �̳��� 5ȸ �̻� ���� �̸��� ������ ���̵�/��й�ȣ ���� ã�� �õ�
                        message = "24�ð� �̳��� 5ȸ �̻� ���̵�/��й�ȣ ã�⸦ �õ��߽��ϴ�.";
                        break;
                    default:
                        // statusCode 400 => ������Ʈ �� Ư�����ڰ� �߰��� ���(�ȳ� ���� �̹߼� �� ���� �߻�
                        message = callbck.GetMessage();
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
}
