using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public Weapon weapon;
    public Shotgun shotGun;

    public GameObject shotGunWeapon;
    public GameObject rifle;
    private Player player;

    [SerializeField]
    bool isShotgun = false;

    private void Start()
    {
        weapon = GetComponent<Weapon>();
        shotGun = GetComponent<Shotgun>();
        player = GetComponent<Player>();
        ChangeWeapon();
    }
    private void Update()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            isShotgun = !isShotgun;
            ChangeWeapon();
        }
    }
    private void ChangeWeapon()
    {
        if (isShotgun)
        {
            rifle.SetActive(false);
            weapon.mirar = false;
            weapon.enabled = false;
            shotGunWeapon.SetActive(true);
            player.isShotgun = true;
            shotGun.enabled = true;
        }
        else
        {
            shotGunWeapon.SetActive(false);
            shotGun.mirar = false;
            shotGun.enabled = false;
            rifle.SetActive(true);
            player.isShotgun = false;
            weapon.enabled = true;
        }
    }
    
}
