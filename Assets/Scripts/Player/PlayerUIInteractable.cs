using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIInteractable : MonoBehaviour
{
    public LayerMask interactableMask;
    public bool interacting = false;

    private bool isSwitcher;

    private Switcher switcher;
    private Fonte fonte;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interacting)
        {
            Interact();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (((1 << other.gameObject.layer) & interactableMask) != 0 & other.gameObject != null)
        {
            interacting = true;
            GameUIManager.instance.InteractionController(interacting);

            if (other.gameObject.CompareTag("Switcher") && interacting)
            {
                switcher = other.GetComponent<Switcher>();
                isSwitcher = true;
            }
            else if(other.gameObject.CompareTag("Fonte") && interacting)
            {
                fonte = other.GetComponent<Fonte>();
            }
        }
        
    }
    private void Interact()
    {
        if (isSwitcher)
        {
            switcher.ActivateSwitch();
        }
        else
        {
            fonte.Heal();
        }
        interacting = false;
        GameUIManager.instance.InteractionController(interacting);
    }
    private void OnTriggerExit(Collider other)
    {
        interacting = false;
        if (((1 << other.gameObject.layer) & interactableMask) != 0 & other.gameObject != null)
        {
            GameUIManager.instance.InteractionController(interacting);
        }
    }
}
