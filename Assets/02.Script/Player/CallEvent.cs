using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    public void CallEventStart()
    {
        player = Manager.Instance.Game.PlayerController;
    }

    public void MoveForward()
    {
        Vector3 moveDirection = transform.forward * 0.1f;
        GetComponent<CharacterController>().Move(moveDirection);
    }

    public void AttackEndEvent()
    {
        player.CallEndAttackEvent();
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

    public void SwimSound(string swim)
    {
        Debug.Log("수영");
        string path = "Foot/" + swim;
        Manager.Instance.Sound.SetAudioAudioPath(AudioType.PlayerFoot, path);
    }

    public void SkillSound(string skill)
    {
        string path = "Skill/" + skill;
        Manager.Instance.Sound.SetAudioAudioPath(AudioType.PlayerAttack, path);
    }

    public void DieEnd()
    {
        player.DieEnd();
    }

    public void JumpEnd()
    {
        player.JumpEnd();
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
        player.SkillEnd();
    }
}
