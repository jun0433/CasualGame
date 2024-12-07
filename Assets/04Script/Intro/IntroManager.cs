using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{

    [SerializeField]
    private Progress progress;

    [SerializeField]
    private SceneName nextScene;

    private void Awake()
    {
        SystemSetup();
    }

    private void SystemSetup()
    { 
        // 활성화 되지 않은 상태에서도 게임이 계속 진행
        Application.runInBackground = true;

        // 해상도 설정 (QHD 2560*1440, 16:9)

        int width = (int)(Screen.height * 16/9);
        int height = Screen.height;
        Screen.SetResolution(width, height, true);

        // 화면이 꺼지지 않도록 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 로딩 애니메이션 시작, 재생 완료시 OnAfterProgress 실행
        progress.Play(OnAfterProgress);

    }

    private void OnAfterProgress()
    {
        GameManager.Inst.AsyncLoadingNextScene(nextScene);
    }
}
