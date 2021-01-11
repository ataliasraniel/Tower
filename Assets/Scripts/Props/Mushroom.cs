using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int explosionDamage = 25;
    public float timeToExplode = 1;
    public GameObject explosionFX;
    public bool isExplosive = false;
    private bool isInside = false;

    private LifeSystem lifesystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
            lifesystem = other.GetComponent<LifeSystem>();
            if (isExplosive)
            {
                StartCoroutine(WaitToExplode());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
            AudioManager.instance.Stop();
            StopCoroutine(WaitToExplode());
        }
    }
    private IEnumerator WaitToExplode()
    {
        AudioManager.instance.Play("ThickingBombSFX");
        yield return new WaitForSeconds(timeToExplode);
        Explode();
        
    }
    private void Explode()
    {
        if (isInside)
        {
            AudioManager.instance.Play("PoisonExplosionSFX");
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            CameraShakeManager.instance.ShakeDamageCamera();
            lifesystem.TakeDamage(explosionDamage);
            Destroy(this);
        }
        

    }

}
