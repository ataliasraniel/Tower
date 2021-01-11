using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : MonoBehaviour
{
    private Animator anim;

    public enum EnemyStates
    {
        attacking,
        idle,
        chacing
    }
    public EnemyStates states;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("idle", true);
    }
    private void Update()
    {
        switch (states)
        {
            case EnemyStates.attacking:
                Attack();
                break;
            case EnemyStates.idle:
                break;
            case EnemyStates.chacing:
                break;
            default:
                break;
        }
    }
    private void Attack()
    {
        anim.SetTrigger("attack1");
        anim.SetBool("idle", true);
        
        anim.ResetTrigger("attack1");
    }
    

}
