using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDeathNotifier : MonoBehaviour
{
    private Challenge2 challenge;
    private void Start()
    {
        challenge = FindObjectOfType<Challenge2>();
    }
    private void OnDestroy()
    {
        challenge.UpdateEnemies();
    }
}
