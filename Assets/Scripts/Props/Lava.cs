using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int lavaDamage = 20;
    public LayerMask whatToDamage;
    private void OnTriggerStay(Collider other)
    {
        
        if (((1 << other.gameObject.layer) & whatToDamage) != 0 & other.gameObject != null)
        {
            print("dano!");
            var lifesistem = other.gameObject.GetComponent<LifeSystem>();
            if (lifesistem)
            {
                lifesistem.TakeDamage(lavaDamage);
            }
            else return;
        }
    }
}
