using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using TMPro;

public class CreateAccount : LoginBase
{
    [SerializeField]
    private TMP_InputField idInputField;
    [SerializeField]
    private Image idImage;
    [SerializeField]
    private TMP_InputField pwInputField;
    [SerializeField]
    private Image pwImage;
    [SerializeField]
    private TMP_InputField confirmPwInputField;
    [SerializeField]
    private Image confirmPwImage;
    [SerializeField]
    private TMP_InputField e_MailInputField;
    [SerializeField]
    private Image e_MailImage;

    [SerializeField]
    private Button createAccountBtn;

    public void OnCreateActtounButtonClick()
    {
        ResetUI(idImage, pwImage, confirmPwImage, e_MailImage);

        // 비어있는지 확인
        if (!IsFieldDataEmpty(idImage, idInputField.text, "아이디")) return;
        if (!IsFieldDataEmpty(pwImage, pwInputField.text, "비밀번호")) return;
        if (!IsFieldDataEmpty(confirmPwImage, confirmPwInputField.text, "비밀번호 확인")) return;
        if (!IsFieldDataEmpty(e_MailImage, e_MailInputField.text, "이메일")) return;


        // 비밀번호 확인에 입력한 비밀번호가 서로 같은지 비교
        if (!pwInputField.text.Equals(confirmPwInputField.text))
        {
            GuideForIncorrectlyEnteredData(confirmPwImage, "비밀번호가 일치하지 않습니다.");
            return;
        }

        // 이메일 형식 검사
        if (!e_MailInputField.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(e_MailImage, "이메일 형식이 아닙니다. (ex. address@xx.xx)");
            return;
        }

        createAccountBtn.interactable = false;
        SetMessageOnly("계정 생성중...");

        // 계정 생성 시도
        CustomSignUp();
    }

    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(idInputField.text, pwInputField.text, callback =>
        {
            // 계정생성 버튼 비활성화
            createAccountBtn.interactable = true;

            if (callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(e_MailInputField.text, callback =>
                {
                    if (callback.IsSuccess())
                    {
                        SetMessageOnly($"계정을 만들었습니다. {idInputField.text}님, 환영합니다!");

                        // 계정 만들기 성공 시, 자동 로그인 후, Lobby 씬 이동
                        Manager.Instance.UIAndScene.LoadScene(SceneNames.Lobby);
                    }
                });
            }
            else
            {

                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 409:
                        message = "중복된 ID가 존재합니다.";
                        break;
                    // 차단당한 디바이스인 경우
                    case 403:
                    // 프로젝트 상태가 '점검'인 경우
                    case 401:
                    // 디바이스 정보가 null인 경우
                    case 400:
                    default:
                        message = callback.GetMessage();
                        break;
                }

                // 일반적으로 계정 생성에 실패하는 경우는 이미 해당아이디가 존재하는 경우이다.
                // 메시지 내용에 "아이디"가 포함되어 있으면 해당 아이기 입력 필드 색상을 변경하고, 에러 내용을 출력한다.
                if (message.Contains("ID"))
                    GuideForIncorrectlyEnteredData(idImage, message);
                else
                    SetMessageOnly(message);
            }
        });
    }
}
