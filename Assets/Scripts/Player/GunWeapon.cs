using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunWeapon : MonoBehaviour
{
    //gun stats
    public int damage;
    public float timeBtwShooting, spread, range, reloadTime, timeBtwShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsPerShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //reference
    public Camera fpsCam;
    public Transform attackPoint;
    public LayerMask hitMask;

    //graphics
    //TODO: implementar script de câmera shake
    public GameObject muzzleFlash, bulletHoleGraphic;

    //UI
    public TextMeshProUGUI text;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();
        //set text
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsPerShot = bulletsPerTap;
            Shoot();
        }
    }


    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RaycastHit hit = Physics.Raycast(fpsCam.transform.position, direction, range,
        //    hitMask)
        ////Raycast
        //RaycastHit hit = Physics.Raycast(fpsCam.transform.position, direction, range,
        //hitMask));
        //{
        //    print(hit.transform.name);
        //    if (hit)
        //    {
        //        //TODO: implementar sistema de dano
        //        //hit.collider.GetComponent<ScriptdeVidaInimigo>().TakeDamage(damage);
        //    }
        //}
        bulletsLeft--;
        bulletsPerShot--;
        //Graphics
        //Instantiate(bulletHoleGraphic, hit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Invoke("ResetShot", timeBtwShooting);

        if (bulletsPerShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBtwShots);
        }

    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
