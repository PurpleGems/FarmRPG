using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;

    public static DialogueManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<DialogueManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    //Once a Dialogue starts it stores everything inside these variables.
    private Queue<string> dialogueSentencesQueue;
    private Queue<string> npcNameQueue;
    private Queue<Sprite> npcSpriteQueue;

    public Animator[] textBoxesAnimator;

    public  GameObject[] TextBox;
    private Dialogue dialogue;

    void Start()
    {      
        dialogueSentencesQueue = new Queue<string>();
        npcSpriteQueue = new Queue<Sprite>();
        npcNameQueue = new Queue<string>();
    }



    public void StartDialogue(Dialogue currentdialogue)
    {
        
        FindObjectOfType<PlayerInteract>().isInteracting = true;
        this.dialogue = currentdialogue;
        
        //Selects the textbox style placed in the enum of DialogueTrigger
        //Depending on what TextBox we choose from the enum it will activate that one.
        EnableTextBox(dialogue.style);
        
        //Gets everything from the Dialogue instance we triggered and stores everything inside the dialogue
        //inside the Queue variables
        QueueNPCNames();
        QueueSentence();
        QueueNPCThumbnail();
       


    }

    


    
    //Enables the textbox we triggered and sets the players ability to move to false

    private void EnableRegularTextbox()
    {
        TextBox[0].SetActive(true);
        textBoxesAnimator[0].SetBool("IsOpen", true);
        FindObjectOfType<PlayerMovement>().hasControl = false;
    }
    private void EnableNpcTextbox()
    {
        TextBox[1].SetActive(true);
        textBoxesAnimator[1].SetBool("IsOpen", true);
        FindObjectOfType<PlayerMovement>().hasControl = false;
    }
    private void EnableOneLineTextbox()
    {
        TextBox[2].SetActive(true);
        textBoxesAnimator[2].SetBool("IsOpen", true);
        FindObjectOfType<PlayerInteract>().isInteracting = false;

    }

    



    private void EnableTextBox(TextBoxStyle style)
    {
        //This Function should disable all activated TextBoxes and enable the one we triggered
        DisableAllTextboxs();

        switch (style)
        {
            case TextBoxStyle.Regular:
                EnableRegularTextbox();
                Time.timeScale = 0;
                Debug.Log("RegularTextBoxEnabled");
                break;
            case TextBoxStyle.Npc:
                EnableNpcTextbox();
                Time.timeScale = 0;
                Debug.Log("NPCTextBoxEnabled");
                break;
            case TextBoxStyle.OneLine:
                Debug.Log("OneLineTextBoxEnabled");
                EnableOneLineTextbox();
                //Should remove a one line text after 3 seconds.
                StartCoroutine(DialogueDisplayTime(3));
                break;
            default:
                Debug.Log("No textbox to enable");
                break;
        }
    }



    private void QueueSentence()
    {
        //Clears everything inside of the Sentence queue (clears everything from previous dialogue)
        //also checks to see which queue to grab the text info from npc or regular
        //since the npc textbox has its own Type
        dialogueSentencesQueue.Clear();

        if (dialogue.style == TextBoxStyle.Npc)
        {
            foreach (NPCText element in dialogue.npcDialogue)
            {
                dialogueSentencesQueue.Enqueue(element.text);
            }
        }
        else
        {
            foreach (string element in dialogue.text)
            {
                Debug.Log("Did we get into one line queue?");
                dialogueSentencesQueue.Enqueue(element);
            }
        }
        DisplayNextSentence();
    }

    private void QueueNPCThumbnail()
    {
        //Clears everything inside the npcimage Queue and stores the new ones from the dialogue we just triggered.
        npcSpriteQueue.Clear();
      
            foreach (NPCText element in dialogue.npcDialogue)
            {
                npcSpriteQueue.Enqueue(element.sprite);
            }

        DisplayNextThumbnail();
    }

    private void QueueNPCNames()
    {
        //Clears everything inside the npcName Queue and stores the new ones from the dialogue we just triggered.
        npcNameQueue.Clear();
        foreach (NPCText element in dialogue.npcDialogue)
        {
            npcNameQueue.Enqueue(element.name);
        }
    }

    

    public void  DisplayNextSentence()
    {
        if (dialogueSentencesQueue.Count <= 0)
        {
            EndDialogue();
            return;
        }
        //Removes the first string and stores the 2nd element from the queue inside this string then sets the text of the textbox we are using to
        //that string.
        string sentence = dialogueSentencesQueue.Dequeue();

        switch (dialogue.style)
        {
            case TextBoxStyle.Regular:
                TextBox[(int) TextBoxStyle.Regular].GetComponentInChildren<TextMeshProUGUI>().text = sentence;
                break;
            case TextBoxStyle.Npc:
                Debug.Log("MADE IT HERE");
                TextBox[(int)TextBoxStyle.Npc].transform.Find("NPCDialogueText").GetComponent<TextMeshProUGUI>().text = sentence;
                DisplayNextThumbnail();
                DisplayNextName();
                break;
            case TextBoxStyle.OneLine:
                TextBox[(int)TextBoxStyle.OneLine].GetComponentInChildren<TextMeshProUGUI>().text = sentence;
                break;
            default:
                Debug.Log("DisplayNextSentence?");
                break;
        }
    }

    private void DisplayNextThumbnail()
    {
        if (npcSpriteQueue.Count <= 0)
            return;
        Sprite currentSprite = npcSpriteQueue.Dequeue();
        TextBox[1].transform.Find("NPCThumbnailImage").GetComponentInChildren<Image>().sprite = currentSprite;

    }

    private void DisplayNextName()
    {
        if (npcNameQueue.Count <= 0)
        {
            return;
        }

        string currentName = npcNameQueue.Dequeue();
        TextBox[1].transform.Find("NPCNameText").GetComponent<TextMeshProUGUI>().text = currentName;

    }


    private void EndDialogue()
    {
        foreach (var element in textBoxesAnimator)
        {
            element.SetBool("IsOpen", false);
        }

        FindObjectOfType<PlayerInteract>().isInteracting = false;
        FindObjectOfType<PlayerMovement>().hasControl = true;
        Time.timeScale = 1;

        Debug.Log("End of Conversation");
    }

    private void TypeWritterText(string text)
    {

    }


    private IEnumerator DialogueDisplayTime(float time)
    {
        //ONLY GETS CALLED IF THE TEXT BOX STYLE IS ONE LINE

        //Gets the gameobject we are interacting with and stores it, waits 3 seconds to see if we triggered a different one line text box
        //if so it shouldnt remove the new one before 3 seconds so do nothing until that new ones 3 seconds is up.
        Debug.Log("Interacted with a textbox?");
        GameObject interactedGameObject = FindObjectOfType<PlayerInteract>().currentInteractingTextObject;
        yield return new WaitForSeconds(time);
        if (dialogue.style == TextBoxStyle.OneLine && interactedGameObject == FindObjectOfType<PlayerInteract>().currentInteractingTextObject ||
            interactedGameObject == FindObjectOfType<PlayerInteract>().previousInteractedObject)
        EndDialogue();
        else
        {
            Debug.Log("Different dialogue was open before the timer was up");
        }

        
    }
    

    private void DisableAllTextboxs()
    {      
        for (int i = 0; i < TextBox.Length; i++)
        {
            TextBox[i].SetActive(false);        
        }     
    }

    public TextBoxStyle GetTextBoxStyle()
    {
        return dialogue.style;
    }



}
