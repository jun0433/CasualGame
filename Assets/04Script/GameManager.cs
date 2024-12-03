using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum SceneName
{
    IntroScene,
    TitleScene,
    LobbyScene,
    //LoadingScene,
}


public class GameManager : Singleton<GameManager>
{


    #region _SceneChange_
    private SceneName nextLoadScene;
    public SceneName NEXTSCENE
    {
        get => nextLoadScene;
    }

    // ���� ��ü�Ҷ� ����ϴ� �Լ�
    public void AsyncLoadingNextScene(SceneName nextScene)
    {
        nextLoadScene = nextScene;

        SceneManager.LoadScene(nextScene.ToString());
    }

    #endregion


}
