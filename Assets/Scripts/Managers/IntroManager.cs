using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
public class IntroManager : MonoBehaviour
{
    private bool skipIntro;
    private float timer;

    public TextMeshProUGUI texto;
    public GameObject loadingScreen;

    private string[] textLine = new string[] 
    { 
        "The world has changed. Much had been lost, because none of the living can remember...",
        "<size=86>DEVAAS KEE MEENAAR",
        "War... what is war? Our people don't knew such meaning and we lived in peace for years", 
        "but nothing lasts forever...",
        "one day, our peace was broken. A great evil, moved by greed and power came to this world." +
        " This evil has a name: <color=orange><size=56>Deva.",
        "We fell, we can't afford such <color=orange><size=56> Demonic Sight.",
        "The life is close to <color=orange><size=56>anihilation",
        "Now, in the end of times, we <color=orange><size=56>Resistência</size></color> move towards " +
        "the Deva Fortress.",
        "The <color=orange><size=56>Fire Gates</size></color> are open waiting the last soldier" +
        ".<color=orange>GO! Defeat Deva!"


    };

    private void Start()
    {
        StartCoroutine(TextoAnim());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            timer += Time.deltaTime;
            if(timer >= 0.5f)
            {
                skipIntro = true;
                if (skipIntro)
                {
                    StartCoroutine(LoadAsyncrousnly(2));
                    Destroy(gameObject);
                }
            }
        }
    }
    private IEnumerator TextoAnim()
    {
        texto.DOFade(1, 4);
        texto.text = textLine[0];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[1];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[2];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[3];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[4];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[5];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[6];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[7];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[8];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[9];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        texto.DOFade(1, 4);
        texto.text = textLine[10];
        yield return new WaitForSeconds(5);
        texto.DOFade(0, 4);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(2);
    }
    private IEnumerator LoadAsyncrousnly(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        Destroy(texto);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}

