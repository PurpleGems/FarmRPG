using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> dialogueSentencesQueue;
    private Queue<string> npcNameQueue;
    private Queue<Sprite> npcSpriteQueue;

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
        this.dialogue = currentdialogue;
        //Selects the textbox style placed in the enum of DialogueTrigger
        EnableTextBox(dialogue.style);
       // SetDialogueFields(dialogue);
       QueueNPCNames();
       QueueSentence();
       QueueNPCThumbnail();
       


    }

    


    
    #region Enable TextBoxes Methods

    private void EnableRegularTextbox()
    {
        TextBox[0].SetActive(true);
    }
    private void EnableNpcTextbox()
    {
        TextBox[1].SetActive(true);
    }
    private void EnableOneLineTextbox()
    {
        TextBox[2].SetActive(true);
    }

    #endregion


    
    private void EnableTextBox(TextBoxStyle style)
    {
        DisableAllTextboxs();

        switch (style)
        {
            case TextBoxStyle.Regular:
                EnableRegularTextbox();
                Debug.Log("RegularTextBoxEnabled");
                break;
            case TextBoxStyle.Npc:
                EnableNpcTextbox();
                Debug.Log("NPCTextBoxEnabled");
                break;
            case TextBoxStyle.OneLine:
                Debug.Log("OneLineTextBoxEnabled");
                EnableOneLineTextbox();
                break;
            default:
                Debug.Log("No textbox to enable");
                break;
        }
    }



    private void QueueSentence()
    {
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
            npcSpriteQueue.Clear();
      
            foreach (NPCText element in dialogue.npcDialogue)
            {
                npcSpriteQueue.Enqueue(element.sprite);
            }
        
       
        



        DisplayNextThumbnail();
    }

    private void QueueNPCNames()
    {
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
        Debug.Log("End of Conversation");
    }

    private void TypeWritterText(string text)
    {

    }


    
    

    private void DisableAllTextboxs()
    {
        TextBox[0].SetActive(false);
        TextBox[1].SetActive(false);
        TextBox[2].SetActive(false);

    }





}
