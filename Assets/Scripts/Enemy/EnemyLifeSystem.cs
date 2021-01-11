using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyLifeSystem : MonoBehaviour
{
    public delegate void EnemyDeath();
    public event EnemyDeath OnEnemyDeath;

    public GameObject hitFX;


    public int health = 100;
    public LayerMask whatIsDamageble;
    public bool takedFirstHit = false;
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        health = enemy.HP;
    }
    public void TakeDamage(int amount)
    {
        if (!takedFirstHit)
        {
            takedFirstHit = true;
        }
        health -= amount;
        
        if (health <= 0)
        {
            Die();
        }
    }
    public void HitFX(Vector3 pos)
    {
        if (hitFX != null)
        {
            Instantiate(hitFX, pos, Quaternion.identity);
        }
        
    }
    public void TakeHeadDamage(int amount)
    {
        if (!takedFirstHit)
        {
            takedFirstHit = true;
        }
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath();
        }
        Destroy(this);
    }
}
