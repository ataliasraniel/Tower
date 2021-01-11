using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fonte : MonoBehaviour
{
    public void Heal()
    {
        var vida = FindObjectOfType<LifeSystem>();
        vida.Heal(50);
        AudioManager.instance.Play("HealSFX");
        Destroy(gameObject);
    }
}
