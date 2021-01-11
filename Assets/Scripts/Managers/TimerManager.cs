using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    public static TimerManager _instance { get { return instance; } }

    public float timer;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
            
    }
    public void DestroyTimer()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
}
