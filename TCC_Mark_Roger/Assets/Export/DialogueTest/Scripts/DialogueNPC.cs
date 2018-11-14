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
    public void ChangeNPCState(int id) {
        List<RuntimeNPCDialogueContainer> dialogueContainers = new List<RuntimeNPCDialogueContainer>(GameObject.Find("NPCS").GetComponentsInChildren<RuntimeNPCDialogueContainer>(true));

        foreach(RuntimeNPCDialogueContainer npc_DC in dialogueContainers)
        {
            if (npc_DC.Dialogue_Id == id)
            {
                activeNPCState = npc_DC;
                break;

            }
        }
    }
}





