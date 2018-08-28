using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public static GameEngine instance;

    public string playerName;
    public int health;
    public int money;
    

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

   


   

    


}
