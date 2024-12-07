[System.Serializable]
public class UserGameData
{
    public int level;   // 플레이어 레벨
    public float exp;   // 플레이어 경험치
    public int gold;    // 무료 재화
    public int jewel;   // 유료 재화
    public int heart;   // 게임 플레이 소모 재화

    public void Reset()
    {
        level = 1;
        exp = 0;
        gold = 0;
        jewel = 0;
        heart = 30;
    }

}
