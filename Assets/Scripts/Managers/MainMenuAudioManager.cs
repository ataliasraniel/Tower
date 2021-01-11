using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioManager : MonoBehaviour
{
    public float thunderTimeMin;
    public float thunderTimeMax;

    [SerializeField]
    private AudioSource _audioSource;

    public AudioClip thunderSFX;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        StartCoroutine(CallThunder());
    }
    private IEnumerator CallThunder()
    {
        while (true)
        {
            _audioSource.PlayOneShot(thunderSFX);
            yield return new WaitForSeconds(Random.Range(thunderTimeMin, thunderTimeMax));
        }
    }
}
