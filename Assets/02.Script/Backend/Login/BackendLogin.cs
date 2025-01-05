using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using BackEnd.BackndLitJson;

public class BackendLogin : LoginBase
{
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldPW;
    [SerializeField]
    private Image imagePW;

    [SerializeField]
    private Button loginBtn;


    private bool isLogin;
    public void OnLoginButtonClick()
    {
        isLogin = false;
        // message나 색이 변한 InputField의 Image 초기화
        ResetUI(imageID, imagePW);

        // 비어있는지 확인
        if (!IsFieldDataEmpty(imageID, inputFieldID.text, "로그인")) return;
        if (!IsFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;


        // 로그인 버튼 연타 방지를 위한, 상호작용 해체
        loginBtn.interactable = false;

        StartCoroutine(LoginProcess());

        ResponseLogin(inputFieldID.text, inputFieldPW.text);
    }

    private void StopLoddingCoroutine()
    {
        StopCoroutine(LoginProcess());
    }

    private void ResponseLogin(string id, string pw)
    {
        Backend.BMember.CustomLogin(id, pw, callback =>
        {
            // 로그인 성공
            if (callback.IsSuccess())
            {
                isLogin = true;
                SetMessageOnly($"{inputFieldID.text}님, 환영합니다!");

                // PlayerObject를 여기서 만들수 있다면 좋겠는데
                SceneManager.LoadScene(SceneNames.Lobby.ToString());
            }
            else
            {
                Debug.Log("로그인 실패");
                isLogin = true;
                loginBtn.interactable = true;

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 401:
                        message = callback.GetMessage().Contains("customId") ? "존재하지 않는 아이디 입니다." : "존재하지 않는 비밀번호 입니다.";
                        break;
                    case 403:
                        message = callback.GetMessage().Contains("user") ? "차단된 유저 입니다." : "차단된 기기 입니다.";
                        break;
                    case 410:
                        message = "탈퇴진행 중인 유저입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                // 아이디, 비밀번호 중에서 에러가 발생한 필드의 색상을 붉은색으로 변경하고, 에러 내용을 출력
                if (message.Contains("비밀번호"))
                    GuideForIncorrectlyEnteredData(imagePW, message);
                else
                    GuideForIncorrectlyEnteredData(imageID, message);
            }
        });
    }


    private IEnumerator LoginProcess()
    {
        float time = 0;

        isLogin = false;

        while (!isLogin)
        {
            time += Time.deltaTime;

            SetMessageOnly($"로그인 중...{time:F1}");

            yield return null;
        }
    }
}
