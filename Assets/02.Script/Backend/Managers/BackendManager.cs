using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using System;

public class BackendManager : MonoBehaviour
{
    private static BackendManager instance;
    public static BackendManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Scene에서 BackendManager 객체를 찾는다.
                instance = FindObjectOfType<BackendManager>();

                // 없다면 새로운 GameObject를 생성하고 BackendManager를 추가한다.
                if (instance == null)
                {
                    GameObject obj = new GameObject("BackendManager");
                    instance = obj.AddComponent<BackendManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


        Backend.InitializeAsync(callback =>
        {
            if(callback.IsSuccess())
            {
                Debug.Log("백엔트 서버 초기화 완료");
            }
        });
    }

    private bool RefreshTheBackendToken(int maxRepeatCount)
    {
        if(maxRepeatCount <= 0)
        {
            return false;
        }

        var bro = Backend.BMember.RefreshTheBackendToken();

        if (bro.IsSuccess())
        {
            return true;
        }
        else
        {
            // 클라이언트의 일시적인 네크워크 끊김 시
            if (bro.IsClientRequestFailError())
                return RefreshTheBackendToken(maxRepeatCount - 1);
            // 서버의 일시적인 장애 발생 시
            else if (bro.IsServerError())
                return RefreshTheBackendToken(maxRepeatCount - 1);
            // 서버 상태가 "점검"일 시
            else if (bro.IsMaintenanceError())
            {
                // 필요하다면 팝업창 띄우기
                // 로그인 화면으로 보내기
                Manager.Instance.UIAndScene.LoadScene(SceneNames.Login);
                return false;
            }
            // 단기간에 많은 요청을 보낼 경우, 403 Forbbiden 발생 시
            else if (bro.IsTooManyRequestError())
            {
                return false;
            }
            else
            {
                // 재시도를 해도 엑세스토큰 재발급이 불가능한 경우
                // 커스텀 로그인 혹은 페데레이션 로그인을 통해 수동 로그인을 진행해야한다.
                // 중복 로그인일 경우 401 Bad refreshToken 에러와 함께 발생할 수 있다.
                return false;
            }
        }
    }

    public string CheckError(BackendReturnObject bro)
    {
        if (bro.IsSuccess())
        {
            return "Success";
        }
        else
        {
            // 클라이언트의 일시적인 네트워크 오류
            if (bro.IsClientRequestFailError())
                return "Repeat";
            // 서버의 이상 발생
            else if (bro.IsServerError())
                return "Repeat";
            // 서버가 '점검'중
            else if (bro.IsMaintenanceError())
                return bro.IsMaintenanceError().ToString();
            // 단기간에 많은 요청이 발생
            else if (bro.IsTooManyRequestError())
                // 5분동안 뒤끝의 함수 요청을 중지해야한다.
                return bro.IsTooManyRequestError().ToString();
            else if (bro.IsBadAccessTokenError())
            {
                bool isRefreshSuccess = RefreshTheBackendToken(3);

                if (isRefreshSuccess)
                {
                    return "Repeat";
                }
                else
                {
                    return "False";
                }
            }
            else
            {
                Debug.Log("오류가 발생 하였습니다. " + bro.ToString());
                return "False";
            }
                
        }
    }

    public void UpdateGameData(string updateField, string inData, Param param, Action<BackendReturnObject> onCompleted = null)
    {
        // 서버 DB에 자신의 데이터를 갱신한다.
        if (!Backend.IsLogin)
        {
            Debug.Log("로그인이 되어있지 않다.");
            return;
        }

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(inData))
        {
            bro = Backend.GameData.Update(updateField, new Where(), param);
        }
        else
        {
            bro = Backend.GameData.UpdateV2(updateField, inData, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("데이터 수정 성공");
        }
        else
        {
            Debug.Log("데이터 수정 실패");
        }
    }

    public void InsertGameData(string updateField, int maxRepeatCount, Param param)
    {
        if (!Backend.IsLogin)
            return;

        if (maxRepeatCount <= 0)
            return;

        Backend.GameData.Insert(updateField, param, callback =>
        {
            switch (CheckError(callback))
            {
                case "401":
                    Debug.LogError("서버 점검 중");
                    break;
                case "False":
                case "false":
                    Debug.LogError("초기화 실패");
                    break;
                case "Repeat":
                case "repeat":
                    Debug.LogError("연결 재시도");
                    InsertGameData(updateField, maxRepeatCount - 1, param);
                    break;
                case "Success":
                case "success":
                    Debug.Log("데이터 추가 성공");
                    break;
            }
        });
    }
}
