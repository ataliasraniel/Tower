using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isDead = false;

    [Header("Scripts para desativar")]
    private PlayerMovement _playerMovement;
    private Weapon _playerWeapon;
    private LifeSystem _lifeSystem;

    public GameObject weaponModel;
    public bool isShotgun;

    private BoxCollider _col;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerWeapon = GetComponent<Weapon>();
        _col = GetComponent<BoxCollider>();
        _lifeSystem = GetComponent<LifeSystem>();

    }

    public void OnPlayerDeath()
    {
        isDead = true;
        if (isDead)
        {
            if (isShotgun)
            {
                weaponModel = GameObject.Find("Weapon Model Shotgun");
                Destroy(weaponModel);
            }
            else
            {
                weaponModel = GameObject.Find("Weapon Model");
                Destroy(weaponModel);
            }
            _playerMovement.enabled = false;
            _playerWeapon.enabled = false;
            _col.enabled = false;
            Destroy(_lifeSystem);
            _lifeSystem.enabled = false;
        }
            
    }
}
