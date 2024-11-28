using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TitleManager : MonoBehaviour
{
    private GameObject loginPopup; // �α��� �˾�â

    private TextMeshProUGUI textMessage; // �˸�

    private Button loginBtn; // �α��� ��ư
    private Button signUpBtn; // ȸ������ ��ư
    private Button okBtn; // �α��� Ȯ�� ��ư 
    private Button cancelBtn; // �α��� ��� ��ư

    private InputField inputFieldID; // ȸ������ ID �Է�
    private InputField inputFieldPW; // ȸ������ PW �Է�

    private GameObject obj;


    private void Awake()
    {
        loginPopup = GameObject.Find("LoginPopup");

        textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();

        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        signUpBtn = GameObject.Find("SignUpBtn").GetComponent<Button>();
        okBtn = GameObject.Find("OKBtn").GetComponent<Button>();
        cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();


        inputFieldID = GameObject.Find("InputFieldID").GetComponent<InputField>();
        inputFieldPW = GameObject.Find("InputFieldPW").GetComponent<InputField>();

        loginBtn.onClick.AddListener(OnClick_LoginBtn);
        okBtn.onClick.AddListener(OnClick_OKBtn);
        cancelBtn.onClick.AddListener(OnClick_CancelBtn);

    }


    // �޽��� ���� InputField ���� �ʱ�ȭ
    protected void ResetUI(params Image[] images)
    {
        // �ʱ�ȭ
        textMessage.text = string.Empty;

        for (int i = 0; i< images.Length; i++)
        {
            images[i].color = Color.white;
        }  
    }

    // message�� �Ű������� ����
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
    }

    // �Է� ������ �ִ� inputfield�� ���� ���� �� ���� �޽��� ���
    protected void GuideForIncorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }
    
    // �ʵ尪�� ����ִ��� Ȯ��(image:�ʵ�, field:����, result:���)
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"\"{result}\" �ʵ带 ä���ּ���.");
            return true;
        }

        return false;
    }

    public void OnClick_LoginBtn()
    {
        Debug.Log("123");
        LeanTween.scale(loginPopup, Vector3.one, 0.4f).setEase(LeanTweenType.clamp);
    }

    public void OnClick_OKBtn()
    {
        LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }

    public void OnClick_CancelBtn()
    {
        LeanTween.scale(loginPopup, Vector3.zero, 0.4f).setEase(LeanTweenType.clamp);
    }


}
