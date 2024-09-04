using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    public void MoveForward()
    {
        Vector3 moveDirection = transform.forward * 0.1f;
        GetComponent<CharacterController>().Move(moveDirection);
    }

    public void AttackEndEvent()
    {
        player.isCombo = false;
        player.isMove = true;
        player.playerState = PlayerState.Idel;
        player.Ani.SetBool("IsAttack", false);
    }

    public void AttackSound(string weapon)
    {
        string path = "Weapon/" + weapon;
        Manager.Instance.Sound.SetAudioAudioPath(AudioType.PlayerAttack, path);
    }

    public void FootSound(string foot)
    {
        string path = "Foot/" + foot;
        Manager.Instance.Sound.SetAudioAudioPath(AudioType.PlayerFoot, path);
    }

    public void DieEnd()
    {
        player.Ani.SetBool("Die", false);
    }

    public void JumpEnd()
    {
        player.Ani.SetBool("Jump", false);
        player.Ani.SetBool("DoubleJump", false);
        player.Ani.SetBool("JumpDown", false);
    }

    public void SkillMove(float move)
    {
        if (move == 1.0f)
            player.isMove = true;
        else
            player.isMove = false;
    }


    public void SkillEnd()
    {
        player.playerState = PlayerState.Idel;
        player.isMove = true;
        player.SkillEffect.Stop();
    }
}
