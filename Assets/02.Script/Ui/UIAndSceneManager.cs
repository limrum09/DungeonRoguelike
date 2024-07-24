using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAndSceneManager : MonoBehaviour
{
    public static UIAndSceneManager instance;

    public delegate void ChangeEquipmentEvent();
    public delegate void onSelectQuestListHandler(Quest quest);
    public delegate void QuestUISucessChangeHandler(Quest quest);

    [SerializeField]
    private InventoryButton invenUI;
    [SerializeField]
    private StatusUIManager statusUI;
    [SerializeField]
    private UIProfile uiProfile;
    [SerializeField]
    private QuestViewUI questUI;
    [SerializeField]
    private NPCTalkUIController npcTalkUIController;
    [SerializeField]
    private NPCUI npcUI;

    public ChangeEquipmentEvent onChangeEquipment;
    public event onSelectQuestListHandler onSelectQuestListView;

    public InventoryButton InventoryUI => invenUI;
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

    public void ChangeEquipment(WeaponItem item) => Manager.Instance.Game.PlayerWeaponChange(item);
    public void ChangeEquipment(ArmorItem item) => Manager.Instance.Game.PlayerArmorChange(item);
    public void ChangeHPBar() 
    {
        uiProfile.SetHPBar();
        statusUI.SetStatusUIText();
    }
    public void ChangeEXPBar()
    {
        uiProfile.SetExpBar();
        statusUI.SetStatusUIText();
    }

    public void LevelUPUI()
    {
        uiProfile.SetExpBar();
        statusUI.SetStatusUIText();
        statusUI.ViewAndHideStateButton();
    }

    public void ChangeQuestDetailView(Quest quest)
    {
        onSelectQuestListView?.Invoke(quest);
    }

    public void SetTackerViewQuest(Quest quest, bool isOn) => questUI.SetTrackerViewQuest(quest, isOn);
    public void NPCQuestTalkWithPlayer(Scenario basicScenario, List<QuestAndScenario> questAndScenario, Sprite npcImage) => npcTalkUIController.GetQuestAndScenario(basicScenario ,questAndScenario, npcImage);
    public void PlayerInQuestNPC() => npcUI.InteractionText.PlayerIn();
    public void PlayerOutQuestNPC() => npcUI.InteractionText.PlayerOut();
}
