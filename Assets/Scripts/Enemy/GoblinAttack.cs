using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LifeSystem lifesystem = other.GetComponent<LifeSystem>();
            if (lifesystem)
            {
                lifesystem.TakeDamage(damage);
                Destroy(gameObject);
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
