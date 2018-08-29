using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIFocus : MonoBehaviour
{
/*This script will somehow be able to go on a script and depending on what u press "focus" you on the element shown
 every ui that will be able to move on will need this script maybe the game engine will keep track of which uifocus its using
 so it can call the right function so when u press a key it "focuses" the right ui element*/

    public UnityEvent Up;
    public UnityEvent Down;
    public UnityEvent Left;
    public UnityEvent Right;
    public UnityEvent Rb;
    public UnityEvent Lb;

    public GameObject InputedGameObject;

    public void Start()
    {
        
    }


    



    public void FocusUp(GameObject gameobj)
    {
        
    }

    public void FocusDown(GameObject gameobj)
    {
        
    }

    public void FocusLeft(GameObject gameobj)
    {
        
    }

    public void FocusRight()
    {
        object obj = Right.GetPersistentTarget(0);
        GameObject gameObject = obj as GameObject;
        GameEngine.instance.CurrentFocus = gameObject.GetComponent<UIFocus>();
        Debug.Log(gameObject.name);

    }

    private void Focus(GameObject obj)
    {
        
        
    }

   

    
}
