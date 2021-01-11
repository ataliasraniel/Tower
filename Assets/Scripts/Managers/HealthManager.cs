using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public static HealthManager _instance { get { return instance; } }

    public int maxHealth;
    public int health;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    private void Start()
    {
        LifeSystem life = FindObjectOfType<LifeSystem>();
        health = life.hp;
    }
    public void DestroyHealth()
    {
        Destroy(this.gameObject);
    }
}
