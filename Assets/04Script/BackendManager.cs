using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class BackendManager : MonoBehaviour
{

    private void Awake()
    {
        // Backend.AsyncPoll 호출을 위해 오브젝트 파괴 불가
        DontDestroyOnLoad(gameObject);

        // 서버 초기화
        BackendSetup();
        
    }
    
    private void Update()
    {
        // 서버의 비동기 메소드 호출(콜백 함수 폴링)을 위해 작성
        if (Backend.IsInitialized)
        {
            
        }
    }


    private void BackendSetup()
    {



        var bro = Backend.Initialize(); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }
    }

    public void Test()
    {
        BackendLogin.Instance.CustomSignUp("user1", "1234");
    }
}
