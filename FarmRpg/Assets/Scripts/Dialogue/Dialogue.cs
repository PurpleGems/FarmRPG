﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextBoxStyle
{
    Regular,
    Npc,
    OneLine
}

[System.Serializable]
public class Dialogue
{

    [Header("Text Box Style")]
    public TextBoxStyle style;

    [Header("General")]
    [TextArea(3,6)]
    public string[] text;

    public DialogueCustomEvents[] events;

    [Header("NPC")]
    public NPCText[] npcDialogue;
    





}
