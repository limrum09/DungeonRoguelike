using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAndSceneManager : MonoBehaviour
{
    public delegate void ChangeEquipmentEvent();
    public delegate void onSelectQuestListHandler(Quest quest);
    public delegate void QuestUISucessChangeHandler(Quest quest);

    [Header("InGame")]
    [SerializeField]
    private LobbyButtonManager lobbyUI;
    [SerializeField]
    private ShortKeyManager shortCutBox;
    [SerializeField]
    private InventoryButton invenUI;
    [SerializeField]
    private StatusUIManager statusUI;
    [SerializeField]
    private UIProfile uIProfile;
    [SerializeField]
    private QuestViewUI questUI;
    [SerializeField]
    private NPCTalkUIController npcTalkUIController;
    [SerializeField]
    private NPCUI npcUI;
    [SerializeField]
    private SettingContorller settingUI;
    [SerializeField]
    private UISkillController skillUI;

    [Header("Other")]
    [SerializeField]
    private ViewAndHideUIPanels viewAndHideUIPanles;

    public ChangeEquipmentEvent onChangeEquipment;
    public event onSelectQuestListHandler onSelectQuestListView;

    public InventoryButton InventoryUI => invenUI;
    public ShortKeyManager ShortCutBox => shortCutBox;
    public ViewAndHideUIPanels viewAndHide => viewAndHideUIPanles;

    public void UIAndSceneManagerStart()
    {
        this.gameObject.GetComponent<ViewAndHideUIPanels>().ViewAndHideUIStart();

        Manager.Instance.Game.AfterUIStartInGamemanager();
        invenUI.InvenToryStart();
        npcTalkUIController.NPCTalkControllerStart();
        
        skillUI.SkillUIInitialized();
        lobbyUI.LobbyUIStart();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 로드 완료");
        lobbyUI.LobbyUIActiveFalse();
        viewAndHide.CheckCurrentScene();
        Manager.Instance.Game.PlayerController.PlayerInRespawnPoint();
    }

    public void ChnageSelectScene(string sceneName)
    {
        Manager.Instance.Game.PlayerController.SceneChanging();
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeHPBar() 
    {
        uIProfile.SetHPBar();
        statusUI.SetStatusUIText();
    }
    public void ChangeEXPBar()
    {
        uIProfile.SetExpBar();
        statusUI.SetStatusUIText();
    }

    public void LevelUPUI()
    {
        uIProfile.SetExpBar();
        statusUI.SetStatusUIText();
        statusUI.ViewAndHideStateButton();
    }
    public void LoadSettingUIData() => settingUI.SettingControllerStart();
    public void ChangeEquipment(WeaponItem item) => Manager.Instance.Game.PlayerWeaponChange(item);
    public void ChangeEquipment(ArmorItem item) => Manager.Instance.Game.PlayerArmorChange(item);
    public void ChangeQuestDetailView(Quest quest) => onSelectQuestListView?.Invoke(quest);
    public void SelectSkill(ActiveSkill skill) => skillUI.SelectSkill(skill);
    public void SetTackerViewQuest(Quest quest, bool isOn) => questUI.SetTrackerViewQuest(quest, isOn);
    public void NPCQuestTalkWithPlayer(Scenario basicScenario, List<QuestAndScenario> questAndScenario, Sprite npcImage) => npcTalkUIController.GetQuestAndScenario(basicScenario ,questAndScenario, npcImage);
    public void PlayerInQuestNPC() => npcUI.InteractionText.PlayerIn();
    public void PlayerOutQuestNPC() => npcUI.InteractionText.PlayerOut();
    public void ChangeShortCutValue(string keyString) => shortCutBox.ChangeShortKey(keyString);
    public void LoadShortCutKeys(List<ShortCutKeySaveData> keys) => shortCutBox.ShortCutBoxStart(keys);
}
