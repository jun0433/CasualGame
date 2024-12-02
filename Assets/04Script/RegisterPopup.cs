using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class RegisterPopup : LoginBase
{
    [SerializeField]
    private GameObject registerPopup; // ȸ������ �˾�â

    [SerializeField]
    private Button registerPopupBtn; // ȸ������ �˾�â ��ư
    [SerializeField]
    private Button registerBtn; // ȸ������ ��ư
    [SerializeField]
    private Button registerCancelBtn; // ȸ������ ��� ��ư

    [SerializeField]
    private Image imageID; // ID �ʵ� ����
    [SerializeField]
    private Image imagePW; // PW �ʵ� ����
    [SerializeField]
    private Image imagePW2; // PW Ȯ�� �ʵ� ����
    [SerializeField]
    private Image imageEmail; // Email �ʵ� ����

    [SerializeField]
    private TMP_InputField registerID; // ȸ������ ID �Է�
    [SerializeField]
    private TMP_InputField registerPW; // ȸ������ PW �Է�
    [SerializeField]
    private TMP_InputField registerPW2; // ȸ������ PW ���Է�
    [SerializeField]
    private TMP_InputField registerEmail; // ȸ������ Email �Է�

    private void Awake()
    {
        registerBtn.onClick.AddListener(OnClick_RegisterBtn);
        registerPopupBtn.onClick.AddListener(OnClick_RegisterPopupBtn);
        registerCancelBtn.onClick.AddListener(OnClick_RegisterCancelBtn);
    }

    // ȸ������ ��ư
    public void OnClick_RegisterBtn()
    {
        //InputField UI�� ����� �Ű����� �ʱ�ȭ
        ResetUI(imageID, imagePW, imagePW2, imageEmail);
        
        // �ʵ尪�� ����ִ��� üũ
        if(IsFieldDataEmpty(imageID, registerID.text, "���̵�"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW, registerPW.text, "��й�ȣ"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW2, registerPW2.text, "��й�ȣ Ȯ��"))
        {
            return;
        }
        if (IsFieldDataEmpty(imageEmail, registerEmail.text, "���� �ּ�"))
        {
            return;
        }

        // ��й�ȣ�� ��й�ȣ Ȯ���� ������ üũ
        if (!registerPW.text.Equals(registerPW2.text))
        {
            GuideForIncorrectlyEnteredData(imagePW2, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        // ���� ���� �˻�
        if (!registerEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.");
            return;
        }

        // ���� ���� �ߺ� ����(��ư ��Ȱ��ȭ)
        registerBtn.interactable = false;
        SetMessage("���� �������Դϴ�...");

        // ���� ���� ���� ���� �õ�
        CustomSignUp();
    }

    /// <summary>
    /// ���� ���� �õ� �� ������ ���� message�� ������� ���� ó��
    /// </summary>
    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(registerID.text, registerPW.text, callback =>
        {
            // "ȸ������" ��ư ��ȣ�ۿ� Ȱ��ȭ
            registerBtn.interactable = true;

            // ȸ������ ����
            if (callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(registerEmail.text, callback =>
                {
                    if (callback.IsSuccess())
                    {
                        SetMessage($"ȸ������ ����, {registerID.text}�� ȯ���մϴ�.");
                        LeanTween.scale(registerPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
                    }
                });
            }
            // ȸ������ ����
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 409: // �ߺ��� ID�� �ִ� ���
                        message = "�̹� �����ϴ� ���̵��Դϴ�.";
                        break;
                    case 403: // ���ܴ��� ����̽��� ���
                    case 401: // ������Ʈ ���°� �������� ���
                    case 400: // ����̽� ������ null�� ���
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("���̵�"))
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

    // ȸ������ �˾�â �ѱ�
    public void OnClick_RegisterPopupBtn()
    {
        LeanTween.scale(registerPopup, Vector3.one, 0.2f).setEase(LeanTweenType.clamp);
    }
    

    // ȸ������ �˾�â ����
    public void OnClick_RegisterCancelBtn()
    {
        //InputField UI�� ����� �Ű����� �ʱ�ȭ
        ResetUI(imageID, imagePW, imagePW2, imageEmail);
        LeanTween.scale(registerPopup, Vector3.zero, 0.2f).setEase(LeanTweenType.clamp);
    }
}
