using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;


/// <summary>
/// ������ ������ ���� ������ ����
/// </summary>
public class BackendGameData : MonoBehaviour
{
    private static BackendGameData instance = null; // �ܺο��� ���ǵ� �޼ҵ带 ���� ȣ���� �� �ֵ��� �ν��Ͻ� ���� ����
    public static BackendGameData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new BackendGameData();
            }

            return instance;
        }
    }

    private UserGameData userGameData = new UserGameData(); // ������ ���� �� ���� �������� ������ �ִ� ����
    public UserGameData UserGameData => userGameData;


    private string gameDataRowInDate = string.Empty; // �ҷ��� ���� ������ ���� ���� ����

    /// <summary>
    /// �ܼ� ���̺� ���ο� ���� ���� �߰�
    /// </summary>
    public void GameDataInsert()
    {
        // ���� ���� �ʱⰪ ����
        userGameData.Reset();

        // ���̺� �߰��� �����ͷ� ����
        Param param = new Param()
        {
            {"level", userGameData.level },
            {"exp", userGameData.exp },
            {"gold",userGameData.gold },
            {"jewel", userGameData.jewel},
            {"heart", userGameData.heart }
        };

        // ù ��° �Ű������� �ܼ��� "���� ���� ����" �ǿ� ������ ���̺� �̸�
        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            // ���� ���� �߰��� �������� ��
            if (callback.IsSuccess())
            {
                // ���� ������ ������
                gameDataRowInDate = callback.GetInDate();

                Debug.Log($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
            // �������� ��
            else
            {
                Debug.LogError($"���� ���� ������ ���Կ� �����߽��ϴ�. : {callback}");
            }
        });
    }

}
