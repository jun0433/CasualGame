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

    // 씬을 교체할때 사용하는 함수
    public void AsyncLoadingNextScene(SceneName nextScene)
    {
        nextLoadScene = nextScene;

        SceneManager.LoadScene(nextScene.ToString());
    }

    #endregion


}
