using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool isInteracting = false;
    public GameObject currentInteractingTextObject;

    public GameObject previousInteractedObject;
	// Update is called once per frame
	void Update ()
	{

	    if (Input.GetButtonDown("InteractText") && currentInteractingTextObject && !isInteracting)
	    {
            currentInteractingTextObject.GetComponent<DialogueTrigger>().TriggerDialogue();
	        previousInteractedObject = currentInteractingTextObject;

	    }
        else if (Input.GetButtonDown("InteractText") && currentInteractingTextObject && isInteracting)
	    {
            if(FindObjectOfType<DialogueManager>().GetTextBoxStyle() != TextBoxStyle.OneLine)
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

	    }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "TextObject" && !currentInteractingTextObject)
        {
            currentInteractingTextObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
            currentInteractingTextObject = null;
    }

}
