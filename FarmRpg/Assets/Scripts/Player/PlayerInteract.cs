using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool isInteracting = false;
    public GameObject currentInteractingTextObject;
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetButtonDown("InteractText") && currentInteractingTextObject && !isInteracting)
	    {
            currentInteractingTextObject.GetComponent<DialogueTrigger>().TriggerDialogue();
	    }
        else if (Input.GetButtonDown("InteractText") && currentInteractingTextObject && isInteracting)
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "TextObject" && !currentInteractingTextObject)
        {
            currentInteractingTextObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (currentInteractingTextObject.gameObject == other.gameObject)
        {
            currentInteractingTextObject = null;
        }
    }

}
