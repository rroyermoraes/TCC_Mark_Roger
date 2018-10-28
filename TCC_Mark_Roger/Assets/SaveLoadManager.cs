using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaveLoadManager : MonoBehaviour {

    public GameSaveData data = new GameSaveData();

    public void SaveGame(int slot) {

        //SavePlayerPosition-----------------------------------------------------------
        data.playerPosition = new Vector3Serial(FindObjectOfType<PlayerController>().transform.position);
        
        //SaveObjectives----------------------------------------------------------
        int objIndex = 0;
        List<Objective> obj = FindObjectOfType<ObjectiveManager>().objectives;
        foreach(Objective o in obj){ 
            objIndex = data.objectivesData.FindIndex(x => x.id == o.Data.id);
            if (objIndex != -1) { 
                data.objectivesData[objIndex] = o.Data;
            }else{
                data.objectivesData.Add(o.Data);
            }
        }

        //NPCs-------------------------------------------------------------------------
                //NPC Data Gathering---------------------------------------------------
        List<NPCData> npcDatas = new List<NPCData>();
        DialogueNPC[] sceneNPCs = GameObject.Find("NPCS").GetComponentsInChildren<DialogueNPC>(true);


        foreach (DialogueNPC s in sceneNPCs){
            Debug.Log(s.name);
            NPCData a = new NPCData
            {
                enabled = s.gameObject.activeInHierarchy,
                id = s.NPC_Id,

                nPCStateId = s.activeNPCState.Dialogue_Id
            };
            npcDatas.Add(a);
        }
       
        //NPC Data Saving
        int npc_Index = 0;
        foreach(NPCData n in npcDatas){
            npc_Index = data.npcsData.FindIndex(x => x.id == n.id);
            if(npc_Index != -1){
                data.npcsData[npc_Index] = n;
            }else{
                data.npcsData.Add(n);
            }
        }




        //////////////////////////////////////////
        //FileSave
        FileManager.SaveGame(data, slot);
    }

    public void LoadGame(int slot) {
        ///////////////////////////////////////////
        ////FileLoaded
        data=FileManager.LoadGame(slot);

        //Player Position----------------------------------------------------
        FindObjectOfType<PlayerController>().GetComponent<NavMeshAgent>().Warp(new Vector3(data.playerPosition.x, data.playerPosition.y, data.playerPosition.z));

        //Objectives---------------------------------------------------------
        int objIndex = 0;
        ObjectiveManager  objManager = FindObjectOfType<ObjectiveManager>();
        foreach (ObjectiveData oD in data.objectivesData)
        {
            objIndex = objManager.objectives.FindIndex(x => x.Data.id == oD.id);
            if (objIndex != -1){
                objManager.objectives[objIndex].Data = oD;
            }
        }

        //NPC--------------------------------------------------------------------
        /////NPC Gathering
        
        List<DialogueNPC> sceneNPCs = new List<DialogueNPC>(GameObject.Find("NPCS").GetComponentsInChildren<DialogueNPC>(true));
        List<RuntimeNPCDialogueContainer> dialogueContainers = new List<RuntimeNPCDialogueContainer>(GameObject.Find("NPCS").GetComponentsInChildren<RuntimeNPCDialogueContainer>(true));

        

        int npc_index = 0;
        foreach(NPCData n in data.npcsData)
        {
            npc_index = sceneNPCs.FindIndex(x => x.NPC_Id == n.id);
            if(npc_index != -1){
                sceneNPCs[npc_index].enabled = n.enabled;
                sceneNPCs[npc_index].gameObject.SetActive(n.enabled);
                sceneNPCs[npc_index].activeNPCState = dialogueContainers.Find(x => x.Dialogue_Id == n.nPCStateId);
            }
        }
    }


}
