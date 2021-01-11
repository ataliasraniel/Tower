using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mage : MonoBehaviour
{

    [Header("Animations")]
    public Animation anim;
    public GameObject deathFX;

    [Header("General")]
    public bool isLeft;
    public float movementSpeed;
    public float timeToChangeDirection;
    public float minDisplacementx= 1;
    public float maxDisplacementy = 1;
    public float directionX;

    [Header("Attack")]
    public bool canAttack = true;
    public float timeBtwAttacks = 1;
    public float attackThreshold;
    public Transform playerPos;
    public Transform attackPos;     
    public GameObject attackPF;
    public float delay;

    public bool diying = false;
    public float timer;
    private BoxCollider headCollider;
    private BoxCollider bodyCollider;
    private EnemyLifeSystem lifesystem;

    public enum EnemyState
    {
        idle,
        patrolling,
        attacking,
        dying
    };

    public EnemyState enemyStates;


    private void Start()
    {
        lifesystem = GetComponent<EnemyLifeSystem>();
        lifesystem.OnEnemyDeath += Death;
        playerPos = FindObjectOfType<Player>().GetComponent<Transform>();
        headCollider = transform.GetChild(2).GetComponent<BoxCollider>();
        bodyCollider = transform.GetChild(1).GetComponent<BoxCollider>();
    }
    

    private void Update()
    {
        
        float distance = Vector3.Distance(transform.position, playerPos.position);
        if (distance >= attackThreshold)
        {
            enemyStates = EnemyState.patrolling;
        }
        else enemyStates = EnemyState.attacking;



        switch (enemyStates)
        {
            case EnemyState.idle:
                Idle();
                break;
            case EnemyState.patrolling:
                Patrolling();
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
    private void Idle()
    {
        anim.Play("idle");
    }
    private IEnumerator Attack()
    {
        
        if (canAttack)
        {
            canAttack = false;
            anim.Play("attack_short_001");
            yield return new WaitForSeconds(anim.GetClip("attack_short_001").length);
            attackPos.LookAt(playerPos.position);
            AudioManager.instance.Play("FireballSFX");
            var clone = Instantiate(attackPF, attackPos.position, attackPos.rotation);
            canAttack = true;
        }
        
        
    }
    private void Patrolling()
    {
        anim.Play("move_forward_fast");
        timer += Time.deltaTime;
        if(timer >= timeToChangeDirection)
        {
            timer = 0;
            directionX *= -1;
        }
        if (isLeft)
        {
            transform.Translate(new Vector3(-directionX, 0, 0) * 5 * Time.deltaTime);
        }else transform.Translate(new Vector3(directionX, 0, 0) * 5 * Time.deltaTime);

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
