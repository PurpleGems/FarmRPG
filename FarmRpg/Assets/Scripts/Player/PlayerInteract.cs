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
        //If the player isn't already interacting, and hes in the zone of a text area that gets stored
        //inside current then access the DialogueTrigger from that zone and start the dialogue.
	    if (Input.GetButtonDown("InteractText") && currentInteractingTextObject && !isInteracting)
	    {
            currentInteractingTextObject.GetComponent<DialogueTrigger>().TriggerDialogue();
	        previousInteractedObject = currentInteractingTextObject;

	    }
        //If the person is already interacting and is inside a textarea then display the next line of the dialogue with the same key.
	    //only if the textbox style is anything but one line since a one line textbox gets removed via timer instead of a key press.
        else if (Input.GetButtonDown("InteractText") && currentInteractingTextObject && isInteracting)
	    {
            if(FindObjectOfType<DialogueManager>().GetTextBoxStyle() != TextBoxStyle.OneLine)
                DialogueManager.instance.DisplayNextSentence();

            //FindObjectOfType<DialogueManager>().DisplayNextSentence(); what I did previously incase something goes wrong...

	    }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        //if the player enters a "TextArea" tagged collision then store that gameobject inside current so you can acces its dialoguetrigger
        if (other.tag == "TextObject" && !currentInteractingTextObject)
        {
            currentInteractingTextObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Once he leaves that area make sure to set it to null so hes not inside of a text area. and cant activate that stored textbox anywhere
        //for how many times he wants.
            currentInteractingTextObject = null;
    }

}
