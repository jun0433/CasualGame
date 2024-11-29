using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPopup : LoginBase
{
    [SerializeField]
    private GameObject loginPopup; // �α��� �˾�â

    [SerializeField]
    private Button loginPopupBtn; // �α��� ��ư
    [SerializeField]
    private Button loginBtn; // �α��� Ȯ�� ��ư
    [SerializeField]
    private Button loginCancelBtn; // �α��� ��� ��ư

    [SerializeField]
    private Image imageID; // ID �ʵ� ����
    [SerializeField]
    private Image imagePW; // PW �ʵ� ����

    [SerializeField]
    private TMP_InputField inputFieldID; // ȸ������ ID �Է�
    [SerializeField]
    private TMP_InputField inputFieldPW; // ȸ������ PW �Է�

    private GameObject obj;


    private void Awake()
    {
        loginPopupBtn.onClick.AddListener(OnClick_LoginPopupBtn);
        loginBtn.onClick.AddListener(OnClick_LoginBtn);
        loginCancelBtn.onClick.AddListener(OnClick_LoginCancelBtn);

    }

    /// <summary>
    /// �α��� �õ� ��ư 
    /// </summary>
    public void OnClick_LoginBtn()
    {
        // ID,PW ����� ���� �ʱ�ȭ
        ResetUI(imageID, imagePW);

        // �ʵ尪 üũ
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "��й�ȣ"))
        {
            return;
        }

        // ��Ÿ���� ���ϵ��� ��Ȱ��ȭ
        loginBtn.interactable = false;

        // ������ �α����� ��û�ϴ� ���� ȭ�鿡 ���
        StartCoroutine("LoginProcess");

        // ������ �α��� �õ�
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);

        // LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }



    /// <summary>
    /// �α��� �õ� �� ������ ���޹��� message ������� ���� ó��
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    private void ResponseToLogin(string ID, string PW)
    {
        // ������ �α��� ��û(�񵿱�)
        // ȣ�⵿�� loginprocess ��� ����
        // ȣ�� �Ϸ�� coroutine �ߴ�, callback �Լ� ����
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            StopCoroutine("LoginProcess");

            // �α��� ����
            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldID.text}�� ȯ���մϴ�.");
            }
            // �α��� ����
            else
            {
                // �α��� ��ư ��Ȱ��ȭ(�ٽ� �õ��ؾ���)
                loginBtn.interactable = true;

                string message = string.Empty;

                // ���� ��ȣ ������ �ҷ����� ���
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:   // �������� �ʴ� ���̵�, �߸��� ��й�ȣ
                                // �������� ���� �޽����� ������ ���Ե� �ܾ �˻�
                        message = callback.GetMessage().Contains("customId") ? "�������� �ʴ� ���̵��Դϴ�." : "�߸��� ��й�ȣ �Դϴ�.";
                        break;
                    case 403:   // ����||����̽� ����
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410:   // Ż�� ������
                        message = "Ż�� �������Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;

                }

                // Error:401(��й�ȣ)
                if (message.Contains("��й�ȣ"))
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

            SetMessage($"�α��� ���Դϴ�... {time:F1}");


            yield return null;
        }
    }

    // �α��� â ����
    public void OnClick_LoginPopupBtn()
    {
        LeanTween.scale(loginPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

    // �α��� â �ݱ�
    public void OnClick_LoginCancelBtn()
    {
        LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }



}
