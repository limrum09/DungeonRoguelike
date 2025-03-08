using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSkillSetPosition : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;          // 땅의 레이어 
    [SerializeField]
    private GameObject skillIndexObject;    // 타켓팅 시, 마우스 위치를 알려준다. 
    [SerializeField]
    private PlayerController player;

    private Camera playerCamera;                  // 현제 카메라
    private bool isTarget;
    private float skillRange;
    private float skillTimer;

    private ActiveSkill skill;

    // 타켓팅 시작
    public void StartTargeting(ActiveSkill _skill)
    {
        skill = _skill;
        playerCamera = Manager.Instance.Camera.CurrentCamera;
        skillIndexObject.gameObject.SetActive(true);
        isTarget = true;
        skillRange = _skill.SkillMoveRange;
        skillTimer = 3.0f;
        Manager.Instance.Game.PlayerController.StartTargetingSkill();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Manager.Instance.Camera.CurrentCamera;
        skillIndexObject.gameObject.SetActive(false);
        isTarget = false;
        skill = null;
        skillTimer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTarget)
        {
            // 왼쪽 마우스 버튼 클릭시, 해당 위치로 skill정보와 위치값 전송
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("스킬 사용------------");
                CheckTargetingPosition();
            }

            UpdateSkillIndexObejctPos();

            skillTimer -= Time.deltaTime;
        }
    }

    private void UpdateSkillIndexObejctPos()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 tempskillPosition = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);

            // Player와 거리 구하기
            float distanceFromPlayer = Vector3.Distance(player.transform.position, tempskillPosition);

            // 임시 스킬 사용 위치가 SkillRange보다 벗어 날 경우
            if (distanceFromPlayer > skillRange)
            {
                // Player와 스킬 위치까지 방향 잡기
                Vector3 direction = (tempskillPosition - player.transform.position).normalized;

                // 거리 줄이기
                tempskillPosition = player.transform.position + direction * skillRange;
            }

            // 스킬 사용 위치 업데이트
            skillIndexObject.transform.position = tempskillPosition;

            player.RotatePlayerToMousePos(skillIndexObject.transform.position);

            Debug.Log("스킬 이밎 위치 : " + skillIndexObject.transform.position);
        }

        // 스킬 타이머가 0이 되면 스킬 사용 취소
        if (skillTimer <= 0.0f)
            ResetTargeting();
    }

    private void CheckTargetingPosition()
    {
        // 스킬 사용 시작
        Manager.Instance.Game.PlayerController.ActionTargetingSkill(skill, skillIndexObject.transform);

        ResetTargeting();
    }

    // 타켓팅 리셋
    private void ResetTargeting()
    {
        skillIndexObject.SetActive(false);
        isTarget = false;
        skill = null;
        Manager.Instance.Game.PlayerController.EndTagetingSkill();

        Debug.Log("스킬 종료 이후 이미지 위치 : " + skillIndexObject.transform.position);
    }
}
