using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TMPro;
using UnityEngine.UI;

public class FindID : LoginBase
{
    [SerializeField]
    private Image eMailImage;
    [SerializeField]
    private TMP_InputField eMailInputField;

    [SerializeField]
    private Button findIdBtn;
    public void OnFindIdButtonClick()
    {
        // 연타 방지
        findIdBtn.interactable = false;

        // 비어있는지 확인
        if (!IsFieldDataEmpty(eMailImage, eMailInputField.text, "이메일")) return;

        // 형식이 틀린 경우
        if (!eMailInputField.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(eMailImage, "이메일 형식이 아닙니다. (ex. address@xx.xx)");
            return;
        }

        SetMessageOnly("이메일로 아이디 보내는 중...");

        FindCustomID();
    }

    private void FindCustomID()
    {
        Backend.BMember.FindCustomID(eMailInputField.text, callback =>
        {
            findIdBtn.interactable = true;

            if(callback.IsSuccess())
            {
                SetMessageOnly($"{eMailInputField.text}주소로 메일을 보냈습니다.");
            }
            else
            {
                string message = string.Empty;

                switch(int.Parse(callback.GetStatusCode()))
                {
                    // 해당 이메일의 게이머가 없는 경우
                    case 404:
                        message = "해당 이메일로 가입된 계정이 없습니다.";
                        break;
                    // 24시간 이내에 5회이상 같은 이메일 정보로 아이디/비밀번호 찾기를 시도한 경우
                    case 429:
                        message = "24시간 이내 5회 이상 같은 이메일로 아이디/비밀번호를 찾았습니다.";
                        break;
                    // statusCode : 400 => 프로젝트명에 특수문자가 추가된 경우 (안내 메일 미발송 및 에러 발생)
                    default:
                        message = callback.GetMessage();
                        break;
                }

                // 실패한 사유 출력
                if (message.Contains("이메일"))
                    GuideForIncorrectlyEnteredData(eMailImage, message);
                else
                    SetMessageOnly(message);
            }
        });
    }
}
