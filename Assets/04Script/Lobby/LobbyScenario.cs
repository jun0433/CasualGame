using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScenario : MonoBehaviour
{
    // �޼ҵ� ȣ���� ���� ���� ����
    [SerializeField]
    private UserInfo user;

    private void Awake()
    {
        // Login�� �� ������ ������ Lobby�� ���
        user.GetUserInfoFromBackend();
    }
}
