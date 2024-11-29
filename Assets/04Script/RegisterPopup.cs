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
    private Button registerBtn; // ȸ������ Ȯ�� ��ư
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

    /// <summary>
    /// ���� ���� ���� �õ� �� ���޹��� message�� ������� ���� ó��
    /// </summary>
    private void CustomSignUp()
    {

    }

    // ȸ������ ��ư
    public void OnClick_RegisterBtn()
    {
        ResetUI(imageID, imagePW, imagePW2, imageEmail);

        if(IsFieldDataEmpty(imageID, registerID.text, "���̵�"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW, registerPW.text, "���̵�"))
        {
            return;
        }
        if (IsFieldDataEmpty(imagePW2, registerPW2.text, "���̵�"))
        {
            return;
        }
        if (IsFieldDataEmpty(imageEmail, registerEmail.text, "���̵�"))
        {
            return;
        }
    }

    // ȸ������ �˾�â �ѱ�
    public void OnClick_RegisterPopupBtn()
    {
        LeanTween.scale(registerPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }
    

    // ȸ������ �˾�â ����
    public void OnClick_RegisterCancelBtn()
    {
        LeanTween.scale(registerPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }
}
