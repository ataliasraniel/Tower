using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitToAttack : MonoBehaviour
{
    private EnemyLifeSystem lifesystem;
    private bool firstHit = false;
    private void Start()
    {
        lifesystem = GetComponent<EnemyLifeSystem>();
    }
    private void Update()
    {
        firstHit = lifesystem.takedFirstHit;
        if (firstHit)
        {
            Wolf wolf = GetComponent<Wolf>();
            wolf.isFirstHit = false;
            Destroy(this);
        }
    }
    
}
