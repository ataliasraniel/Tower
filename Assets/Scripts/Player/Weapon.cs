using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class Weapon : MonoBehaviour
{

    [Header("Prefabs")]
    public Transform pfBullet;

    [Header("Lógica")]
    private RaycastHit hit;
    public int gunMinDamage = 2;
    public int gunMaxDamage = 3;
    public float raycastDistance;
    private Vector3 adjusteBefore;
    public Vector3 adjusteVector;
    public LayerMask hitMask;
    public float gunRange = 200;
    public float fireRate = 0.3f;
    public int magazine = 200;
    private int currentMagazine;
    public int MaxMagazine = 9999;
    public int munition = 100;
    public Transform shotForwardPos;
    public Transform[] gunShotPos;
    public bool canShot = true;
    private PlayerMovement playerMovement;
    public bool mirar = false;
    public float miraSpeed;
    private float standardSpeed;

    [Header("Spread")]
    public float spread;
    public float x;
    public float y;


    [Header("Animação e FX")]
    public GameObject muzzle;
    public GameObject BurstFx;
    public GameObject smoke;
    public LineRenderer rastroTiro;
    public GameObject hitFX;

    public float modelStartPosZ;
    public float modelRecoilZ;
    public float modelRecoilY;
    public float ModelRotationRecoil = 0.5f;

    [Header("Aim")]
    public bool aiming = false;
    public int aimingFOV = 60;
    public int normalFov = 80;
    public float aimSpeed = 0.2f;
    public Transform weaponModelTransform;
    private float weaponModelStartPosX;

    [Header("Audio")]
    public string shotSFXname;
    public AudioClip shotSFX;
    public AudioClip eptMagazine;
    public AudioClip reloadSFX;
    private float nextFire;
    

    
    public Camera fpsCam;

    private FpsCameraController _cameraController;

    private void Start()
    {

        adjusteBefore = adjusteVector;
        magazine = MaxMagazine;
        _cameraController = FindObjectOfType<FpsCameraController>();
        weaponModelTransform = GameObject.Find("Weapon Model").GetComponent<Transform>();
        weaponModelStartPosX = weaponModelTransform.position.x;
        modelStartPosZ = weaponModelTransform.position.z;
        playerMovement = FindObjectOfType<PlayerMovement>();
        standardSpeed = playerMovement.moveSpeed;
    }
    private void Update()
    {
        Mirar();
        Shot();
        Reload();
        
    }

    private void Shot()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire && magazine > 0 && canShot == true)
        {
            magazine--;
            currentMagazine++;
            UpdateUI();
            nextFire = Time.time + fireRate;

            y = Random.Range(-spread, spread);
            x = Random.Range(-spread, spread);
            gunShotPos[0].Rotate(x, y, 0);
            StartCoroutine(ShotFX());

        }

        if (Input.GetMouseButtonDown(0) && magazine <= 0)
        {
            AudioManager.instance.Play("EmptyMagazineSFX");
        }
        
    }
    private void Mirar()
    {
        
        if (Input.GetMouseButton(1))
        {   
            mirar = true;
            MirarController();
            
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mirar = false;
            MirarController();
        }
    }
    private void MirarController()
    {
        if (mirar && fpsCam!=null)
        {
            x = 0;
            y = 0;
            fpsCam.DOFieldOfView(aimingFOV, aimSpeed);
            weaponModelTransform.DOLocalMoveX(-1.13f, aimSpeed);
            GameUIManager.instance.ShowCrossHair(false);
            adjusteVector = Vector3.zero;
            playerMovement.moveSpeed = miraSpeed;
        }
        else
        {
            if (fpsCam != null)
            {
                fpsCam.DOFieldOfView(normalFov, aimSpeed);
                weaponModelTransform.DOLocalMoveX(weaponModelStartPosX, aimSpeed);
                GameUIManager.instance.ShowCrossHair(true);
                playerMovement.moveSpeed = standardSpeed;
            }
            adjusteVector = adjusteBefore;
        }
    }
    
    private IEnumerator ShotFX()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, gunRange, hitMask))
        {

            StartCoroutine(LineFX());

            int bulletDamage = Random.Range(gunMinDamage, gunMaxDamage);
            var enemy = hit.collider.GetComponentInParent<EnemyLifeSystem>();
            
            if (hit.collider.gameObject.layer == 10)
            {
                var head = hit.collider.GetComponentInParent<EnemyLifeSystem>();
                bulletDamage *= 2;
                head.TakeHeadDamage(bulletDamage);
                head.HitFX(hit.point);
                GameUIManager.instance.ShowDamagePopup(bulletDamage, hit.point, hit.transform,
                    Color.red,
                    1);
                yield return null;
            }    
            else if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage);
                enemy.HitFX(hit.point);
                GameUIManager.instance.ShowDamagePopup(bulletDamage, hit.point.normalized, hit.transform,  
                    Color.white,
                    1);
            }
        }

        weaponModelTransform.DOPunchPosition(new Vector3(0, modelRecoilY, modelRecoilZ), 0.1f);
        if (weaponModelTransform.localRotation.x < -1)
        {
            weaponModelTransform.DOLocalRotate(Vector3.zero, 0);
        }
        else
        {
            weaponModelTransform.DOBlendablePunchRotation(new Vector3(ModelRotationRecoil, 0, 0), 0.3f);

        }
        yield return null;
        CameraShakeManager.instance.ShakeCamera();


        GunAudio();
        var muzzleClone = Instantiate(muzzle,
        gunShotPos[Random.Range(0, gunShotPos.Length)].position, gunShotPos[0].rotation);
        var burstClone = Instantiate(BurstFx,
        gunShotPos[Random.Range(0, gunShotPos.Length)].position, gunShotPos[0].rotation);
        Destroy(muzzleClone, 1);

    }
    private IEnumerator LineFX()
    {
        rastroTiro.enabled = true;
        rastroTiro.SetPosition(0, gunShotPos[0].position);
        rastroTiro.SetPosition(1, hit.point);
        yield return new WaitForSeconds(0.3f);
        rastroTiro.enabled = false;
    }
    void GunAudio()
    {
        AudioManager.instance.Play(shotSFXname);
    }
    
    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && munition > 0 && magazine < MaxMagazine) //recarrega a arma e atualiza a UI
        {
            munition -= currentMagazine;
            magazine = MaxMagazine;
            currentMagazine = 0;
            StartCoroutine(ReloadFX());
            UpdateUI();
        }
    }
    IEnumerator ReloadFX()
    {
        AudioManager.instance.Play("ReloadSFX");
        canShot = false;
        yield return new WaitForSeconds(1.59f);
        canShot = true;
    }
    private void UpdateUI()
    {
        GameUIManager.instance.UpdateMagazine(magazine, munition);
    }

}
