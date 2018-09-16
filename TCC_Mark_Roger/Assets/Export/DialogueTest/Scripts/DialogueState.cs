using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue State", menuName = "Dialogue/DialogueState")]
public class DialogueState : ScriptableObject {
    [Tooltip("The greeting message the NPC will give the player")]
    [TextArea]
    public string greeting;
    [Space(10)]
    [Tooltip("Special Portrait for the NPC greeting, leave empty to use the default npc portrait")]
    public Sprite specialGreetingPortrait;
    [Tooltip("Turn this on, and only the greenting message will be shown and the conversation will end")]
    public bool blankState = false;
    [Header("Dialogue Lines")]
    
    public DialogueLineSerial[] lines;


    public void EnableObj(int id)
    {
        GameObject.FindObjectOfType<ObjectiveManager>().EnableObjective(id);
    }
    public void CompleteObj(int id)
    {
        GameObject.FindObjectOfType<ObjectiveManager>().CompleteObjetive(id);
    }



}
