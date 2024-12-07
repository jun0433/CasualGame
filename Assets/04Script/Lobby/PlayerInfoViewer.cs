using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNickname;

    public void UpdateNickname()
    {
        // �г����� ������ gamer_id�� ���, �г����� ������ �г��� ���
        textNickname.text = UserInfo.Data.nickname == null ? 
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;


    }
}
