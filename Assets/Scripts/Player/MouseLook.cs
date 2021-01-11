using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 100;

    //REFERENCES
    private Transform body;
    public Transform cameraTransform;
    public bool lookWithMouse = true;

    //ADJUSTMENTS
    float xRotation = 0;
    private void Start()
    {
        EventManager.instance.OnLevelFinished += ControlLook;
        body = GetComponent<Transform>();
    }
    private void Update()
    {
        if (lookWithMouse)
        {
            LookWithMouse();
        }
        else return;
    }
    private void LookWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        body.Rotate(Vector3.up * mouseX);
    }
    private void ControlLook()
    {
        EventManager.instance.OnLevelFinished -= ControlLook;
        StartCoroutine(LookAtPortal());
    }
    private IEnumerator LookAtPortal()
    {
        lookWithMouse = false;
        LifeSystem life = GetComponent<LifeSystem>();
        life.imortal = true;
        Vector3 cameraInitialPos = cameraTransform.position;

        var portalPos = GameObject.FindGameObjectWithTag("Portal position").GetComponent<Transform>();
        cameraTransform.DOLookAt(new Vector3(portalPos.position.x, portalPos.position.y + 5,
            portalPos.position.z), 0.2f);
        yield return new WaitForSeconds(0.4f);
        cameraTransform.DOLocalRotate(cameraInitialPos, 0.1f);
        yield return new WaitForSeconds(0.2f);
        life.imortal = false;
        lookWithMouse = true;
        
    }
}
