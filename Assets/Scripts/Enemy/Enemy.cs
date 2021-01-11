using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int HP;
    private EnemyLifeSystem lifesystem;

    [Header("Animacoes")]
    private Animation anim;
    public string deathAnimName;
    public string attackAnimName;

    [Header("Efeitos")]
    public GameObject deathFX;

    [Header("Audio")]
    public string deathSFXName;
    private void Start()
    {
        lifesystem = GetComponent<EnemyLifeSystem>();
        lifesystem.OnEnemyDeath += Death;
        anim = GetComponent<Animation>();
    }
    public void Death()
    {
        anim.Play(deathAnimName);
        LevelManager.instance.CountDeaths();
        AudioManager.instance.Play(deathSFXName);
        lifesystem.OnEnemyDeath -= Death;
        StartCoroutine(WaitToAnimFishn());
        
    }
    private IEnumerator WaitToAnimFishn()
    {
        float duration = anim.GetClip(deathAnimName).length;
        yield return new WaitForSeconds(duration);
        Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
