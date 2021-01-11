using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public TextMeshProUGUI timerTxt;

    [Header("Hud")]
    public GameObject hud;
    public Canvas canvas;
    private GameObject crosshair;
    private Image damagePanel;
    public GameObject loadingScreen;

    [Header("Pause menu")]
    public GameObject pauseMenu;
    private GameObject pauseM;
    private Transform pauseMenuPos;
    private MouseLook mouseLook;

    [Header("Magazine")]
    private TextMeshProUGUI magazineCounter;

    [Header("Death screen")]
    public GameObject deathScreen;
    public TextMeshProUGUI deathTitle;
    private Button buttonRetry;
    private Button ButtonQuit;
    private Image deathPanel;

    [Header("Health bar")]
    public Slider healthBar;
    private HealthManager lifeSystem;
    public bool showMouse = false;

    [Header("Popup dano")]
    public GameObject damagePopup;    
    public float timeToDestroy;
    public int yOffset;
    public float fadeTime;
    public float punchMultiplier;
    private Transform playerpos;

    [Header("Interactions")]
    private TextMeshProUGUI interactableLabel;
    private Image interactableImage;

    [Header("Level Finish")]
    public GameObject levelFinishLabel;

    [Header("Level Name")]
    public GameObject levelName;
    

    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        hud = GameObject.Find("HUD");
        interactableImage = hud.transform.GetChild(0).GetComponent<Image>();
        interactableLabel = hud.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        damagePanel = canvas.gameObject.transform.Find("Damage panel").GetComponent<Image>();
        pauseMenuPos = canvas.transform.Find("Pause menu pos");
        timerTxt = GameObject.Find("Timer txt").GetComponent<TextMeshProUGUI>();
        Cursor.lockState = CursorLockMode.Locked;
        crosshair = GameObject.Find("Crosshair");
        magazineCounter = GameObject.Find("Magazine txt").GetComponent<TextMeshProUGUI>();
        mouseLook = FindObjectOfType<MouseLook>();
        playerpos = FindObjectOfType<Player>().GetComponent<Transform>();

        

        healthBar = GameObject.Find("Health bar").GetComponent<Slider>();
        LifeSystem playerLifesystem = FindObjectOfType<LifeSystem>();
        if (healthBar != null)
        {
            healthBar.maxValue = HealthManager.instance.maxHealth;
            healthBar.value = HealthManager.instance.health;
        }
        

        if (showMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        hud.SetActive(true);

        InteractionController(false);

        LevelStartLabel();

        var muni = FindObjectOfType<Weapon>().munition;
        var maga = FindObjectOfType<Weapon>().MaxMagazine;
        UpdateMagazine(maga, muni);

        EventManager.instance.OnLevelFinished += LevelFinishLabel;
        
    }

    public void HealthBarController(int value)
    {
        if (healthBar != null)
        {
            healthBar.value = value;
            healthBar.DOValue(value, 1f).SetEase(Ease.OutElastic);
        }
        
    }
    public void ShowPanelDamage()
    {
        StartCoroutine(DamagePanelAnim());
    }
    private IEnumerator DamagePanelAnim()
    {
        damagePanel.gameObject.SetActive(true);
        damagePanel.DOFade(0.5f, 0.2f);
        yield return new WaitForSeconds(0.3f);
        damagePanel.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        damagePanel.gameObject.SetActive(false);
    }
    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
    public void PauseController(bool ispaused)
    {

        if (ispaused)
        {
            Cursor.lockState = CursorLockMode.None;
            mouseLook.enabled = false;
            hud.SetActive(false);
            pauseM = Instantiate(pauseMenu, pauseMenuPos.transform.position, Quaternion.identity,
                pauseMenuPos.transform);

            var btnsHolder = pauseM.transform.Find("Btns hold");
            var btnResume = btnsHolder.transform.GetChild(0).GetComponent<Button>();
            var btnQuit = btnsHolder.transform.GetChild(1).GetComponent<Button>();
            btnResume.onClick.AddListener(() => { GameManager.instance.ResumeGame(); });
            btnQuit.onClick.AddListener(() => { GameManager.instance.QuitToMainMenu(); });
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            hud.SetActive(true);
            mouseLook.enabled = true;
            Destroy(pauseM);
        }
    }
    public void UpdateMagazine(int magazine, int munition)
    {
        if (magazineCounter != null)
        {
            magazineCounter.text = "<color=orange>" + magazine.ToString() + "</color>" + "/" + munition.ToString();
        }
    }
    
    public void UpdateTimer(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void CallDeathScreen()
    {
        var screen = Instantiate(deathScreen, canvas.transform.position, Quaternion.identity, canvas.transform);
        deathTitle = screen.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        deathPanel = screen.transform.GetChild(0).GetComponent<Image>();
        deathPanel.DOFade(1, 0.8f);
        deathTitle.DOFade(1, 1.5f);
        ButtonQuit = screen.transform.GetChild(3).GetComponent<Button>();
        buttonRetry = screen.transform.GetChild(2).GetComponent<Button>();
        buttonRetry.onClick.AddListener(() => GameManager.instance.Retry());
        ButtonQuit.onClick.AddListener(()=> GameManager.instance.QuitToMainMenu());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowDamagePopup(int amount, Vector3 pos, Transform rot, Color color, float punchMulti)
    {
        var clone = Instantiate(damagePopup, new Vector3(pos.x, pos.y + yOffset
            , pos.z),
            Quaternion.identity);

        clone.transform.LookAt(playerpos.position);
        clone.transform.DOPunchScale(Vector3.one * punchMultiplier, 0.2f);
        clone.GetComponent<TextMeshPro>().text = amount.ToString();
        clone.GetComponent<TextMeshPro>().color = color;
        Destroy(clone, timeToDestroy);

    }

    public void ShowCrossHair(bool show)
    {
        crosshair.SetActive(show);
    }

    public void LevelStartLabel()
    {
        StartCoroutine(HideUI());
        var clone = Instantiate(levelFinishLabel, hud.transform.position, Quaternion.identity, canvas.transform);
        var image = clone.GetComponent<Image>();
        image.DOFade(1, 0.5f);
        var txt = clone.transform.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = LevelManager.instance.levelName;
        txt.DOFade(1, 1f);
        Destroy(clone, 4f);
    }

    public void LevelFinishLabel()
    {
        StartCoroutine(HideUI());
        var clone = Instantiate(levelFinishLabel, hud.transform.position, Quaternion.identity, canvas.transform);
        var image = clone.GetComponent<Image>();
        image.DOFade(1, 0.5f);
        var txt = clone.transform.GetComponentInChildren<TextMeshProUGUI>();
        txt.DOFade(1, 1f);
        txt.text = "desafio completo";
        Destroy(clone, 1.5f);
        EventManager.instance.OnLevelFinished -= LevelFinishLabel;
    }
    private IEnumerator HideUI()
    {
        hud.SetActive(false);
        yield return new WaitForSeconds(2f);
        hud.SetActive(true);
    }
    public void InteractionController(bool isInteracting)
    {
        if (isInteracting)
        {
            interactableImage.gameObject.SetActive(true);
            interactableLabel.gameObject.SetActive(true);
            interactableImage.DOFade(1, 0.2f);
            interactableLabel.DOFade(1, 0.3f);
        }
        else 
        {
            interactableImage.DOFade(0, 0.2f);
            interactableLabel.DOFade(0, 0.1f);
            interactableLabel.gameObject.SetActive(false);
            interactableImage.gameObject.SetActive(false);
        }
    }

}
