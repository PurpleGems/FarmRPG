using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInteractingTextObject;
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetButtonDown("InteractText") && currentInteractingTextObject)
	    {
            currentInteractingTextObject.GetComponent<DialogueTrigger>().TriggerDialogue();
	    }
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
