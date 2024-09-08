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
    private GameObject weaponPosEffect;
    [SerializeField]
    private GameObject centerPosEffect;

    private InputKey key;
    private Camera currentCamera;
    private bool isDoubleJump;
    public Animator Ani => animator;

    private float preVelocityY;

    // Start is called before the first frame update
    public void PlayerControllerStart()
    {
        controller = GetComponent<CharacterController>();
        PlayerInRespawnPoint();
        isMove = true;
        isCombo = false;
    }

    public void PlayerInRespawnPoint()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("PlayerRespawnPoint").transform;
        transform.position = respawnPoint.position;
        controller.transform.position = respawnPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerInteractionStatus.instance.isDie)
        {
            isGround = groundCheck.IsGround();
            if(isGround)
                animator.SetBool("JumpDown", true);

            if (isGround && playerVector.y < 0)
            {
                isDoubleJump = false;
                animator.SetBool("Jump", false);
                playerVector.y = 0f;
            }

            switch (playerState)
            {
                case PlayerState.Idel:
                    playerMove();
                    break;
                case PlayerState.Move:
                    playerMove();
                    break;
                case PlayerState.Attack:
                    PlayerAttack();
                    break;
                case PlayerState.Skill:
                    playerMove();
                    break;
            }

            // EventSystem.current.IsPointerOverGameObject() <= UI 클릭시 호출
            if (!EventSystem.current.IsPointerOverGameObject() && !animator.GetBool("Jump") && (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)))
            {
                playerState = PlayerState.Attack;
                isCombo = false;
            }
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Walk", false);
            animator.SetBool("IsAttack", false);

            animator.SetBool("Die", true);
        }
        
    }

    private void playerMove()
    {
        if (!isMove)
            return;

        PlayerJump();

        // 방향키
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        playerSpeed = PlayerInteractionStatus.instance.PlayerSpeed;
        key = Manager.Instance.Key;

        if (Input.GetKey(key.GetKeyCode("Sprint")))
        {
            playerSpeed = playerSpeed * 1.5f;
            animator.SetBool("Sprint", true);
        }
        else
        {
            animator.SetBool("Sprint", false);
        }

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
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void PlayerJump()
    {
        // Jump
        if (isGround && Input.GetKeyDown(key.GetKeyCode("Jump")) && !animator.GetBool("Jump"))
        {
            playerVector.y += Mathf.Sqrt(jumpHeigt * -3.0f * gravityValue);
            animator.SetBool("Jump", true);
        }
        // Double Jump
        else if(animator.GetBool("Jump") && Input.GetKeyDown(key.GetKeyCode("Jump")) && !isDoubleJump)
        {
            isDoubleJump = true;
            playerVector.y += Mathf.Sqrt(jumpHeigt * -3.0f * gravityValue);
            animator.SetBool("DoubleJump", true);
        }

        if (playerVector.y > 0)
        {
            playerVector.y += gravityValue * Time.deltaTime;
            controller.Move(playerVector * Time.deltaTime);

            if (preVelocityY > transform.position.y)
            {
                animator.SetBool("JumpDown", false);
                // 정점
                Debug.Log("정점 인가? " + transform.position.y);
            }


            preVelocityY = transform.position.y;
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

    public void UseActiveSkill(ActiveSkill skill)
    {
        if (skill.RightWeaponValue != animator.GetInteger("RightWeaponValue") && skill.WeaponValue != SkillWeaponValue.Public)
            return;
        isMove = skill.CanMove;
        string aniName = skill.AnimationName;

        // 공용 스킬일 경우 무기의 종류에 따라서 스킬의 이름의 일부분이 다르기 때문에 알맞게 추가한다.
        if (skill.WeaponValue == SkillWeaponValue.Public)
        {
            aniName = PublicSrkillAnimName(aniName);
        }

        skill.UseSkill();
        animator.Play(aniName);
        playerState = PlayerState.Skill;

        if(skill.SkillEffect != null)
        {
            skillEffectController.ActiveSkillEffrct(skill);
        }

        Debug.Log("스킬 " + aniName + " 사용");
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
}
