using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;


public class TitleManager : MonoBehaviour
{
    private GameObject loginPopup; // 로그인 팝업창
    private GameObject signUpPopup; // 회원가입 팝업창

    private Button loginBtn; // 로그인 버튼
    private Button signUpBtn; // 회원가입 버튼


    private GameObject obj;


    private void Awake()
    {
        loginPopup = GameObject.Find("LoginPopup");
        signUpPopup = GameObject.Find("SignUpPopup");

        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        signUpBtn = GameObject.Find("SignUpBtn").GetComponent<Button>();


        loginBtn.onClick.AddListener(OnClick_LoginBtn);

    }

    // 로그인 창 열기
    public void OnClick_LoginBtn()
    {
        LeanTween.scale(loginPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

}
