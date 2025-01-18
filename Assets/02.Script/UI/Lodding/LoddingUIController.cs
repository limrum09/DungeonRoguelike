using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoddingUIController : MonoBehaviour
{
    [SerializeField]
    private LoddingImagesAndTips imagesAndTips;
    [SerializeField]
    private CanvasGroup group;
    private float loddingTimer;

    [Header("Image & Tip")]
    [SerializeField]
    private Image loddingImage;
    [SerializeField]
    private TextMeshProUGUI loddingTipText;

    [Header("Lodding Bar")]
    [SerializeField]
    private TextMeshProUGUI loddingInfoText;
    [SerializeField]
    private TextMeshProUGUI loddingRatingPerText;
    [SerializeField]
    private Slider loddingRatingBar;

    private float loddingRating;
    // Start is called before the first frame update
    public void LoddingUIStart()
    {
        loddingRating = 0.0f;
        group.alpha = 1;
        loddingTimer = 1.5f;

        int imageIndex = Random.Range(0, imagesAndTips.Images.Count);
        Sprite selectLoddingSprite = imagesAndTips.Images[imageIndex];

        int textIndex = Random.Range(0, imagesAndTips.Tips.Count);
        string selectLoddingTip = imagesAndTips.Tips[textIndex];

        loddingImage.sprite = selectLoddingSprite;
        loddingTipText.text = selectLoddingTip;

        loddingInfoText.text = "유저 정보 확인 중...!";
        loddingRatingPerText.text = "( " + Mathf.RoundToInt(loddingRating) + " / 100 )";
        loddingRatingBar.value = loddingRating;
    }

    public void LoddingRateValue(string infoText, float loddingRate)
    {
        loddingRating = loddingRate;

        loddingInfoText.text = infoText;
        loddingRatingPerText.text = "( " + Mathf.RoundToInt(loddingRating) + " / 100 )";
        loddingRatingBar.value = loddingRating;

        if (loddingRating >= 100.0f)
            StartCoroutine(LoddingComplete());
    }

    // 씬 로드가 완료되면 LoddingUI를 1.5초동안 투명하게 만들기
    IEnumerator LoddingComplete()
    {
        float timer = loddingTimer;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            group.alpha = Mathf.Clamp01(timer / loddingTimer);

            yield return null;
        }

        group.alpha = 0;
        this.gameObject.SetActive(false);
        Manager.Instance.Game.PlayerController.PlayerCanMove();
    }
}
