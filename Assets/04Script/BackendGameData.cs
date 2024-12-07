using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;


/// <summary>
/// 서버와 연동해 유저 정보를 제어
/// </summary>
public class BackendGameData : MonoBehaviour
{
    private static BackendGameData instance = null; // 외부에서 정의된 메소드를 쉽게 호출할 수 있도록 인스턴스 변수 선언
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
    }

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

}
