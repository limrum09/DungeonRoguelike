using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class UserInfoViewController : MonoBehaviour
{
    [Header("UserInfo")]
    [SerializeField]
    private TextMeshProUGUI playerNickname;
    [SerializeField]
    private TextMeshProUGUI playerId;
    [SerializeField]
    private TextMeshProUGUI playerCountry;
    [SerializeField]
    private Image playerImage;
    

    [Header("Update Nickname")]
    [SerializeField]
    private TMP_InputField inputUpdateNickname;
    [SerializeField]
    private Button nicknameUpdateOKBtn;
    [SerializeField]
    private Button nicknameUpdateCancelBtn;
    [SerializeField]
    private GameObject userInfoFrame;
    [SerializeField]
    private SingleTextPanel updateCheckPanel;
    [SerializeField]
    private SingleTextPanel updateNicknameFailPanel;
    [SerializeField]
    private SingleTextPanel updateNicknameSuccessPanel;

    [Header("Update Password")]
    [SerializeField]
    private TMP_InputField inputUpdateOldpassword;
    [SerializeField]
    private TMP_InputField inputUpdateNewPassword1;
    [SerializeField]
    private TMP_InputField inputUpdateNewPassword2;
    [SerializeField]
    private TextMeshProUGUI updatePasswordErrorText;
    [SerializeField]
    private Button passwordUpdateOKBtn;
    [SerializeField]
    private Button passwordUpdateCancelBtn;
    [SerializeField]
    private SingleTextPanel updatePasswordPanel;
    [SerializeField]
    private SingleTextPanel updatePasswordFailPanel;
    [SerializeField]
    private SingleTextPanel updatePasswordSuccessPanel;

    private void Start()
    {
        userInfoFrame.SetActive(false);
    }

    public void SetUserInfo()
    {
        Manager.Instance.UserInfoData.GetUserInfoFromBackend();

        playerNickname.text = string.IsNullOrEmpty(UserInfo.UserData.nickname) ? UserInfo.UserData.gamerId : UserInfo.UserData.nickname;
        playerId.text = UserInfo.UserData.gamerId;
        playerCountry.text = UserInfo.UserData.countryCode;

        inputUpdateNickname.text = string.Empty;
    }

    public void StopInputKey()
    {
        Manager.Instance.canInputKey = false;
    }

    public void StartInputKey()
    {
        Manager.Instance.canInputKey = true;
    }

    public void OnClickUpdateCheckBtn()
    {
        if (string.IsNullOrEmpty(inputUpdateNickname.text))
        {
            inputUpdateNickname.text = $"<color=red>닉네임을 입력해주세요!";
        }
        else if (inputUpdateNickname.text.Contains("!"))
        {
            inputUpdateNickname.text = $"<color=red>닉네임을 입력해주세요!";
        }
        else
        {
            updateCheckPanel.gameObject.SetActive(true);
            string message = $"정말 [{inputUpdateNickname.text}]로 닉네임을 변경 하시겠습니까?";
            updateCheckPanel.SetMessage(message);
        }
    }

    public void OnClickUpdateNicknameBtn()
    {
        // 넥네임 변경 도중, 버튼을 중복으로 누를 수 없도록 조치
        nicknameUpdateOKBtn.interactable = false;
        nicknameUpdateCancelBtn.interactable = false;

        UpdateNickname();
    }

    public void OnClickUpdatePasswordCheckBtn()
    {
        bool isAllCheck = true;

        string errorMessage = string.Empty;
        if (string.IsNullOrEmpty(inputUpdateOldpassword.text))
        {
            errorMessage += $"<color=red>현재 비밀번호를 입력해주세요!\n";
            isAllCheck = false;
        }

        if (string.IsNullOrEmpty(inputUpdateNewPassword1.text))
        {
            errorMessage += $"<color=red>변경하는 비밀번호를 입력해주세요!\n";
            isAllCheck = false;
        }

        if (string.IsNullOrEmpty(inputUpdateNewPassword2.text))
        {
            errorMessage += $"<color=red>변경하는 비밀번호 확인을 입력해주세요!\n";
            isAllCheck = false;
        }

        if (!inputUpdateNewPassword1.text.Equals(inputUpdateNewPassword2.text))
        {
            errorMessage += $"<color=red>비밀번호가 다릅니다!";
            isAllCheck = false;
        }

        updatePasswordErrorText.text = errorMessage;

        if (isAllCheck)
        {
            updatePasswordPanel.gameObject.SetActive(true);
            string message = $"정말 비밀번호를\n 변경 하시겠습니까?";
            updatePasswordPanel.SetMessage(message);
        }
    }

    public void OnClickUpdatePasswordBtn()
    {
        passwordUpdateCancelBtn.interactable = false;
        passwordUpdateOKBtn.interactable = false;

        UpdatePassword();
    }

    private void UpdateNickname()
    {
        string updateNickname = inputUpdateNickname.text;

        // 닉네임 업데이트
        Backend.BMember.UpdateNickname(updateNickname, callback =>
        {
            nicknameUpdateCancelBtn.interactable = true;
            nicknameUpdateOKBtn.interactable = true;

            string message = string.Empty;

            if (callback.IsSuccess())
            {
                updateNicknameFailPanel.gameObject.SetActive(false);
                updateNicknameSuccessPanel.gameObject.SetActive(true);

                Manager.Instance.UserInfoData.GetUserInfoFromBackend();
                playerNickname.text = UserInfo.UserData.nickname;

                message = $"{updateNickname}으로 닉네임 변경에 성공했습니다.";
                updateNicknameSuccessPanel.SetMessage(message);
            }
            else
            {
                updateNicknameFailPanel.gameObject.SetActive(true);
                updateNicknameSuccessPanel.gameObject.SetActive(false);

                message = "닉네임 변경에 실패했습니다.\n";

                switch(int.Parse(callback.GetStatusCode()))
                {
                    // 닉네임이 비어있거나, 20자 이상의 닉네임, 앞 or 뒤에 공백이 있는 닉네임
                    case 400:
                        message += "20자 이상의 닉네임이거나\n 앞 또는 뒤에 공백이 있습니다.";
                        break;
                    // 이미 존재하는 닉네임
                    case 409:
                        message += "이미 존재하는 닉네임 입니다.";
                        break;
                    default:
                        message += callback.GetMessage();
                        break;
                }

                updateNicknameFailPanel.SetMessage(message);
            }
        });
    }
    private void UpdatePassword()
    {
        string oldPassword = inputUpdateOldpassword.text;
        string updatePassword = inputUpdateNewPassword1.text;

        Backend.BMember.UpdatePassword(oldPassword,updatePassword, callback=> {
            passwordUpdateCancelBtn.interactable = true;
            passwordUpdateOKBtn.interactable = true;

            string message = string.Empty;
            if (callback.IsSuccess())
            {
                updatePasswordSuccessPanel.gameObject.SetActive(true);
                updatePasswordFailPanel.gameObject.SetActive(false);

                message = $"비밀번호 변경에 성공했습니다!";

                updatePasswordSuccessPanel.SetMessage(message);
            }
            else
            {
                updatePasswordSuccessPanel.gameObject.SetActive(false);
                updatePasswordFailPanel.gameObject.SetActive(true);

                message = "비밀번호 변경에 실패했습니다.\n";

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        message += $"현재 비밀번호를\n 잘못 입력하셨습니다.";
                        break;
                }

                updatePasswordFailPanel.SetMessage(message);
            }
        });
    }
}
