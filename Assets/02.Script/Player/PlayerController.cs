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
    [SerializeField]
    public PlayerState playerState = PlayerState.Idel;

    public GameObject groundCheckPos;
    private GroundCheck groundCheck;
    private CharacterController controller;
    private Vector3 playerVector;
    public Animator animator;

    [SerializeField]
    private bool isGround;
    [SerializeField]
    private float playerSpeed;
    [SerializeReference]
    private float rotationSpeed;
    [SerializeField]
    private float jumpHeigt;
    [SerializeField]
    private float gravityValue;
    public bool isCombo;

    private bool isDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = groundCheckPos.GetComponent<GroundCheck>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isCombo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerStatus.instance.isDie)
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
                    PlayerCombo();
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
            animator.SetBool("Die", true);
            //animator.SetTrigger("TDie");
        }
        
    }

    private void playerMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * vertical;

        playerSpeed = PlayerStatus.instance.PlayerSpeed;
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

    private void PlayerCombo()
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
