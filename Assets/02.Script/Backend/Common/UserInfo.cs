using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    // 유저의 정보를 불러오는데 성공했을 경우, 호출할 이벤트 클래스 정의
    [System.Serializable]
    public class UserInfoEvent : UnityEngine.Events.UnityEvent { }

    public UserInfoEvent onUserInfoEvent = new UserInfoEvent();

    // 현제 로그인 한 유저의 정보를 저장
    private static UserInfoData userData = new UserInfoData();

    public static UserInfoData UserData => userData;

    public void GetUserInfoFromBackend()
    {
        var bro = Backend.BMember.GetUserInfo();

        if (bro.IsSuccess() == false)
        {
            userData.Reset();
            return;
        }

        // Json 데이터 피싱 성공 시
        try
        {
            // data변수에 json 데이터들을 저장
            JsonData json = bro.GetReturnValuetoJSON()["row"];

            userData.gamerId = json["gamerId"].ToString();
            userData.countryCode = json["countryCode"]?.ToString();
            userData.nickname = json["nickname"]?.ToString();
            userData.inDate = json["inDate"]?.ToString();
            userData.emailForFindPassword = json["emailForFindPassword"]?.ToString();
            userData.subscriptionType = json["subscriptionType"]?.ToString();
            userData.federationId = json["federationId"]?.ToString();
        }
        // Json 데이터 피싱 실패시
        catch(System.Exception e)
        {
            // 유저 정보를 기본상태로 설정
            userData.Reset();

            // try-catch 에러 내용 출력
            Debug.LogError(e);
        }

        // 정보를 불러오기 완료한 경우, onUserInfoEvent에 포함되어 있는 메소드들을 계속해서 불러낸다.
        onUserInfoEvent?.Invoke();
    }
}

public class UserInfoData
{
    public string gamerId;                  // 우저의 gamerId
    public string countryCode;              // 국가코드, 없으면 null
    public string nickname;                 // 닉네임, 설정 안했으면 null
    public string inDate;                   // 유저의 inData
    public string emailForFindPassword;     // 이메일 주소, 설정 안했으면 null;
    public string subscriptionType;         // 커스텀, 패더레이션 타입(연동인가?)
    public string federationId;             // 구글, 애플, 페이스북 패더레이션 ID, 커스텀 계정은 null;


    // 대부분의 정보를 null로 바꾼다.
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
