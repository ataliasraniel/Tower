using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [Header("Timer")]
    public float timer = 0f;

    [Header("General")]
    public bool isPaused = false;

    [Header("Botões")]
    public Button continueBtn;
    public Button quitBtn;

    [Header("Player")]
    public Player _player;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        DOTween.SetTweensCapacity(1000, 100);
    }

    #region funcionalidade btns
    public void ResumeGame()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameUIManager.instance.PauseController(isPaused);

    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneAudioManager var = FindObjectOfType<SceneAudioManager>();
        Destroy(var.gameObject);
        var health = FindObjectOfType<HealthManager>();
        Destroy(health);
        SceneManager.LoadScene(2);
    }
    public void QuitToMainMenu()
    {
        SceneAudioManager var = FindObjectOfType<SceneAudioManager>();
        Destroy(var.gameObject);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        var health = FindObjectOfType<HealthManager>();
        Destroy(health);
        SceneManager.LoadScene(0);
    }
    #endregion

    public void PlayerDeath()
    {
        _player.OnPlayerDeath();
        Time.timeScale = 0.1f;
        var health = FindObjectOfType<HealthManager>();
        Destroy(health);
        GameUIManager.instance.CallDeathScreen();
        TimerManager.instance.DestroyTimer();
    }
    private void Update()
    {
        timer = TimerManager.instance.timer;
        GameUIManager.instance.UpdateTimer(timer);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            GameUIManager.instance.PauseController(isPaused);
        }
    }
}
