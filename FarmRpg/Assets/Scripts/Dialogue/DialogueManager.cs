using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> dialogueSentences;
    public GameObject[] TextBox;
    private Dialogue dialogue;

    void Start()
    {
        dialogueSentences = new Queue<string>();
    }



    public void StartDialogue(Dialogue currentdialogue)
    {
        this.dialogue = currentdialogue;
        //Selects the textbox style placed in the enum of DialogueTrigger
        EnableTextBox(dialogue.style);
        SetDialogueFields(dialogue);
        QueueNextSentence();
        


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
                TextBox[(int) dialogue.style].transform.Find("NPCThumbnailImage").GetComponent<Image>().sprite = dialogue.npcThumbnail;
                break;
            case TextBoxStyle.OneLine:
                TextBox[(int) dialogue.style].GetComponentInChildren<TextMeshProUGUI>().text = dialogue.text[0];
                break;
            default:
                Debug.Log("no fields somehow");
                break;
        }

    }


    private void QueueNextSentence()
    {
        dialogueSentences.Clear();

        foreach (string element in dialogue.text)
        {
            dialogueSentences.Enqueue(element);
        }
        DisplayNextSentence();
    }

    

    public void DisplayNextSentence()
    {
        if (dialogueSentences.Count <= 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueSentences.Dequeue();

        switch (dialogue.style)
        {
            case TextBoxStyle.Regular:
                TextBox[(int) TextBoxStyle.Regular].GetComponentInChildren<TextMeshProUGUI>().text = sentence;
                break;
            case TextBoxStyle.Npc:
                Debug.Log("MADE IT HERE");
                TextBox[(int)TextBoxStyle.Npc].transform.Find("NPCDialogueText").GetComponent<TextMeshProUGUI>().text = sentence;
                break;
            case TextBoxStyle.OneLine:
                TextBox[(int)TextBoxStyle.OneLine].GetComponentInChildren<TextMeshProUGUI>().text = sentence;
                break;
            default:
                Debug.Log("DisplayNextSentence?");
                break;
        }
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
