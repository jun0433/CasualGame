using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginBase : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMessage; // �˸�


    private void Awake()
    {
        //textMessage = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
    }

    // �޽��� ���� InputField ���� �ʱ�ȭ
    protected void ResetUI(params Image[] images)
    {
        // �ʱ�ȭ
        textMessage.text = string.Empty;

        for (int i = 0; i < images.Length; i++)
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
        // �ʵ尪�� ������� ��� GuideForIncorrectlyEnteredData �Լ� ȣ��
        // Trim�� �̿��Ͽ� ���� ����
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"{result}�� �Է����ּ���.");
            return true;
        }

        return false;
    }
}
