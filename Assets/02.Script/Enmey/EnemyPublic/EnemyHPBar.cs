using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    private EnemyStatus enemyStatus;
    public Image currentHPBar;
    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHPBar.rectTransform.sizeDelta = new Vector2((float)enemyStatus.CurrentHP / (float)enemyStatus.MaxHP * 2.5f, currentHPBar.rectTransform.sizeDelta.y);
    }
}
