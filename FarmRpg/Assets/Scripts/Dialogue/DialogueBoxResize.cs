using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxResize : MonoBehaviour
{
    //This script sits on the OneLineTextbox, gets the text from it and rezises itself to the length of the inputted text.
    public TextMeshProUGUI textToResize;  

    void Update()
    {
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(textToResize.preferredWidth + 15f, 32f);
    }
}
