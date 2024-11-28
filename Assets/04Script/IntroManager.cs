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
        // Ȱ��ȭ ���� ���� ���¿����� ������ ��� ����
        Application.runInBackground = true;

        // �ػ� ���� (QHD 2560*1440, 16:9)

        int width = (int)(Screen.height * 16/9);
        int height = Screen.height;
        Screen.SetResolution(width, height, true);

        // ȭ���� ������ �ʵ��� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // �ε� �ִϸ��̼� ����, ��� �Ϸ�� OnAfterProgress ����
        progress.Play(OnAfterProgress);

    }

    private void OnAfterProgress()
    {
        GameManager.Inst.AsyncLoadingNextScene(nextScene);
    }
}
