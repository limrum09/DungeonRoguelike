using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    [SerializeField]
    private SaveDatabase saveManager;
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private GameManager gameManager;

    public GameManager Game => gameManager;
    public SoundManager Sound => soundManager;

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
        Instantiate(gameManager, this.transform);
        gameManager.GameManagerStart();

        Instantiate(soundManager, this.transform);
        soundManager.SoundManagerStart();

        Instantiate(saveManager, this.transform);
        saveManager.SaveDatabaseStart();
    }
}
