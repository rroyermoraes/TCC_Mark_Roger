using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuntimeNPCDialogueContainer : MonoBehaviour {

    [SerializeField]
    private int dialogue_Id=0;
    public Sprite portrait;
    [SerializeField]
    public RuntimeDialogueState defaultDialogueState;
    [SerializeField]
    public List<RuntimeDialogueState> runtimeStates= new List<RuntimeDialogueState>();

    public int Dialogue_Id{get{return dialogue_Id;}}



    //private RuntimeDialogueState defualtDialogueState;

    private void Start()
    {
        if (!defaultDialogueState){
            if (!runtimeStates.Count.Equals(0))
            {
                defaultDialogueState = runtimeStates[0];
            }
            
        }
    }




}
