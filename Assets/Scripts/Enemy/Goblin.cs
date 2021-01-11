using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Goblin : MonoBehaviour
{
    private Animation anim;

    [Header("General")]
    public bool isFirstHit = false;
    public float movementSpeed;
    public float timeToChangeDirection;
    public float minDisplacementx = 1;
    public float maxDisplacementy = 1;
    public float directionX;

    [Header("Attack")]
    public bool canAttack = true;
    public bool attacking = false;
    public int damage;
    public float timeBtwAttacks = 1;
    public float attackThreshold;
    private Transform playerPos;
    public Transform attackPos;
    public GameObject attackPF;
    public float delay;

    public bool diying = false;
    public float timer;
    private BoxCollider headCollider;
    private BoxCollider bodyCollider;
    private EnemyLifeSystem lifesystem;



    private void Start()
    {
        lifesystem = GetComponent<EnemyLifeSystem>();
        anim = GetComponent<Animation>();
        AudioManager.instance.Play("GoblinSpawnSFX");
        lifesystem.OnEnemyDeath += Death;
        playerPos = FindObjectOfType<Player>().GetComponent<Transform>();
        headCollider = transform.GetChild(2).GetComponent<BoxCollider>();
        bodyCollider = transform.GetChild(1).GetComponent<BoxCollider>();
    }



    public enum EnemyState
    {
        idle,
        chacing,
        attacking,
        dying
    };

    public EnemyState enemyStates;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Damage();
        }
        if (isFirstHit) return;
        StateController();

        switch (enemyStates)
        {
            case EnemyState.idle:
                IdleState();
                break;
            case EnemyState.chacing:
                Chacing();
                break;
            case EnemyState.attacking:
                transform.DOLookAt(playerPos.position, 0.1f);
                StartCoroutine(Attack());
                break;
            case EnemyState.dying:
                Death();
                break;
            default:
                break;
        }
    }
    private void StateController()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);
        if (distance >= attackThreshold)
        {
            enemyStates = EnemyState.chacing;
        }
        else enemyStates = EnemyState.attacking;
    }
    private void IdleState()
    {
        anim.Play("idle01");
    }
    private void Damage()
    {
        anim.CrossFade("damage", 0.1f);
    }
    private IEnumerator Attack()
    {

        if (canAttack)
        {
            canAttack = false;
            attacking = true;
            anim.CrossFade("attack02", 0.2f);
            AudioManager.instance.Play("GoblinAttackSFX");
            yield return new WaitForSeconds(anim.GetClip("attack02").length);
            attacking = false;
            var clone = Instantiate(attackPF, attackPos.position, attackPos.rotation);
            clone.GetComponent<GoblinAttack>().damage = damage;
            canAttack = true;
        }


    }
    private void Chacing()
    {
        if (attacking) return;
        anim.Play("run");
        transform.DOLookAt(playerPos.position, 0.2f);
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position,
            movementSpeed);

    }
    private void Death()
    {
        lifesystem.OnEnemyDeath -= Death;
        Destroy(headCollider);
        Destroy(bodyCollider);
        Enemy enemy = GetComponent<Enemy>();
        enemy.Death();
        Destroy(this);
    }
}
