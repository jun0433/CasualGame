using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    private GameObject createPopup; // ȸ������ �˾�â

    private Button signInBtn; // �α��� ��ư
    private Button signUpBtn; // ȸ������ ��ư

    private InputField inputFieldID; // ȸ������ ID �Է�
    private InputField inputFieldPW; // ȸ������ PW �Է�

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
