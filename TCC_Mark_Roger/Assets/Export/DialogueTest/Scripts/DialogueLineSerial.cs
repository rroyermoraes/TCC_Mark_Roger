using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public struct UnlockableLine {
    public DialogueState state;
    public int index;

    public UnlockableLine(DialogueState s, int i)
    {
        state = s;
        index = i;
    }
}

[System.Serializable]
public class DialogueResponseSerial {
    public string text;
    public AudioClip audioClip;

}



//[CreateAssetMenu(fileName ="New Dialogue Line",menuName ="Dialogue/Line")]
[System.Serializable]
public class DialogueLineSerial
{
    [Space(10)]
    
    [Tooltip("The player line that will trigger a response")]
    [TextArea]
    public string line;
   // public Color customLineColor= new Color(0,0,0,1);
    [Tooltip("The NPC response lines, use multiple to longer texts that will overflow the textbox")]
    [TextArea]
    public string[] responses;
    [Space(10)]
    public bool isHidden = false;
    [Tooltip("Will this line use a special portrait for the NPC response? Leave empty to use the default npc portrait")]
    public Sprite specialPortrait;
    [Space(30)]
    [Header("End Line Options")]
    // public DialogueLineSerial nextline;
    //   public bool backToStateDialogue = false;
    //[Tooltip("Is this line the default end of conversation line for this DialogueState? False by default")]
   // public bool defaultEndLine=false;

    [Tooltip("Will this line move the conversation to a new DialogueState? None by default")]
    public DialogueState nextState;

    [Tooltip("Will this line change the NPCState? None by default")]
    public NPCState targetNPCDialogue;

    [HideInInspector]
    public bool allreadySayd = false;


    [Tooltip("What dialogue lines will this one unlock? None by default")]
    public UnlockableLine[] linesToUnlock;


    [Tooltip("UnityEvents by this line")]
    public UnityEvent eventsX;

    [Tooltip("Turn on if you want this line to end de conversation")]
    public bool exitDialogue = false;



    public DialogueLineSerial() {
        responses = new string[1];
    }





}

