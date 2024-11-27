using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    private GameObject createPopup; // 회원가입 팝업창

    private Button signInBtn; // 로그인 버튼
    private Button signUpBtn; // 회원가입 버튼

    private InputField inputFieldID; // 회원가입 ID 입력
    private InputField inputFieldPW; // 회원가입 PW 입력

    private GameObject obj;

    private void Awake()
    {
        createPopup = GameObject.Find("CreatePopup");
        
        signInBtn = GameObject.Find("SingInBtn").GetComponent<Button>();
        signUpBtn = GameObject.Find("SingUpBtn").GetComponent<Button>();

        inputFieldID = GameObject.Find("InputFieldID").GetComponent<InputField>();
        inputFieldPW = GameObject.Find("InputFieldPW").GetComponent<InputField>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
