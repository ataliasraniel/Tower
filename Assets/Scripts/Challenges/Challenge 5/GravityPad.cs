using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Challenge5.instance.StartLevel();
            Destroy(this);
        }
    }
}
