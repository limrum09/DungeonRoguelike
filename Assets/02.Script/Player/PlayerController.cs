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
    public bool isCombo;

    private bool isDoubleJump;
    public Animator Ani => animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        isCombo = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerInteractionStatus.instance.isDie)
        {
            isGround = groundCheck.IsGround();
            if (isGround && playerVector.y < 0)
            {
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * vertical;

        playerSpeed = PlayerInteractionStatus.instance.PlayerSpeed;
        controller.Move(moveDirection * playerSpeed * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(0, horizontal * rotationSpeed, 0);

        if (isGround)
        {
            isDoubleJump = false;
            if (horizontal < 0)
            {
                animator.SetFloat("Rotation", -1);
            }
            else if (horizontal > 0)
            {
                animator.SetFloat("Rotation", 1);
            }
            else if (horizontal == 0)
            {
                animator.SetFloat("Rotation", 0);
            }

            if (vertical < 0)
            {
                animator.SetFloat("Forward", -1);
            }
            else if (vertical >= 0)
            {
                animator.SetFloat("Forward", 0);
            }

            if (new Vector3(horizontal, 0, vertical) != Vector3.zero)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        

        PlayerJump();
    }

    private void PlayerJump()
    {
        // Jump
        if (isGround && Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("Jump"))
        {
            playerVector.y += Mathf.Sqrt(jumpHeigt * -3.0f * gravityValue);
            animator.SetBool("Jump", true);
        }
        // Double Jump
        else if(animator.GetBool("Jump") && Input.GetKeyDown(KeyCode.Space) && !isDoubleJump)
        {
            isDoubleJump = true;
            playerVector.y += Mathf.Sqrt(jumpHeigt * -3.0f * gravityValue);
            animator.SetBool("DoubleJump", true);
        }

        playerVector.y += gravityValue * Time.deltaTime;
        controller.Move(playerVector * Time.deltaTime);
    }

    private void PlayerAttack()
    {
        if (!isCombo)
        {
            isCombo = true;
            animator.SetBool("IsAttack", true);

            // 앞으로 조금 전진
            // Vector3 moveDirection  = transform.forward *  0.1f;
            // controller.Move(moveDirection);

            PlayerComboEnd();
        }
        
    }

    private void PlayerComboEnd()
    {
        //isCombo = false;
    }

    
}
