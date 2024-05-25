using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAndSceneManager : MonoBehaviour
{
    public static UIAndSceneManager instance;

    public delegate void ChangeEquipmentEvent();

    [SerializeField]
    GameObject lobbyUI;
    [SerializeField]
    GameObject shortcutkeyUI;
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    private UIProfile uiProfile;

    int GameUIIndex;

    public ChangeEquipmentEvent onChangeEquipment;

    public UIProfile Profile => uiProfile;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        var findObject =  GameObject.FindGameObjectWithTag("GameUI");

        GameUIIndex = findObject.transform.childCount - 1;

        findObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.activeSelf)
            {
                HideInventory();
            }
            else
            {
                ViewInventory();
            }            
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (statusUI.activeSelf)
            {
                HideStatus();
            }
            else
            {
                ViewStatus();
            }            
        }
    }

    public void ViewInventory()
    {
        inventoryUI.transform.SetSiblingIndex(GameUIIndex);
        inventoryUI.SetActive(true);
    }
    public void HideInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void ViewStatus()
    {
        statusUI.transform.SetSiblingIndex(GameUIIndex);
        statusUI.SetActive(true);
    }
    public void HideStatus()
    {
        statusUI.SetActive(false);
    }

    public void LobbyScene()
    {
        lobbyUI.SetActive(true);
    }
    public void OUTLobbyScene()
    {
        lobbyUI.SetActive(false);
    }

    public void ChangeEquipment(SelectTest item) => GameManager.instance.PlayerItemChange(item);

    public void ChangeHPBar() => uiProfile.SetHPBar();
    public void ChangeEXPBar() => uiProfile.SetExpBar();
}
