using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 direction;
    public LayerMask damagebleLayer;
    public int bulletDamage;

    public Color damageColor;
    public Color headShotColor;

    public GameObject bloodFX;

    private void Start()
    {
        Destroy(gameObject, 1);
    }
    private void Update()
    { 
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    //colisão do tiro
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Flesh"))
        {
            BloodFX();
        }

        if (other.gameObject.layer == 10)
        {
            var head = other.gameObject.GetComponentInParent<EnemyLifeSystem>();
            bulletDamage *= 2;
            if (head != null)
            {
                head.TakeHeadDamage(bulletDamage);
                //GameUIManager.instance.ShowDamagePopup(bulletDamage, transform, headShotColor, 1.5f);
            }
            else Destroy(gameObject);
        }
        else if (((1 << other.gameObject.layer) & damagebleLayer) != 0 & other.gameObject != null)
        {
            print("colidiu com" + other.transform.name);
            var body = other.gameObject.GetComponentInParent<EnemyLifeSystem>();
            if (body != null)
            {
                body.TakeDamage(bulletDamage);
                //GameUIManager.instance.ShowDamagePopup(bulletDamage, transform, damageColor, 0);
                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }

    private void BloodFX()
    {
        Instantiate(bloodFX, transform.position, transform.rotation);
    }
}
