using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetecting : MonoBehaviour
{
    [SerializeField]
    private EnemyCollider enemyCollider;
    [SerializeField]
    private float detectRadius;
    [SerializeField]
    private LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDetectPlayer());
    }

    // 일정 범위 안에 플레이어가 있는지 확인
    private float CheckPlayerInBossArea()
    {
        float detectTime = 3.0f;

        // 일정한 번위에서 플레이어를 찾는 로직 필요
        Collider[] detectPlayer = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        if (detectPlayer.Length > 0)
        {
            detectTime = 10.0f;
            enemyCollider.CheckMosterArea(true);
            enemyCollider.DetectPlayer(true);

            Debug.Log("플레이어 감지");
        }
        else
        {
            enemyCollider.CheckMosterArea(false);
            enemyCollider.DetectPlayer(false);
            
            Debug.Log("플레이어가 없다");
        }
            

        return detectTime;
    }

    // 일정 시간마다 CheckPlayerInBossArea를 호출하는 코루틴
    private IEnumerator AutoDetectPlayer()
    {
        while (true)
        {
            float coolTime = CheckPlayerInBossArea();

            yield return new WaitForSecondsRealtime(coolTime);
        }
    }
}
