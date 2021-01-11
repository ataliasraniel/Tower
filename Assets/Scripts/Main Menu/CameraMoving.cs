using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform myCamera;
    public float speed;
    private void Update()
    {
        speed += Time.deltaTime * 0.1f;
        myCamera.Translate(0, 0, speed * Time.deltaTime);
    }
}
