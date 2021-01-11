using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Switcher : MonoBehaviour
{
    public bool isOn = false;
    public bool isSecondPhase = false;

    public GameObject outRing;
    public GameObject innerRing;
    public float rotateSpeed;
    public float animSpeed = 0.5f;

    private BoxCollider _collider;
    public Renderer rend;
    private void Start()
    {
        StartCoroutine(RotateRing());
        _collider = GetComponent<BoxCollider>();
    }
    
    public void ActivateSwitch()
    {
        isOn = true;
        AudioManager.instance.Play("SwitchSFX");
        LevelManager.instance.ActivateSwitch(isSecondPhase);
        Destroy(_collider);
    }
    private void ItemAnimation()
    {

        if (isOn)
        {
            outRing.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), animSpeed);
            innerRing.transform.DOScale(90, 0.8f);
            rend.material.SetColor("_EmissionColor", Color.green * 5);
        }
                
    }
    private IEnumerator RotateRing()
    {
        while (!isOn)
        {
            outRing.transform.Rotate(new Vector3(0, 360, -360) * rotateSpeed * Time.deltaTime);
            yield return null;
        }
        ItemAnimation();
        
    }
}
