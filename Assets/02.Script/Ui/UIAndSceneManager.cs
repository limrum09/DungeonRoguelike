using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAndSceneManager : MonoBehaviour
{
    public static UIAndSceneManager instance;

    public delegate void ChangeEquipmentEvent();
    public delegate void onSelectQuestListHandler(Quest quest);
    public delegate void QuestUISucessChangeHandler(Quest quest);

    [Header("GameObject")]
    [SerializeField]
    GameObject lobbyUI;
    [SerializeField]
    GameObject shortcutkeyUI;
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    GameObject questViewUI;
    [SerializeField]
    GameObject NPCUI;

    [Header("Script")]
    [SerializeField]
    private UIProfile uiProfile;
    [SerializeField]
    private QuestViewUI questUI;
    [SerializeField]
    private NPCTalkUIController npcTalkUIController;

    int GameUIIndex;

    public ChangeEquipmentEvent onChangeEquipment;
    public event onSelectQuestListHandler onSelectQuestListView;

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
        
        // questViewUI.QuestUIStart();
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (questViewUI.activeSelf)
            {
                HideQuestUI();
            }
            else
            {
                ViewQuestUI();
            }
        }
    }

    public void ViewInventory()
    {
        inventoryUI.transform.SetAsFirstSibling();
        inventoryUI.SetActive(true);
    }
    public void HideInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void ViewStatus()
    {
        statusUI.transform.SetAsFirstSibling();
        statusUI.SetActive(true);
    }
    public void HideStatus()
    {
        statusUI.SetActive(false);
    }

    public void ViewQuestUI()
    {
        questViewUI.transform.SetAsFirstSibling();
        questViewUI.SetActive(true);
    }
    public void HideQuestUI()
    {
        questViewUI.SetActive(false);
    }

    public void LobbyScene()
    {
        lobbyUI.SetActive(true);
    }
    public void OUTLobbyScene()
    {
        lobbyUI.SetActive(false);
    }


    public void ChangeEquipment(WeaponItem item) => GameManager.instance.PlayerWeaponChange(item);
    public void ChangeEquipment(ArmorItem item) => GameManager.instance.PlayerArmorChange(item);
    public void ChangeHPBar() => uiProfile.SetHPBar();
    public void ChangeEXPBar() => uiProfile.SetExpBar();

    public void ChangeQuestDetailView(Quest quest)
    {
        onSelectQuestListView?.Invoke(quest);
    }

    public void SetTackerViewQuest(Quest quest, bool isOn) => questUI.SetTrackerViewQuest(quest, isOn);
    public void NPCQuestTalkWithPlayer(Scenario basicScenario, List<QuestAndScenario> questAndScenario) => npcTalkUIController.GetQuestAndScenario(basicScenario ,questAndScenario);
}
