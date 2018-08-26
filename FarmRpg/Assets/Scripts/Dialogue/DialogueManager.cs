using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> dialogueSentencesQueue;
    private Queue<Sprite> npcSpriteQueue;
    public GameObject[] TextBox;
    private Dialogue dialogue;

    void Start()
    {
        dialogueSentencesQueue = new Queue<string>();
        npcSpriteQueue = new Queue<Sprite>();
    }



    public void StartDialogue(Dialogue currentdialogue)
    {
        this.dialogue = currentdialogue;
        //Selects the textbox style placed in the enum of DialogueTrigger
        EnableTextBox(dialogue.style);
       // SetDialogueFields(dialogue);
       QueueNextSentence();
       QueueNextNPCThumbnail();
        


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

    //TODO Have the dialogue system take in a custom data type for npcs so it can have a text field and a thumbnail

        /*
    private void SetDialogueFields(Dialogue dialogue)
    {
        switch (dialogue.style)
        {
            case TextBoxStyle.Regular:
                TextBox[(int) dialogue.style].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.text[0];
                break;
            case TextBoxStyle.Npc:
                TextBox[(int) dialogue.style].transform.Find("NPCDialogueText").GetComponent<TextMeshProUGUI>().text = dialogue.text[0];
                TextBox[(int) dialogue.style].transform.Find("NPCNameText").GetComponent<TextMeshProUGUI>().text = dialogue.npcName;
                TextBox[(int) dialogue.style].transform.Find("NPCThumbnailImage").GetComponent<Image>().sprite = dialogue.textBox[0].sprite;
                break;
            case TextBoxStyle.OneLine:
                TextBox[(int) dialogue.style].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.text[0];
                break;
            default:
                Debug.Log("no fields somehow");
                break;
        }

    } // Pointless...
    */


    private void QueueNextSentence()
    {
        dialogueSentencesQueue.Clear();

        foreach (NPCText element in dialogue.textBox)
        {
            dialogueSentencesQueue.Enqueue(element.text);
        }
        DisplayNextSentence();
    }

    private void QueueNextNPCThumbnail()
    {
        npcSpriteQueue.Clear();

        foreach (NPCText element in dialogue.textBox)
        {
            npcSpriteQueue.Enqueue(element.sprite);
        }

        DisplayNextThumbnail();
    }

    

    public void DisplayNextSentence()
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
                break;
            case TextBoxStyle.OneLine:
                TextBox[(int)TextBoxStyle.OneLine].GetComponentInChildren<TextMeshProUGUI>().text = sentence;
                break;
            default:
                Debug.Log("DisplayNextSentence?");
                break;
        }
    }

    public void DisplayNextThumbnail()
    {
        if (npcSpriteQueue.Count <= 0)
            return;
        Sprite currentSprite = npcSpriteQueue.Dequeue();
        TextBox[1].transform.Find("NPCThumbnailImage").GetComponentInChildren<Image>().sprite = currentSprite;

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
