using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapKey : MonoBehaviour
{
    public GameObject TrapParent;
    public GameObject Key;
    public GameObject Dialog;
    public bool canInteract;
    public Animator playerKeyAnimator;
    public Animator[] trapAnimators;

    void Awake(){

        Dialog.SetActive(false);

        playerKeyAnimator = Key.GetComponentInChildren<Animator>();

        trapAnimators = TrapParent.GetComponentsInChildren<Animator>();
        
        playerKeyAnimator.SetBool("enabled", false);
        EnableTrapParent();
    }
    
    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player")){

            Dialog.SetActive(true);
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if(other.CompareTag("Player")){

            Dialog.SetActive(false);
            canInteract = false;
        }
    }

    void Update()
    {
        InteractWithKeyToDisableTraps();
    }

    void InteractWithKeyToDisableTraps(){

        if(Input.GetKeyDown(KeyCode.E) && canInteract){

            print("Interacting");

            //Intreract with key
            PlayKeyAnimation(); //Play Key animation
            DisableTrapParent(); //Disable all referenced traps
        }
    }

    void PlayKeyAnimation(){

        playerKeyAnimator.SetBool("enabled", true);
    }

    void DisableTrapParent(){

        foreach (Animator trapAnim in trapAnimators)
        {
            
            trapAnim.SetBool("enabled", false); //disable traps
        }

        canInteract = false;
    }

    void EnableTrapParent(){

        foreach (Animator trapAnim in trapAnimators)
        {
            
            trapAnim.SetBool("enabled", true);
        }
    }
}
