using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    [SerializeField]
    private SaveDatabase saveManager;
    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Manager");
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Manager Destroyed");
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        Instantiate(gameManager, this.transform);
        gameManager.GameManagerStart();

        Instantiate(saveManager, this.transform);
        saveManager.SaveDatabaseStart();
    }
}
