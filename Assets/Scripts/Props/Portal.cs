using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private int sceneIndex;
    public bool isDesiredScene;
    public int desiredScene;
    public bool isLast;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isLast)
            {
                AudioManager.instance.Play("PortalEntrySFX");
                DadoManager.instance.CallDesiredScene(10);
            }
            else if (isDesiredScene)
            {
                AudioManager.instance.Play("PortalEntrySFX");
                DadoManager.instance.CallDesiredScene(desiredScene);
            }
            else
            {
                AudioManager.instance.Play("PortalEntrySFX");
                DadoManager.instance.CallScene();
            }
            
            
        }
        
    }
}
