using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private Sound s;

    private void Awake()
    {
        instance = this;
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("O som " + name + "não foi encontrado");
            return;
        }
        s.source.Play();
    }
    public void Stop()
    {
        s.source.Stop();
    }
}