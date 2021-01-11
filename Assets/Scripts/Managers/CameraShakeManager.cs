using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [Header("Camera Shake")]
    public float duration = 0.1f;
    public float strength = 0.4f;
    public int vibrato = 30;
    public float randomness = 60;
    public bool fadeOut;
    public Camera fpsCam;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fpsCam = FindObjectOfType<Camera>();
    }
    public void ShakeDamageCamera()
    {
        fpsCam.DOShakePosition(0.3f, 2f, 60,
        randomness, fadeOut);
    }
    public void ShakeCamera()
    {
        fpsCam.DOShakePosition(duration, strength, vibrato,
        randomness, fadeOut);
    }
}
