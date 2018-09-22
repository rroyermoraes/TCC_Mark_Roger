using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDialogue : MonoBehaviour {
    public NPCSpeaker nPCSpeaker;

    [TextArea]
    public string[] specialDialogue;

    public void StreamSpecialDialogue() {
        nPCSpeaker.NPCSpeak(specialDialogue);

    }

}
