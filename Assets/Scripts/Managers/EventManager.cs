using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void LevelFinished();
    public event LevelFinished OnLevelFinished;

    public delegate void SecondPhase();
    public event SecondPhase OnSecondPhase;

    public delegate void ThirdPhase();
    public event ThirdPhase OnThirdPhase;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartSecondPhase();
        }
    }
    public void StartSecondPhase()
    {
        if (OnSecondPhase != null)
        {
            OnSecondPhase();
        }
    }
    public void StartThirdPhase()
    {
        if(OnThirdPhase != null)
        {
            OnThirdPhase();
        }
    }
    public void LevelHasFinished()
    {
        if (OnLevelFinished != null)
        {
            OnLevelFinished();
        }
    }

}
