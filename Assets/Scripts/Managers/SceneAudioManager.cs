using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    public static SceneAudioManager instance;
    public static SceneAudioManager _instance { get { return instance; } }

    private AudioSource audioSource;

    public AudioClip[] gameMusic;
    public AudioClip sanctuaryMusic;
    public AudioClip cogumeloMusic;
    public AudioClip bossMusic;
    public AudioClip creditsMusic;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        if (_instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameMusic[Random.Range(0, 2)]);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (audioSource != null) 
        {
            if (SceneManager.GetActiveScene().name == "Sanctuary")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(sanctuaryMusic);
            }
            else if (SceneManager.GetActiveScene().name == "Desafio 3")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(cogumeloMusic);
            }
            else if (SceneManager.GetActiveScene().name == "Desafio 5")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(bossMusic);
            }
            else if (SceneManager.GetActiveScene().name == "Desafio 4")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(gameMusic[0]);
            }
            else if (SceneManager.GetActiveScene().name == "Credits Scene")
            {
                audioSource.Stop();
                audioSource.PlayOneShot(creditsMusic);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                return;
            }
            else
            {
                if (audioSource != null && audioSource.isPlaying == true)
                {
                    return;
                }
                else
                {
                    audioSource.PlayOneShot(gameMusic[Random.Range(0, 2)]);
                }
            }
        }
        
        
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
