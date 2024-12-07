using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using BackEnd;


public class UserInfo : MonoBehaviour
{
    // 유저 정보를 불러오는데 성공했을 때 호출할 메소드
    [System.Serializable]
    public class UserInfoEvent : UnityEngine.Events.UnityEvent { }
    public UserInfoEvent onUserInfoEvent = new UserInfoEvent();

    private static UserInfoData data = new UserInfoData();
    public static UserInfoData Data => data;
    public void GetUserInfoFromBackend()
    {
        // 현재 로그인한 사용자 정보 불러오기
        Backend.BMember.GetUserInfo(callback =>
        {
            // 정보 불러오기 성공
            if (callback.IsSuccess())
            {
                // JSON 데이터 파싱 성공
                try
                {
                    JsonData json = callback.GetReturnValuetoJSON()["row"];

                    data.gamerId = json["gamerId"].ToString();
                    data.countryCode = json["countryCode"]?.ToString();
                    data.nickname = json["nickname"]?.ToString();
                    data.inDate = json["inDate"].ToString();
                    data.emailForFindPassword = json["emailForFindPassword"]?.ToString();
                    data.subscriptionType = json["subscriptionType"].ToString();
                    data.federationId = json["federationId"]?.ToString();
                }
                // JSON 파싱 실패
                catch (System.Exception e)
                {
                    // 유저 정보를 기본 상태로 설정
                    data.Reset();
                    // try-catch 에러 출력
                    Debug.LogError(e);
                }
            }
            // 정보 불러오기 실패
            else
            {
                // 유저 정보를 기본 상태로 설정
                // 일반적으로 오프라인 상태를 대비해 기본적인 정보를 저장해두고 오프라인일 때 불러서 사용
                data.Reset();
                Debug.LogError(callback.GetMessage());
            }

            // 유저 정보 불러오기에 성공했을 떄 onUserInfoData에 등록되어 있는 이벤트 메소드 호출
            onUserInfoEvent?.Invoke();
        });
    }
}

public class UserInfoData
{
    public string gamerId; // 유저의 GameID;
    public string countryCode; // 국가코드 설정 안했으면 null
    public string nickname; // 닉네임 설저 안했으면 null
    public string inDate; // 유저의 접속 일시
    public string emailForFindPassword; // 계정을 찾기 위한 메일주소
    public string subscriptionType; // 커스텀/페더레이션 타입
    public string federationId; // 구글, 애플, 페이스북 페더레이션 ID, 커스텀은 null;

    public void Reset()
    {
        gamerId = "Offline";
        countryCode = "Unknown";
        nickname = "Noname";
        inDate = string.Empty;
        emailForFindPassword = string.Empty;
        subscriptionType = string.Empty;
        federationId = string.Empty;
    }

}
