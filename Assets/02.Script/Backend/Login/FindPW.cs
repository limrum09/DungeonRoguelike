using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BackEnd;
using UnityEngine.UI;

public class FindPW : LoginBase
{
    [SerializeField]
    private Image idImage;
    [SerializeField]
    private TMP_InputField idInputField;
    [SerializeField]
    private Image eMailImage;
    [SerializeField]
    private TMP_InputField eMailInputField;

    [SerializeField]
    private Button findPwBtn;

    public void OnSelect()
    {
        idInputField.Select();
    }

    public void OnFindPWButtonClick()
    {
        findPwBtn.interactable = false;

        // 비어있는지 확인
        if (!IsFieldDataEmpty(idImage, idInputField.text, "아이디")) return;
        if (!IsFieldDataEmpty(eMailImage, eMailInputField.text, "이메일")) return;

        // 이메일 형식 검사
        if (!eMailInputField.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(eMailImage, "이메일 형식이 아닙니다. (ex. address@xx.xx)");
            return;
        }

        SetMessageOnly("이메일로 임시 비밀번호 보내는 중...");

        // 비밀번호 찾기
        FindCustomPassword();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && this.gameObject.activeSelf)
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
    }

    private void FindCustomPassword()
    {
        Backend.BMember.ResetPassword(idInputField.text, eMailInputField.text, callback =>
        {
            if (callback.IsSuccess())
            {
                SetMessageOnly($"{eMailInputField.text} 주소로 임시 비밀번호를 보냈습니다.");
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    // 해당 이메일의 게이머가 없는 경우
                    case 404:
                        message = "해당 이메일로 가입한 계정이 없습니다..";
                        break;
                    // 24시간 이내에 5회이상 같은 이메일 정보로 아이디/비밀번호 찾기를 시도한 경우
                    case 429:
                        message = "24시간 이내 5회 이상 같은 이메일로 아이디/비밀번호를 찾았습니다.";
                        break;
                    // statusCode : 400 => 프로젝트명에 특수문자가 추가된 셩우 (안내 메일 미발송 및 에러 발생)
                    default:
                        message = callback.GetMessage();
                        break;
                }

                // 실패사유
                if (message.Contains("이메일"))
                {
                    // 이메일이 잘못된 경우
                    GuideForIncorrectlyEnteredData(eMailImage, message);
                }
                else
                {
                    SetMessageOnly(message);
                }
            }
        });
    }
}
