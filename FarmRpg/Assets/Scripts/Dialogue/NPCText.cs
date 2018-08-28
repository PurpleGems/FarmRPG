using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCText
{
    //NPC type, Dialogue uses an array of this. always the ability to change name/thumbnail and text for each dialogue (even on next line)
    //so you can make it seem as if two npc's are talking etc.

    public string name;
    [TextArea(3, 5)]
    public string text;
    public Sprite sprite;

}
