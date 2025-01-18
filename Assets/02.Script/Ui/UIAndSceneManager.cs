using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames
{
    Login = 0,
    Lobby,
    Dongon1,
    Boss
}
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
    private AchievementUI achievementUI;
    [SerializeField]
    private NPCTalkUIController npcTalkUIController;
    [SerializeField]
    private NPCUI npcUI;
    [SerializeField]
    private SettingContorller settingUI;
    [SerializeField]
    private UISkillController skillUI;
    [SerializeField]
    private LoddingUIController loddingUI;
    [SerializeField]
    private EquipmentSelectPanelController equipmentSelectUI;
    [SerializeField]
    private RankingViewer rankingViewer;

    [Header("Other")]
    [SerializeField]
    private ViewAndHideUIPanels viewAndHideUIPanles;
    [SerializeField]
    private GameNotionController gameNotion;

    public ChangeEquipmentEvent onChangeEquipment;
    public event onSelectQuestListHandler onSelectQuestListView;

    public AchievementUI AchievementUI => achievementUI;
    public InventoryButton InventoryUI => invenUI;
    public ShortKeyManager ShortCutBox => shortCutBox;
    public ViewAndHideUIPanels viewAndHide => viewAndHideUIPanles;
    public LoddingUIController LoddingUI => loddingUI;
    public EquipmentSelectPanelController EquipmentSelectUI => equipmentSelectUI;
    public GameNotionController Notion => gameNotion;

    public void UIAndSceneManagerStart()
    {
        this.gameObject.GetComponent<ViewAndHideUIPanels>().ViewAndHideUIStart();

        Manager.Instance.Game.AfterUIStartInGameManager();
        invenUI.InvenToryStart();
        npcTalkUIController.NPCTalkControllerStart();

        equipmentSelectUI.EquipmentSelectPanelStart();
        
        skillUI.SkillUIInitialized();
        lobbyUI.LobbyUIStart();
        loddingUI.gameObject.SetActive(false);
        rankingViewer.RankingStart();

        SceneManager.sceneLoaded += SceneLoadEnd;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoadEnd;
    }

    private void SceneLoadEnd(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning("씬 로드 완료");
        
        viewAndHide.CheckCurrentScene();
        Manager.Instance.Game.PlayerController.PlayerInRespawnPoint();
    }

    public void LoadScene(SceneNames sceneName) => LoadScene(sceneName.ToString());

    public void LoadScene(string sceneName)
    {
        Manager.Instance.Game.PlayerController.SceneChanging();
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    public void ChangeHPBar() 
    {
        uIProfile.SetHPBar();
        statusUI.SetStatusUIText();
    }
    public void ChangeEXPBar()
    {
        uIProfile.SetExpBar();
        statusUI.SetExpUIText();
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

    IEnumerator LoadAsyncScene(string loadSceneName)
    {
        loddingUI.gameObject.SetActive(true);
        loddingUI.LoddingUIStart();

        // 씬이 로드된 정도를 확인한다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadSceneName);

        Manager.Instance.Save.DataSaving();
        yield return new WaitForSeconds(1.0f);

        Manager.Instance.Save.LoadData();
        yield return new WaitForSeconds(1.0f);

        loddingUI.LoddingRateValue("던전 찾는 중...", 60.0f);

        // 씬이 로드가 완료가 되면 isDone이 true값을 바환한다.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
