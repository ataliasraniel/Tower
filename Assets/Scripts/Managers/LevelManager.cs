using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public string levelName;

    public bool secondPhase = false;
    public bool isThirdPhase = false;
    public bool isFinished = false;


    public GameObject portal;
    private Transform portalPos;

    public bool isLastChallenge;
    public GameObject lastPortal;

    [Header("Collect")]
    public int toColect;
    public int colected;

    [Header("Switches")]
    public int switchActivated;
    public int switchNecessary;

    [Header("Kill")]
    public bool isKill;
    public int killed;
    public int toKill;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventManager.instance.OnLevelFinished += LevelFinished;

        var portal = GameObject.FindGameObjectWithTag("Portal position");

        if (portal != null)
        {
            portalPos = portal.GetComponent<Transform>();
        }
    }
    public void CountDeaths()
    {
        killed+=1;
        if(killed >= toKill && isKill)
        {
            isFinished = true;
            CheckLevelFinish();
        }
    }
    public void ActivateSwitch(bool isSecondPhase)
    {
        switchActivated++;
        if (isSecondPhase)
        {
            StartSecondPhase();
        }
        if (isThirdPhase)
        {
            StartThirdPhase();
        }
        if(switchActivated >= switchNecessary)
        {
            isFinished = true;
            CheckLevelFinish();
        }
    }
    private void StartThirdPhase()
    {
        EventManager.instance.StartThirdPhase();
    }
    private void StartSecondPhase()
    {
        EventManager.instance.StartSecondPhase();
    }
    private void CheckLevelFinish()
    {
        if (isFinished)
        {
            EventManager.instance.LevelHasFinished();
        }
        else return;
    }

    public void LevelFinished()
    {
        EventManager.instance.OnLevelFinished -= LevelFinished;
        StartCoroutine(PortalSpawnAnim());
        if (isLastChallenge)
        {
            var clone = Instantiate(lastPortal, portalPos.position, Quaternion.identity);
            AudioManager.instance.Play("Portal spawn SFX");
        }
        else
        {
            var clone = Instantiate(portal, portalPos.position, Quaternion.identity);
            AudioManager.instance.Play("Portal spawn SFX");
        }
    }
    private IEnumerator PortalSpawnAnim()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
    }
}
