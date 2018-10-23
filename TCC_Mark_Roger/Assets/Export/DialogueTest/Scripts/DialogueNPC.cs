using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNPC : MonoBehaviour {

    public Sprite DefaltNPCPortrait;
    public RuntimeNPCDialogueContainer activeNPCState;
    public RuntimeDialogueState activeDialogueState;


    private void Start()
    {
        activeDialogueState = activeNPCState.defaultDialogueState;
    }


    public void ChangeNPCState(RuntimeNPCDialogueContainer nNPC) {

        activeNPCState = nNPC;
    }
 


    public void ChangeDialogueState(RuntimeDialogueState nD) {

        activeDialogueState = nD;

    }

}





