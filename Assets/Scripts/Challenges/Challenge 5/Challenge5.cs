using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Challenge5 : MonoBehaviour
{
    public static Challenge5 instance;
    public Transform centrifuge;
    public GameObject raiosFX;

    public float rotationSpeed;
    public float angle;
    public float gravity;
    public float fallGravity;

    [Header("Scenario")]
    public Renderer[] rends;

    

    private PlayerMovement playerMovement;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        EventManager.instance.OnSecondPhase += StartSecondPhase;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (centrifuge)
        {
            centrifuge.Rotate(new Vector3(0, 1 * rotationSpeed, 0), angle * Time.deltaTime
            , Space.World);
        }
        else return;
        
    }
    public void StartLevel()
    {
        playerMovement.gravity0 = true;
        playerMovement.gravity = gravity;
        playerMovement.groundCheck.gameObject.SetActive(false);
        playerMovement.DoJump();
    }
    private void StartSecondPhase()
    {
        StartCoroutine(ScenarioAnim());
        EventManager.instance.OnSecondPhase -= StartSecondPhase;
        playerMovement.gravity0 = false;
        playerMovement.gravity = fallGravity;
        playerMovement.groundCheck.gameObject.SetActive(true);
        playerMovement.DoJump();
    }
    private IEnumerator ScenarioAnim()
    {
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.DOTiling(new Vector2(10, 10), 3);
        }
        yield return new WaitForSeconds(3);
        Destroy(centrifuge.gameObject);
        Destroy(raiosFX);
    }
}
