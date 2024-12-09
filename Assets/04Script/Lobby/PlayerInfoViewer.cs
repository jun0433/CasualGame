using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfoViewer : Singleton<PlayerInfoViewer>
{
    [SerializeField]
    private TextMeshProUGUI nicknameText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private Slider expSlider;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI jewelText;
    [SerializeField]
    private TextMeshProUGUI heartText;

    private void Awake()
    {
        BackendGameData.Inst.onGameDataLoadEvent.AddListener(UpdateGameData);
        Debug.Log("test2");
    }

    public void UpdateNickname()
    {
        // �г����� ������ gamer_id�� ���, �г����� ������ �г��� ���
        nicknameText.text = UserInfo.Data.nickname == null ? 
                            UserInfo.Data.gamerId : UserInfo.Data.nickname;
    }

    public void UpdateGameData()
    {
        levelText.text = $"{BackendGameData.Inst.UserGameData.level}";
        // �ִ� ����ġ �ӽ÷� 100 ����
        expSlider.value = BackendGameData.Inst.UserGameData.exp / 100;
        goldText.text = $"{BackendGameData.Inst.UserGameData.gold}";
        jewelText.text = $"{BackendGameData.Inst.UserGameData.jewel}";
        heartText.text = $"{BackendGameData.Inst.UserGameData.heart} / 30";
    }
}
