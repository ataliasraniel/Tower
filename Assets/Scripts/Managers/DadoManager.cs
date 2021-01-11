using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DadoManager : MonoBehaviour
{
    public static DadoManager instance;

    public GameObject loadingScreen;
    private Transform pos;
    private TextMeshProUGUI hintTxt;

    private string[] dicas = new string[] 
    {
        "<color=orange><b>Hint:</b></color> headshots do twice damage",
        "<color=orange><b>Hint:</b></color> try to always move to avoid being hit.",
        "<color=orange><b>Hint:</b></color> use the aim to increase precision."
    };

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pos = GameObject.Find("Loading screen pos").GetComponent<Transform>();
    }
    public void CallDesiredScene(int index)
    {
        StartCoroutine(LoadAsyncrousnly(index));
    }
    public void CallScene()
    {
        var realIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadAsyncrousnly(realIndex));
    }
    private IEnumerator LoadAsyncrousnly(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        var clone = Instantiate(loadingScreen, pos.position, Quaternion.identity, pos);
        hintTxt = clone.GetComponentInChildren<TextMeshProUGUI>();
        hintTxt.text = dicas[Random.Range(0, 3)];
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
