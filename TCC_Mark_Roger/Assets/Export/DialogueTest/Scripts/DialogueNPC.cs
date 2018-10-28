using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNPC : MonoBehaviour {
    [SerializeField]
    private int nPC_Id=0;
    public Sprite DefaltNPCPortrait;
    public RuntimeNPCDialogueContainer activeNPCState;
    public RuntimeDialogueState activeDialogueState;

    public int NPC_Id{get{return nPC_Id;}}

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





