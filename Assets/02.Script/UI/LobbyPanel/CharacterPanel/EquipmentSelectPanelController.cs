using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectPanelController : MonoBehaviour
{
    [SerializeField]
    private ItemInfoPanel itemInfoPanel;

    private RectTransform itemInfoPanelRect;
    private WeaponItemDatabase weaponItemDatabase;
    private ArmorItemDatabase armorItemDatabase;

    private Camera viewCamera;

    // Start is called before the first frame update
    public void EquipmentSelectPanelStart()
    {
        itemInfoPanelRect = itemInfoPanel.GetComponent<RectTransform>();

        weaponItemDatabase = Resources.Load<WeaponItemDatabase>("WeaponItemDatabase");
        armorItemDatabase = Resources.Load<ArmorItemDatabase>("ArmorItemDatabase");

        HideItemInfo();
        this.gameObject.SetActive(false);
    }

    public void ViewItemInfo(int itemClassValue, string itemCode, Transform tf, Rect rect)
    {
        // 카메라가 없을 경우, 카메라 할당
        if (viewCamera == null)
            viewCamera = Manager.Instance.Camera.CurrentCamera;

        itemInfoPanel.gameObject.SetActive(true);

        // 툴팁 위치 조절
        Vector3 panelPos = CheckTooltipTransform(tf, rect);
        itemInfoPanel.transform.position = panelPos;


        // itemItemValue, 0 : 무기, 1 : 방어구
        if(itemClassValue == 0)
        {
            // 아이템 찾기
            WeaponItem weaponItem = null;
            weaponItem = weaponItemDatabase.FindItemBy(itemCode);

            if (weaponItem != null)
            {
                if (weaponItem.ItemObject != null)
                    itemInfoPanel.SetItemInfo(weaponItem);
                else
                    HideItemInfo();
            }
        }
        else if(itemClassValue == 1)
        {
            // 아이템 찾기
            ArmorItem armorItem = null;
            armorItem = armorItemDatabase.FindItemBy(itemCode);

            if (armorItem != null)
            {
                if (armorItem.ItemObject != null)
                    itemInfoPanel.SetItemInfo(armorItem);
                else
                    HideItemInfo();
            }
        }
        else
            Debug.LogWarning($"{itemCode}에 해당하는 올바른 아이템이 없습니다.");
    }

    public void HideItemInfo()
    {
        itemInfoPanel.ResetInfoPanel();
        itemInfoPanel.gameObject.SetActive(false);
    }

    private Vector3 CheckTooltipTransform(Transform tf, Rect btnRect)
    {
        float panelHeight = itemInfoPanelRect.rect.height;

        // 현제 마우스 포인터가 올라가 있는 버튼 크기
        float selectBtnWidth = btnRect.width;
        float selectBtnHeight = btnRect.height;

        // 현제 마우스 포인터가 올라가 있는 버튼 위치
        Vector3 currentTf = tf.position;
        
        // itemInfoPanel 위치 정의
        float posX = currentTf.x - (selectBtnWidth / 2);
        float posY = currentTf.y - (panelHeight / 2) + (selectBtnHeight / 2);
        float posZ = currentTf.z;

        // itemInfoPanel의 가장 아래 족 Y좌표;
        float tempPosY = posY - panelHeight;

        // 현제 panel의 임시 좌표 값이 카메라에서 벗어 났는지 확인
        Vector3 viewportPos = viewCamera.WorldToViewportPoint(new Vector3(posX, tempPosY, posZ));

        // viewportPos.y < 0 일 경우, 카메라 아래로 뚫고 내려가는 것
        // viewportPos.y > 0 일 경우, 카메라 위로 뚫고 올라가는 것
        if (viewportPos.y < 0)
        {
            // itemInfoPanel의 제일 아래 족 좌표가 카메라 뷰어의 아래에 위치해 있어, 아래 부분이 카메라에서 안보일 경우
            // 250f로 고정하여 전부다 보이도록 한다.
            posY = 250f;
        }

        Vector3 checkPos = new Vector3(posX, posY, posZ);

        return checkPos;
    }

}
