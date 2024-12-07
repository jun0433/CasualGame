[System.Serializable]
public class UserGameData
{
    public int level;   // �÷��̾� ����
    public float exp;   // �÷��̾� ����ġ
    public int gold;    // ���� ��ȭ
    public int jewel;   // ���� ��ȭ
    public int heart;   // ���� �÷��� �Ҹ� ��ȭ

    public void Reset()
    {
        level = 1;
        exp = 0;
        gold = 0;
        jewel = 0;
        heart = 30;
    }

}
