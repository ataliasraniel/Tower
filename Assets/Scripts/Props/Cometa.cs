using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cometa : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
