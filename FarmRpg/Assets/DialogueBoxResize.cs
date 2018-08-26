using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBoxResize : MonoBehaviour
{

    public TextMeshProUGUI textToResize;
    
    void Start()
    {
        
        
    }

    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(textToResize.preferredWidth + 15f, 32f);
    }
}
