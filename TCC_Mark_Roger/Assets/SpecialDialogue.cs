using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpecialDialogueLine{
    [TextArea]
    public string line;
    public Sprite contextPortrait;
    public UnityEvent specialEvent;

}


public class SpecialDialogue : MonoBehaviour {
    public NPCSpeaker nPCSpeaker;

    //[TextArea]
   // public string[] specialDialogue;

    public List<SpecialDialogueLine> specialDialogueLines = new List<SpecialDialogueLine>();

    public void StreamSpecialDialogue() {


        nPCSpeaker.NPCSpeak(specialDialogueLines);

    }

}
