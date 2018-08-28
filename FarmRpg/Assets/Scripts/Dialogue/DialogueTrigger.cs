using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;


    public void TriggerDialogue()
    {
        //Makes sure the players not interacting with a text box already before starting a new one
        //can use the same key of starting a dialogue when interacting versus starting one when the player isnt interacting with one
        if (!FindObjectOfType<PlayerInteract>().isInteracting)
        {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);           
        }
        
    }
}

