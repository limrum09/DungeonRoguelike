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

    [Header("Modify")]
    [SerializeField]
    private TMP_InputField inputUpdateNickname;

    [Header("Button")]
    [SerializeField]
    private Button nicknameUpdateOKBtn;
    [SerializeField]
    private Button nicknameUpdateCancelBtn;

    [Header("Panel")]
    [SerializeField]
    private GameObject userInfoFrame;
    [SerializeField]
    private SingleTextPanel updateCheckPanel;
    [SerializeField]
    private SingleTextPanel updateFailPanel;
    [SerializeField]
    private SingleTextPanel updateSuccessPanel;

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
        nicknameUpdateOKBtn.interactable = false;
        nicknameUpdateCancelBtn.interactable = false;

        UpdateNickname();
    }

    private void UpdateNickname()
    {
        string updateNickname = inputUpdateNickname.text;

        Backend.BMember.UpdateNickname(updateNickname, callback =>
        {
            nicknameUpdateCancelBtn.interactable = true;
            nicknameUpdateOKBtn.interactable = true;

            string message = string.Empty;

            if (callback.IsSuccess())
            {
                updateFailPanel.gameObject.SetActive(false);
                updateSuccessPanel.gameObject.SetActive(true);

                Manager.Instance.UserInfoData.GetUserInfoFromBackend();
                playerNickname.text = UserInfo.UserData.nickname;

                message = $"{updateNickname}으로 닉네임 변경에 성공했습니다.";
                updateSuccessPanel.SetMessage(message);
            }
            else
            {
                updateFailPanel.gameObject.SetActive(true);
                updateSuccessPanel.gameObject.SetActive(false);

                message = "닉네임 변경에 실패했습니다.\n";

                switch(int.Parse(callback.GetStatusCode()))
                {
                    // 닉네임이 비어있거나, 20자 이상의 닉네임, 앞 or 뒤에 공백이 있는 닉네임
                    case 400:
                        message += "20자 이상의 닉네임이거나, 앞 또는 뒤에 공백이 있습니다.";
                        break;
                    // 이미 존재하는 닉네임
                    case 409:
                        message += "이미 존재하는 닉네임 입니다.";
                        break;
                    default:
                        message += callback.GetMessage();
                        break;
                }

                updateFailPanel.SetMessage(message);
            }
        });
    }
}
