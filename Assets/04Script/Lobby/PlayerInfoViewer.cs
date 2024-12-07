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
        // 닉네임이 없으면 gamer_id를 출력, 닉네임이 있으면 닉네임 출력
        textNickname.text = UserInfo.Data.nickname == null ? 
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;


    }
}
