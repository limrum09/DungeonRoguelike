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
    private LobbyButtonManager lobbyUI;     // 로비UI
    [SerializeField]
    private ShortKeyManager shortCutBox;    // 단출키 박스
    [SerializeField]
    private InventoryButton invenUI;        // 인벤토리 UI
    [SerializeField]
    private StatusUIManager statusUI;       // 능력치 UI
    [SerializeField]
    private UIProfile uIProfile;            // 프로필 UI
    [SerializeField]
    private BuffUIController buffUI;        // 버프 UI
    [SerializeField]
    private QuestViewUI questUI;            // 퀘스트 UI
    [SerializeField]
    private AchievementUI achievementUI;    // 업적 UI
    [SerializeField]
    private NPCTalkUIController npcTalkUIController;    // NPC 대화 UI 컨트롤러
    [SerializeField]
    private NPCUI npcUI;                    // NPC UI
    [SerializeField]
    private SettingContorller settingUI;    // 설정 UI
    [SerializeField]
    private UISkillController skillUI;      // 스킬 UI
    [SerializeField]
    private LoddingUIController loddingUI;  // 씬 변경시 보이는 로딩 이미지
    [SerializeField]
    private EquipmentSelectPanelController equipmentSelectUI;   // Lobby에 보이는 장비 선택 UI
    [SerializeField]
    private RankingViewer rankingViewer;    // 랭킹 UI
    [SerializeField]
    private StoreUIController storeUI;

    [Header("Other")]
    [SerializeField]
    private ViewAndHideUIPanels viewAndHideUIPanles;    // UI Panel을 숨기거나 보이도록 하는 컨트롤러
    [SerializeField]
    private GameNotionController gameNotion;            // 왼쪽 하단에 보이는 게임 노션

    public ChangeEquipmentEvent onChangeEquipment;      // 장비 변경시 호출되는 이벤트
    public event onSelectQuestListHandler onSelectQuestListView;    // 퀘스트 UI에서 현제 퀘스트나 완료된 퀘스트의 디테일한 내용을 보기위해 List에서 퀘스트를 선택하면 호출되는 이벤트

    // 프로퍼티
    public BuffUIController BuffUI => buffUI;
    public AchievementUI AchievementUI => achievementUI;
    public InventoryButton InventoryUI => invenUI;
    public ShortKeyManager ShortCutBox => shortCutBox;
    public ViewAndHideUIPanels viewAndHide => viewAndHideUIPanles;
    public LoddingUIController LoddingUI => loddingUI;
    public EquipmentSelectPanelController EquipmentSelectUI => equipmentSelectUI;
    public GameNotionController Notion => gameNotion;
    public StoreUIController StoreUI => storeUI;

    // UI 초기화
    public void UIAndSceneManagerStart()
    {
        this.gameObject.GetComponent<ViewAndHideUIPanels>().ViewAndHideUIStart();

        buffUI.BuffUIControllerStart();

        Manager.Instance.Game.AfterUIStartInGameManager();
        invenUI.InvenToryStart();
        npcTalkUIController.NPCTalkControllerStart();

        equipmentSelectUI.EquipmentSelectPanelStart();
        
        skillUI.SkillUIInitialized();
        lobbyUI.LobbyUIStart();
        loddingUI.gameObject.SetActive(false);
        rankingViewer.RankingStart();
        StoreUI.StoreUIStart();

        SceneManager.sceneLoaded += SceneLoadEnd;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoadEnd;
    }

    #region Scene Load Functions
    // 씬 로드 끝나면 호출
    private void SceneLoadEnd(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬 로드 완료");
        
        viewAndHide.CheckCurrentScene();
    }

    // 씬 로드 시작, SceneNames로 입력이 들어오면 string으로 전화하여 LoadScene(string sceneName)호출
    public void LoadScene(SceneNames sceneName) => LoadScene(sceneName.ToString());
    // 씬 로드 시작
    public void LoadScene(string sceneName)
    {
        Manager.Instance.Game.PlayerController.SceneChanging();
        // 씬을 로딩하는 코루틴 실행
        StartCoroutine(LoadAsyncScene(sceneName));
    }
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
    #endregion

    #region External Call Functions
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
    #endregion
}
