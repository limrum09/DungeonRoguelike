using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvasController : MonoBehaviour
{
    public Image currentHPBar;

    [SerializeField]
    private EnemyStatus enemyStatus;
    private Canvas enemyCanvas;
    private GameObject playerCamera;

    private Vector2 lastHPSize;
    private float resetDamageTime;
    private float currentDamageTime;

    public float breakDamageTimer;
    // Start is called before the first frame update
    void Start()
    {
        enemyCanvas = GetComponent<Canvas>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        lastHPSize = currentHPBar.rectTransform.sizeDelta;

        enemyCanvas.enabled = false;
        currentDamageTime = 0.0f;
        resetDamageTime = 0.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        LookCamera();
        EnemyHPBar();
    }

    private void EnemyHPBar()
    {
        if(lastHPSize.x > currentHPBar.rectTransform.sizeDelta.x)
        {
            enemyCanvas.enabled = true;

            resetDamageTime += Time.deltaTime;

            if (resetDamageTime >= 1.0f)
            {
                lastHPSize = currentHPBar.rectTransform.sizeDelta;
                currentDamageTime = 0.0f;
            }
        }
        else if(lastHPSize == currentHPBar.rectTransform.sizeDelta)
        {
            currentDamageTime += Time.deltaTime;

            if(currentDamageTime >= breakDamageTimer)
            {
                currentDamageTime = 0.0f;

                enemyCanvas.enabled = false;
            }
        }
        
        currentHPBar.rectTransform.sizeDelta = new Vector2((float)enemyStatus.CurrentHP / (float)enemyStatus.MaxHP * 2.5f, currentHPBar.rectTransform.sizeDelta.y);
    }

    private void LookCamera()
    {        
        this.transform.LookAt(playerCamera.transform);
    }
}
