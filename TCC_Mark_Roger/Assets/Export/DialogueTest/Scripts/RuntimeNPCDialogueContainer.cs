using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuntimeNPCDialogueContainer : MonoBehaviour {
    public Sprite portrait;
    [SerializeField]
    public RuntimeDialogueState defaultDialogueState;
    [SerializeField]
    public List<RuntimeDialogueState> runtimeStates= new List<RuntimeDialogueState>();

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
