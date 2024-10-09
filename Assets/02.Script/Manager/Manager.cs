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
    private UIAndSceneManager uiAndSceneManager;

    public QuestSystem Quest => questSystem;
    public GameManager Game => gameManager;
    public SoundManager Sound => soundManager;
    public InputKey Key => inputKey;
    public CameraController Camera => camearaController;

    public UIAndSceneManager UIAndScene => uiAndSceneManager;

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
        questSystem = Instantiate(questSystem, this.transform);

        gameManager = Instantiate(gameManager, this.transform);
        gameManager.GameManagerStart();

        soundManager = Instantiate(soundManager, this.transform);
        soundManager.SoundManagerStart();

        inputKey = Instantiate(inputKey, this.transform);
        inputKey.InputKeyStart();

        uiAndSceneManager = Instantiate(uiAndSceneManager, this.transform);
        uiAndSceneManager.UIAndSceneManagerStart();

        saveManager = Instantiate(saveManager, this.transform);
        saveManager.SaveDatabaseStart();

        camearaController = Instantiate(camearaController, this.transform);
        camearaController.CameraStart();

        uiAndSceneManager.viewAndHide.CheckCurrentScene();
    }
}
