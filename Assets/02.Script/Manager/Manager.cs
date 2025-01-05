using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    [SerializeField]
    private QuestSystem questSystem;
    [SerializeField]
    private SaveDatabase saveManager;
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private InputKey inputKey;
    [SerializeField]
    private CameraController camearaController;

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private UIAndSceneManager uIAndSceneManager;
    [SerializeField]
    private TransparentObjectManager transparentManager;
    [SerializeField]
    private UserInfo userInfo;

    public QuestSystem Quest => questSystem;
    public SaveDatabase Save => saveManager;
    public GameManager Game => gameManager;
    public SoundManager Sound => soundManager;
    public InputKey Key => inputKey;
    public CameraController Camera => camearaController;
    public TransparentObjectManager TransParent => transparentManager;
    public UserInfo UserInfoData => userInfo;

    public UIAndSceneManager UIAndScene => uIAndSceneManager;

    public bool canUseShortcutKey;

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("Manager");
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Manager Destroyed");
            Destroy(this.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        saveManager.DataSaving();
    }

    private void Start()
    {
        canUseShortcutKey = false;

        questSystem = Instantiate(questSystem, this.transform);

        gameManager = Instantiate(gameManager, this.transform);
        gameManager.GameManagerStart();

        soundManager = Instantiate(soundManager, this.transform);
        soundManager.SoundManagerStart();

        inputKey = Instantiate(inputKey, this.transform);
        inputKey.InputKeyStart();

        uIAndSceneManager = Instantiate(uIAndSceneManager, this.transform);
        uIAndSceneManager.UIAndSceneManagerStart();

        saveManager = Instantiate(saveManager, this.transform);
        saveManager.SaveDatabaseStart();

        // camearaController = Instantiate(camearaController, this.transform);
        camearaController.CameraStart();

        uIAndSceneManager.viewAndHide.CheckCurrentScene();

        transparentManager = Instantiate(transparentManager, this.transform);

        userInfo = Instantiate(userInfo, this.transform);
        userInfo.GetUserInfoFromBackend();

        Resources.UnloadUnusedAssets();
    }
}

/// <summary>
/// 어떤 값이 들어와도 값이 있다면, 해당 Enum 값을 반환
/// </summary>
/// <typeparam name="T"></typeparam>
public static class EnumUntil<T>
{
    public static T Parse(string s)
    {
        // 값이 있는지 확인 및 유효성 검사
        // Enum.TryParse(Enum 타입, string? value, bool 대,소문자 구분 여부, out var 결과)
        if (Enum.TryParse(typeof(T), s, true, out var result))
        {
            return (T)result;
        }
        else
        {
            throw new ArgumentException($"{typeof(T).Name}의 Enum에 {s}값이 없습니다.");
        }
    }

    public static T Parse(int i)
    {
        // Enum 값으로 변환
        T enumValue = (T)Enum.ToObject(typeof(T), i);

        // 유효성 확인
        if(!Enum.IsDefined(typeof(T), enumValue))
        {
            throw new ArgumentException($"{typeof(T).Name}의 Enum에 {i}값을 지닌 값이 없습니다.");
        }

        return enumValue;
    }
}
