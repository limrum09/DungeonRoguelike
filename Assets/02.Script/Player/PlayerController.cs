using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState
{
    Idel,
    Move,
    Attack,
    Jump,
    Hurt,
    Skill,
    Dead
}

public class PlayerController : MonoBehaviour
{
    [Header("States")]
    [SerializeField]
    public PlayerState playerState = PlayerState.Idel;
    [SerializeField]
    private GroundCheck groundCheck;

    private CharacterController controller;
    private Vector3 playerVector;
    [SerializeField]
    private Animator animator;

    [Header("Status Value")]
    [SerializeField]
    private float playerSpeed;
    [SerializeReference]
    private float rotationSpeed;
    [SerializeField]
    private float jumpHeigt;
    [SerializeField]
    private float gravityValue;
    [SerializeField]
    private bool isGround;
    public bool isMove;
    public bool isCombo;

    [Header("Cinemachine")]
    [SerializeField]
    private GameObject playerCollider;
    [SerializeField]
    private Transform respawnPoint;

    [Header("Skill")]
    [SerializeField]
    private MeshCollider playerAttackCollider;
    [SerializeField]
    private SkillEffectController skillEffectController;
    [SerializeField]
    private TargetingSkillSetPosition targetingSkill;
    [SerializeField]
    private GameObject weaponPosEffect;
    [SerializeField]
    private GameObject centerPosEffect;

    private InputKey key;
    private Camera currentCamera;
    private bool isDoubleJump;
    public Animator Ani => animator;

    private float sceneLoadTimer;
    private bool sceneChagne;
    private bool isKeyInput;

    // Start is called before the first frame update
    public void PlayerControllerStart()
    {
        controller = GetComponent<CharacterController>();

        PlayerInRespawnPoint();
        isMove = true;
        isCombo = false;
        sceneChagne = false;
    }

    public void SceneChanging()
    {
        PlayerInRespawnPoint();

        isGround = false;
        isMove = false;
        playerState = PlayerState.Idel;
        animator.SetBool("Walk", false);
        animator.SetBool("Sprint", false);
        animator.SetBool("Jump", false);
        playerVector = Vector3.zero;
    }

    public void PlayerInRespawnPoint()
    {
        controller.enabled = false;
        respawnPoint = GameObject.FindGameObjectWithTag("PlayerRespawnPoint").transform;
        transform.position = respawnPoint.position;
        controller.transform.position = respawnPoint.position;
        sceneChagne = true;
        sceneLoadTimer = 0.0f;
        controller.enabled = true;
        Debug.Log("플레이어 리스폰 지역" + respawnPoint.transform.position);
    }

    // 로딩 UI 까지 없어지고 난 후, 플레이어 움직이기
    public void PlayerCanMove()
    {
        isMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 씬 전환 중
        if (sceneChagne)
        {
            SceneLoadCompleteCheck();
            return;
        }

        if (!PlayerInteractionStatus.instance.isDie)
        {
            GroundCheck();
            PlayerStateCheck();
            isKeyInput = true;
        }
        else
        {
            PlayerDead();
            isKeyInput = false;
        }        
    }

    private void Update()
    {
        if (isKeyInput)
        {
            PlayerJump();
            PlayerAttackCheck();
            PlayerSprintCheck();
        }
    }

    private void SceneLoadCompleteCheck()
    {
        if (!sceneChagne)
            return;

        isKeyInput = false;
        // 씬 전환 완료, 이후 1초동안 리스폰 위치로 플레이어 강제 이동
        respawnPoint = GameObject.FindGameObjectWithTag("PlayerRespawnPoint").transform;

        // CharacterController를 비활성화 후 위치 설정
        // 활성화 되어있으면 순간적으로 위치만 바뀌고 prefab에 입력되어있는 position으로 이동함
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.transform.position = respawnPoint.position;
        controller.enabled = true;

        sceneLoadTimer += Time.deltaTime;

        Manager.Instance.UIAndScene.LoddingUI.LoddingRateValue("던전 가는 중...!", 75.0f);

        // 씬 로드가 완료되고 1.5초 뒤에 플레이어 이동가능
        // 스폰 포인트가 대체로 공중에 떠있기 때문에 플레이어가 떨어지는 시간이 필요함
        if (sceneLoadTimer >= 1.5f)
        {
            // 값이 100이 되어야, LoddingUI가 종료됨
            Manager.Instance.UIAndScene.LoddingUI.LoddingRateValue("던전 도착!", 100.0f);
            sceneChagne = false;
            isKeyInput = true;
        }
    }

    private void PlayerSprintCheck()
    {
        // 달리기
        if (Input.GetKey(key.GetKeyCode("Sprint")))
        {
            playerSpeed = playerSpeed * 1.5f;
            animator.SetBool("Sprint", true);
        }
        else
        {
            animator.SetBool("Sprint", false);
        }
    }

    private void PlayerAttackCheck()
    {
        // EventSystem.current.IsPointerOverGameObject() <= UI 클릭시 호출
        // UI를 클릭하지 않고, 점프상태가 아닌 경우
        bool isAttack = !EventSystem.current.IsPointerOverGameObject() && !animator.GetBool("Jump") && (Input.GetKeyDown(key.GetKeyCode("Attack")) || Input.GetMouseButtonDown(0));
        if (isAttack)
        {
            playerState = PlayerState.Attack;
            isCombo = false;
        }
    }

    private void PlayerStateCheck()
    {
        // 추가 확인 필요 : 중력 및 위치 변경이 중복으로 발생하는지 확인
        if (playerState == PlayerState.Move || playerState == PlayerState.Idel || playerState == PlayerState.Skill)
        {
            playerMove();
        }
        else if (playerState == PlayerState.Attack)
        {
            PlayerAttack();
        }
    }

    private void playerMove()
    {
        if (!isMove || sceneChagne)
            return;

        key = Manager.Instance.Key;

        // 방향키
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerSpeed = PlayerInteractionStatus.instance.PlayerSpeed;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // 입력한 값이 있는 경우
        if (direction.magnitude >= 0.1f)
        {
            if (isGround)
            {
                isDoubleJump = false;
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
            currentCamera = Manager.Instance.Camera.CurrentCamera;

            // 입력한 값을 기준으로 회전 각도 계산
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + currentCamera.transform.eulerAngles.y;

            // 현제 각도를 목표 각도로 보간
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

            // 목표 각도로 플레이어 회전
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // 플레이어 이동 방향
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            // 캐릭터 컨트롤러로 플레이어 이동
            controller.Move(moveDirection * playerSpeed * Time.deltaTime);

            // Cinemachine을 playerCollider로 잡았음. 플레이어가 회전해도 playerCollider가 회전하지 않는다.
            playerCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

            Manager.Instance.Game.PlayerMoveTransform();
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void PlayerJump()
    {
        // 일반 점프
        if (isGround && Input.GetKeyDown(key.GetKeyCode("Jump")) && !animator.GetBool("Jump"))
        {
            playerVector.y = Mathf.Sqrt(jumpHeigt * -3.0f * gravityValue);
            animator.SetBool("Jump", true);
        }
        // 더블 점프
        else if (animator.GetBool("Jump") && Input.GetKeyDown(key.GetKeyCode("Jump")) && !isDoubleJump)
        {
            isDoubleJump = true;
            playerVector.y = Mathf.Sqrt(jumpHeigt * -3.0f * gravityValue);
            animator.SetBool("DoubleJump", true);
        }

        // 중력
        playerVector.y += gravityValue * Time.deltaTime;
        controller.Move(playerVector * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGround = groundCheck.IsGround();
        if (isGround)
            animator.SetBool("JumpDown", true);

        if (isGround && playerVector.y < 0)
        {
            isDoubleJump = false;
            animator.SetBool("Jump", false);
            playerVector.y = 0f;
        }
    }

    private void PlayerAttack()
    {
        isMove = false;
        if (!isCombo)
        {
            isCombo = true;
            animator.SetBool("IsAttack", true);
        }   
    }

    private void PlayerDead()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("DoubleJump", false);
        animator.SetBool("Walk", false);
        animator.SetBool("IsAttack", false);
        animator.SetBool("Die", true);
    }

    #region UsingActiveSkill
    public void InputActiveSkill(ActiveSkill skill)
    {
        if ((skill.RightWeaponValue != animator.GetInteger("RightWeaponValue") || skill.LeftWeaponValue != animator.GetInteger("LeftWeaponValue")) && skill.WeaponValue != SkillWeaponValue.Public)
            return;

        if (!skill.NeedLevelCondition || !skill.NeedSkillCondition)
            return;

        skill.UseSkill();
        isMove = skill.CanMove;
        animator.SetBool("IsSkill", true);

        if (skill.Targeting)
        {
            targetingSkill.StartTargeting(skill);
        }
        else
        {
            skillEffectController.ActiveSkillEffect(skill);
        }

        SkillAnimation(skill);
    }

    public void SkillAnimation(ActiveSkill skill)
    {
        string aniName = skill.AnimationName;

        // 공용 스킬일 경우 무기의 종류에 따라서 스킬의 이름의 일부분이 다르기 때문에 알맞게 추가한다.
        if (skill.WeaponValue == SkillWeaponValue.Public)
        {
            aniName = PublicSrkillAnimName(aniName);
        }

        animator.Play(aniName);
        playerState = PlayerState.Skill;

        Debug.Log("스킬 " + aniName + " 사용");
    }

    public void ActionTargetingSkill(ActiveSkill skill, Transform tf)
    {
        skillEffectController.ActiveSkillEffect(skill, tf);
    }

    // 타켓팅 스킬 사용 중, 화면 클릭 시 회전
    public void RotatePlayerToMousePos(Transform getTf)
    {
        Vector3 targetPosition = getTf.position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 플레이어가 Y축으로만 회전 하는지 확인
        direction.y = 0;

        Debug.Log("위치 확인 : " + targetPosition + ", 방향 확인 : " + direction);

        if (direction.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
            Debug.Log("회전 : " + targetRotation);
        }
    }

    public void EndSkill()
    {
        animator.SetBool("IsSkill", false);
        isMove = true;
        playerState = PlayerState.Idel;
    }

    // 공용 스킬에는 Animation의 일부 이름만 다르기에 무기에까라 알맞은 이름 삽입
    private string PublicSrkillAnimName(string aniName)
    {
        string addString = "";
        int rightWeaponValue = animator.GetInteger("RightWeaponValue");
        int leftWeaponValue = animator.GetInteger("LeftWeaponValue");

        if (rightWeaponValue == 1)
        {
            switch (leftWeaponValue)
            {
                case 0:
                    addString = "OneHand_";
                    break;
                case 1:
                    addString = "DoubleSword_";
                    break;
                case 2:
                    addString = "SwordAndSheild_";
                    break;
            }
        }
        else if (rightWeaponValue == 3)
            addString = "THS_";
        else if (rightWeaponValue == 4)
            addString = "Spear_";
        else if (rightWeaponValue == 5)
            addString = "MagicWand_";

        return addString + aniName;
    }
    #endregion
}
