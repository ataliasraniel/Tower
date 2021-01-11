using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CreditsManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI timerTxt;
    private float timer;
    public Canvas canvas;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(CreditsAnim());
        timer = TimerManager.instance.timer;

    }

    private IEnumerator CreditsAnim()
    {
        text.DOFade(0, 0);
        text.DOFade(1, 2);
        text.text = "Da transcedência, vem a Luz que emana uma refulgência" +
            " deslumbrante que se assemelha à cauda de um cometa.";
        yield return new WaitForSeconds(10);
        text.DOFade(0, 4);
        yield return new WaitForSeconds(4);
        text.DOFade(1, 1);
        text.text = "Obrigado por jogar!";
        yield return new WaitForSeconds(4);
        text.DOFade(0, 0);
        text.DOFade(1, 2);
        text.text = "This game was made by: Atalias Raniel and Micherlon";
        yield return new WaitForSeconds(4);
        text.DOFade(0, 1);
        yield return new WaitForSeconds(1.5f);
        CallEndMenu();
    }
    private void CallEndMenu()
    {
        SceneAudioManager var = FindObjectOfType<SceneAudioManager>();
        Destroy(var.gameObject);
        Cursor.lockState = CursorLockMode.None;

        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        Image img = canvas.GetComponentInChildren<Image>();
        img.DOFade(1, 3);
        canvas.enabled = true;
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
