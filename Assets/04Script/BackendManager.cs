using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class BackendManager : MonoBehaviour
{

    private void Awake()
    {
        // Backend.AsyncPoll ȣ���� ���� ������Ʈ �ı� �Ұ�
        DontDestroyOnLoad(gameObject);

        // ���� �ʱ�ȭ
        BackendSetup();
        
    }
    
    private void Update()
    {
        // ������ �񵿱� �޼ҵ� ȣ��(�ݹ� �Լ� ����)�� ���� �ۼ�
        if (Backend.IsInitialized)
        {
            
        }
    }


    private void BackendSetup()
    {



        var bro = Backend.Initialize(); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
        }
    }

    public void Test()
    {
        BackendLogin.Instance.CustomSignUp("user1", "1234");
    }
}
