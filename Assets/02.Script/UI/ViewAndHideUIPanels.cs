using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewAndHideUIPanels : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField]
    GameObject lobbyUI;
    [SerializeField]
    GameObject shortcutkeyUI;
    [SerializeField]
    GameObject profileUI;
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    GameObject questViewUI;
    [SerializeField]
    GameObject achievementUI;
    [SerializeField]
    GameObject NPCUI;
    [SerializeField]
    GameObject skillUI;
    [SerializeField]
    GameObject settingUI;
    [SerializeField]
    GameObject optionUI;
    [SerializeField]
    GameObject rankUI;

    private GameObject lastUI;
    private InputKey key;

    private List<GameObject> GameUI;
    private List<GameObject> InteractionUI;

    public void ViewAndHideUIStart()
    {
        GameUI = new List<GameObject> { 
            inventoryUI,
            statusUI,
            questViewUI,
            achievementUI,
            settingUI,
            skillUI
        };

        InteractionUI = new List<GameObject>
        {
            lobbyUI,
            profileUI,
            shortcutkeyUI,
        };

        foreach(var ui in GameUI)
        {
            ui.SetActive(false);
        }
        optionUI.SetActive(false);

        key = Manager.Instance.Key;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Player 시점에서 가장 앞에 보이는 UI 종료
            lastUI = HideLastIndexUI();
            if (lastUI != null)
                HideUI(lastUI);
        }

        if (Input.GetKeyDown(key.GetKeyCode("Inventory")))
        {
            ToggleUI(inventoryUI, true);
        }

        if (Input.GetKeyDown(key.GetKeyCode("Status")))
        {
            ToggleUI(statusUI, true);
        }

        if (Input.GetKeyDown(key.GetKeyCode("Quest")))
        {
            ToggleUI(questViewUI, true);
        }

        if (Input.GetKeyDown(key.GetKeyCode("Skill")))
        {
            ToggleUI(skillUI, true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && lastUI == null)
        {
            ToggleUI(optionUI, true);
        }
    }

    public void CheckCurrentScene() => CorrectUIForScene();

    private void CorrectUIForScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        foreach (var ui in InteractionUI)
            ui.SetActive(false);

        if(sceneName == "Login")
        {
            Manager.Instance.canInputKey = false;
        }
        else if(sceneName == "Lobby")
        {
            // Lobby Menu 보여야함
            lobbyUI.SetActive(true);
            profileUI.SetActive(true);
            shortcutkeyUI.SetActive(true);
            Manager.Instance.canInputKey = true;
        }
        else
        {
            // Lobby Menu 안보여야함
            profileUI.SetActive(true);
            shortcutkeyUI.SetActive(true);
            Manager.Instance.canInputKey = true;
        }
    }

    private void ToggleUI(GameObject ui, bool topView)
    {
        if (ui.activeSelf)
        {
            HideUI(ui);
        }
        else
        {
            ViewUI(ui, topView);
        }
    }

    // GameUI에 SiblingLastIndex(제일 마지막에 실행한, 가장 앞에 보이는)인 UI를 넘겨준다.
    private GameObject HideLastIndexUI()
    {
        GameObject lastUI = null;
        int lastIndex = -999;

        // GameUI List의 UI들의 정보
        foreach (var ui in GameUI)
        {
            // UI가 켜져있다면 실행
            if (ui.activeSelf)
            {
                // 부모 Object의 Index값 받기
                int siblingIndex = ui.transform.parent.GetSiblingIndex();

                // 가장 높은 Index값 정하기
                if (siblingIndex > lastIndex)
                {
                    lastIndex = siblingIndex;
                    lastUI = ui;
                }
            }
        }

        if (rankUI.activeSelf)
            lastUI = rankUI;

        return lastUI;
    }

    // OptionUI에서 SettingUI를 실행
    public void ViewUI(GameObject viewUI)
    {
        var root = viewUI.transform.parent;
        root.transform.SetAsLastSibling();

        Manager.Instance.Sound.SetAudioAudioPath(AudioType.UICalling, "UI/UI_OpenAndClose");

        viewUI.SetActive(true);
    }

    public void ViewUI(GameObject viewUI, bool topView)
    {
        // topView(UI중 가장 앞에 보이도록)
        if (topView)
        {
            // UI들은 전부 자식 Object이기에 부모 Object의 값을 가져온다.
            var root = viewUI.transform.parent;
            // Hierachy의 부모 Object의 순서를 가장 아래로 옮긴다.
            root.transform.SetAsLastSibling();
        }

        // 소리 재생
        Manager.Instance.Sound.SetAudioAudioPath(AudioType.UICalling, "UI/UI_OpenAndClose");

        // 보이도록 하기
        viewUI.SetActive(true);
    }

    public void HideUI(GameObject hideUI)
    {
        hideUI.SetActive(false);

        Manager.Instance.Sound.SetAudioAudioPath(AudioType.UICalling, "UI/UI_OpenAndClose");
    }

    public void LobbyScene()
    {
        lobbyUI.SetActive(true);
    }
    public void OUTLobbyScene()
    {
        lobbyUI.SetActive(false);
    }
}
