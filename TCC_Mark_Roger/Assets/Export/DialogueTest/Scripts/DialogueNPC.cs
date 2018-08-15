using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNPC : MonoBehaviour {

    public Sprite DefaltNPCPortrait;
    public NPCState activeNPCState;
    public DialogueState activeDialogueState;

    private bool[] hiddenLines;
	
    // Use this for initialization
	void Start () {
        activeDialogueState = activeNPCState.defualtDialogueState;
        //foreach(DialogueLineSerial l in activeDialogueState.lines)
        //hiddenLines= activeDialogueState.lines
	}
	
    public void ChangeNPCState(NPCState nState) {

        activeNPCState = nState;
    }

    public void ChangeDialogueState(DialogueState nDialogueState) {

        activeDialogueState = nDialogueState;

    }
    public void ChageNPCState(NPCState nNPCState) {
        activeNPCState = nNPCState;
    }

}


public class RuntimeDialogueState {

    public string greeting;
    public Sprite specialGreetingPortrait;
    public bool blankState = false;
    public DialogueLineSerial[] lines;

    public RuntimeDialogueState(DialogueState x) {
        greeting = x.greeting;
        specialGreetingPortrait = x.specialGreetingPortrait;
    }
    
}
public class RuntimeNPCState {
    public RuntimeDialogueState[] runtimeStates;
}

