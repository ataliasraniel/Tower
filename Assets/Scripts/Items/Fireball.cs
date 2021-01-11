using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int fireballDamage = 5;
    public float speed = 5;
    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LifeSystem lifesystem = other.GetComponent<LifeSystem>();
            if (lifesystem)
            {
                lifesystem.TakeDamage(fireballDamage);
            }
            Destroy(gameObject);
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }
}
