using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteractionArea : MonoBehaviour {
    //This class will take care of a bobbing icon when the player is close enough as well as show gizmo in editor mode

    public Sprite icon;
    public float distance;

    private GameObject player;
    private Object bobbingObject;
    private GameObject newBob;

    private bool isInsideDistanceArea = false;

    // Use this for initialization
	void Start ()
	{
        player = GameObject.FindGameObjectWithTag("Player");
	    bobbingObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/DialoguePrefab/BobbingIcon.prefab", typeof(GameObject));
    }
	
	
	void Update ()
	{
	    //If the player gets into the desired range the icon should appear as well as when he leaves it should destroy the created object
        StartBobbing();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position,"ChatBox");       
        Gizmos.DrawWireSphere(transform.position,distance);

    }

    void StartBobbing()
    {
        //should only spawn one animated gameobject when entering the distance zone (checks if you just entered the zone if so you cant spawn another one)
        //Until you leave it)
        if (Vector3.Distance(transform.position, player.transform.position) <= distance && !isInsideDistanceArea)
        {
            isInsideDistanceArea = true;
            newBob = Instantiate(bobbingObject, transform.position, Quaternion.identity, transform) as GameObject;
            if (icon != null)
                newBob.GetComponent<SpriteRenderer>().sprite = icon;
        }

        //if the player leaves the distance zone destroy the created object, should allow you to re create the gameobject if you enter the zone again
        //since you left the one you just entered
        if (Vector3.Distance(transform.position, player.transform.position) >= distance)
        {
            isInsideDistanceArea = false;
            Destroy(newBob);
        }
    }
}
