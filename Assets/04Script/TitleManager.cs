using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;


public class TitleManager : MonoBehaviour
{
    private GameObject loginPopup; // �α��� �˾�â
    private GameObject signUpPopup; // ȸ������ �˾�â

    private Button loginBtn; // �α��� ��ư
    private Button signUpBtn; // ȸ������ ��ư


    private GameObject obj;


    private void Awake()
    {
        loginPopup = GameObject.Find("LoginPopup");
        signUpPopup = GameObject.Find("SignUpPopup");

        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        signUpBtn = GameObject.Find("SignUpBtn").GetComponent<Button>();


        loginBtn.onClick.AddListener(OnClick_LoginBtn);

    }

    // �α��� â ����
    public void OnClick_LoginBtn()
    {
        LeanTween.scale(loginPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

}
