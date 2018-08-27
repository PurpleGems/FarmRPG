using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;

    public bool canSell;

    public int buyValue;
    public int sellValue;

}
