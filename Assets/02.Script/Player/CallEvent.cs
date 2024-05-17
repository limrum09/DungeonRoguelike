using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour
{
    public PlayerController player;

    public void MoveForward()
    {
        Vector3 moveDirection = transform.forward * 0.1f;
        GetComponent<CharacterController>().Move(moveDirection);
    }

    public void AttackEndEvent()
    {
        player.isCombo = false;
        player.playerState = PlayerState.Idel;
        player.animator.SetBool("IsAttack", false);
    }

    public void DieEnd()
    {
        player.animator.SetBool("Die", false);
    }

    public void JumpEnd()
    {
        player.animator.SetBool("Jump", false);
        player.animator.SetBool("DoubleJump", false);
    }
}
