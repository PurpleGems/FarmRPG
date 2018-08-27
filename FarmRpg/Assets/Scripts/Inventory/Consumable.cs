using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Comsumable",menuName = "Items/Comsumable")]
public class Consumable : Item
{
    public int healAmount;
    public int energyReplenishAmount;

    //TODO Add a Buff class so it can take in a Buff[] and set values to give u 
}
