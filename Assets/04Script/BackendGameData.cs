using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;


/// <summary>
/// 서버와 연동해 유저 정보를 제어
/// </summary>
public class BackendGameData : Singleton<BackendGameData>
{
    // 유저 정보를 불러왔을 때 호출할 메소드
    [System.Serializable]
    public class GameDataLoadEvent : UnityEngine.Events.UnityEvent { }
    public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();



    /*private static BackendGameData instance = null; // 외부에서 정의된 메소드를 쉽게 호출할 수 있도록 인스턴스 변수 선언
    public static BackendGameData Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new BackendGameData();
            }

            return instance;
        }
    }*/

    private UserGameData userGameData = new UserGameData(); // 유저의 게임 내 정보 변수들을 가지고 있는 변수
    public UserGameData UserGameData => userGameData;


    private string gameDataRowInDate = string.Empty; // 불러온 게임 정보의 고유 값을 저장

    /// <summary>
    /// 콘솔 테이블에 새로운 유저 정보 추가
    /// </summary>
    public void GameDataInsert()
    {
        // 유저 정보 초기값 설정
        userGameData.Reset();

        // 테이블에 추가할 데이터로 가공
        Param param = new Param()
        {
            {"level", userGameData.level },
            {"exp", userGameData.exp },
            {"gold",userGameData.gold },
            {"jewel", userGameData.jewel},
            {"heart", userGameData.heart }
        };

        // 첫 번째 매개변수는 콘솔의 "게임 정보 관리" 탭에 생성한 테이블 이름
        Backend.GameData.Insert("USER_DATA", param, callback =>
        {
            // 게임 정보 추가에 성공했을 때
            if (callback.IsSuccess())
            {
                // 게임 정보의 고유값
                gameDataRowInDate = callback.GetInDate();

                Debug.Log($"게임 정보 데이터 삽입에 성공했습니다. : {callback}");
            }
            // 실패했을 때
            else
            {
                Debug.LogError($"게임 정보 데이터 삽입에 실패했습니다. : {callback}");
            }
        });
    }

    /// <summary>
    /// 뒤끝 콘솔 테이블에서 유저 정보를 불러올 때 호출
    /// </summary>
    public void GameDataLoad()
    {
        // 자기 자신의 데이터를 조건없이 조회, 비동기로 데이터 호출
        Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
        {
            // 계정 정보 불러오기 성공
            if (callback.IsSuccess())
            {
                Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

                // JSON 파싱 성공
                try
                {
                    // 받아온 데이터를 gameDataJson 변수에 Json 형태로 입력
                    LitJson.JsonData gameDataJson = callback.FlattenRows();

                    // 받아온 데이터의 개수가 0이면 데이터가 존재하지 않음
                    if (gameDataJson.Count <= 0)
                    {
                        Debug.LogWarning("데이터가 존재하지 않습니다.");
                    }
                    else
                    {
                        // 불러온 게임 정보의 고유값
                        // 문자열 데이터를 각 데이터에 맞게 파싱한 후 저장
                        gameDataRowInDate = gameDataJson[0]["inDate"].ToString();
                        // 불러온 게임 정보를 userGameData 변수에 저장
                        userGameData.level = int.Parse(gameDataJson[0]["level"].ToString());
                        Debug.Log(userGameData.level);
                        userGameData.exp = float.Parse(gameDataJson[0]["exp"].ToString());
                        userGameData.gold = int.Parse(gameDataJson[0]["gold"].ToString());
                        userGameData.jewel = int.Parse(gameDataJson[0]["jewel"].ToString());
                        userGameData.heart = int.Parse(gameDataJson[0]["heart"].ToString());

                        // 유저정보 호출에 성공했을 때 호출
                        onGameDataLoadEvent?.Invoke();
                    }
                }
                // JSON 데이터 파싱 실패
                catch (System.Exception e)
                {
                    // 유저 정보를 초기값으로 설정
                    userGameData.Reset();
                    // try-catch 에러 출력
                    Debug.LogError(e);
                }
            }
            // 실패했을 때
            else
            {
                Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
            }

        });
    }

}
