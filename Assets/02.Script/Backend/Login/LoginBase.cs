using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginBase : MonoBehaviour
{
    protected EventSystem system;

    [SerializeField]
    private TextMeshProUGUI textMessage;

    protected void Awake()
    {
        system = EventSystem.current;
    }

    /// <summary>
    /// 매개변수로 넘어온 모든 필드 이미지 색상 하얀색으로 초기화
    /// textMessage 비워주기
    /// </summary>
    /// <param name="images"></param>
    protected void ResetUI(params Image[] images)
    {
        textMessage.text = string.Empty;

        for(int i = 0;i < images.Length; i++)
        {
            images[i].color = Color.white;
        }
    }

    /// <summary>
    /// 오로지 메세지 변경에만 사용
    /// 매개변수로 넘어온 문자열은 textMessage에 출력
    /// </summary>
    /// <param name="msg"></param>
    protected void SetMessageOnly(string msg)
    {
        textMessage.text = msg;
    }

    /// <summary>
    /// 필드값이 오류일 시 사용
    /// 오류에 대한 메세지 출력, 필드의 Image색상 변경
    /// </summary>
    /// <param name="image"></param>
    /// <param name="msg"></param>
    protected void GuideForIncorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;
    }

    /// <summary>
    /// field로 넘어오는 매개변수의 값이 비어있는지 확인
    /// filed의 값이 비어있을 때, 해당 필드의 색상을 붉은색으로 설정하고, textMessage에 "result 필드를 채워주세요"출력
    /// image : 필드, field : 내용, result : 출력될 내용
    /// </summary>
    /// <param name="image"></param>
    /// <param name="field"></param>
    /// <param name="result"></param>
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        // field의 내용이 없는경우
        if (field.Trim().Equals(""))
        {
            GuideForIncorrectlyEnteredData(image, $"{result} 값을 채워주세요");
            return false;
        }

        return true;
    }
}
