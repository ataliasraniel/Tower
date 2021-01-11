using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenuUIManager : MonoBehaviour
{
    #region funcionalidade dos botões

    public GameObject loadingScreen;
    private TextMeshProUGUI hintTxt;
    public int sceneIndex;
    public GameObject controls;

    public string[] dicas = new string[]
    {
        "<color=orange><b>Hint:</b></color> headshots do twice damage",
        "<color=orange><b>Hint:</b></color> try to always move to avoid being hit.",
        "<color=orange><b>Hint:</b></color> use the aim to increase precision."
    };
    public void StartGame()
    {
        StartCoroutine(LoadAsyncrousnly(sceneIndex));
        
    }
    private IEnumerator LoadAsyncrousnly(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);
        hintTxt = loadingScreen.GetComponentInChildren<TextMeshProUGUI>();
        hintTxt.text = dicas[Random.Range(0, 3)];
        while (!operation.isDone)
        {
            yield return null;
        }
    }
    public void ShowControls()
    {
        controls.SetActive(true);
    }
    public void CloseControls()
    {
        controls.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
