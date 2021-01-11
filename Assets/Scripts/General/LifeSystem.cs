using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public int hp = 100;
    public bool imortal = false;

    public LayerMask damagebleMask;

    private void Start()
    {
        hp = HealthManager.instance.health;
        GameUIManager.instance.HealthBarController(hp);
    }
    public void TakeDamage(int amount)
    {
        if (imortal) return;
        hp -= amount;
        UpdateHealth();
        GameUIManager.instance.ShowPanelDamage();
        GameUIManager.instance.HealthBarController(hp);
        CameraShakeManager.instance.ShakeDamageCamera();
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        hp += amount;
        GameUIManager.instance.HealthBarController(hp);
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        HealthManager.instance.health = hp;
    }
    public void Die()
    {
        //implementar lógica da morte
        GameManager.instance.PlayerDeath();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & damagebleMask) != 0)
        {
        }
    }
}
