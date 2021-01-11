using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge2Trigger : MonoBehaviour
{
    public bool isLeft;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var lvl = FindObjectOfType<Challenge2>();
            lvl.LockDoors(isLeft);
            Destroy(gameObject);
        }
    }
}
