using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[System.Serializable]
public class RuntimeDialogueState:MonoBehaviour {
    [SerializeField]
    public DialogueState baseState;
    public string greeting;
    public Sprite specialGreetingPortrait;
    public bool blankState = false;
    //public DialogueLineSerial[] lines;
    public List<DialogueLineRuntime> lines= new List<DialogueLineRuntime>();
    public DialogueLineRuntime endDialogueLine;


    private void OnEnable()
    {
        greeting = baseState.greeting;
        specialGreetingPortrait = baseState.specialGreetingPortrait;
        blankState = baseState.blankState;
        //----------SPECIAL LINE SET
        if (lines.Count.Equals(0)){
            lines.Clear();
            foreach (DialogueLineSerial sLine in baseState.lines)
            {
                DialogueLineRuntime nLine = new DialogueLineRuntime();
                nLine.line = sLine.line;
                nLine.responses = sLine.responses;
                nLine.isHidden = sLine.isHidden;
                nLine.specialPortrait = sLine.specialPortrait;
                nLine.exitDialogue = sLine.exitDialogue;
                lines.Add(nLine);
            }
        }
        //------------SPECIAL END LINE SET
        DialogueLineRuntime nEndLine = new DialogueLineRuntime();
        nEndLine.line = baseState.endDialogueLine.line;
        nEndLine.responses = baseState.endDialogueLine.responses;
        nEndLine.isHidden = baseState.endDialogueLine.isHidden;
        nEndLine.specialPortrait = baseState.endDialogueLine.specialPortrait;
        nEndLine.exitDialogue = baseState.endDialogueLine.exitDialogue;
        endDialogueLine = nEndLine;
        
    }
    /*
    private void OnEnable()
    {
        greeting = baseState.greeting;
        specialGreetingPortrait = baseState.specialGreetingPortrait;
        blankState = baseState.blankState;
        //----------SPECIAL LINE SET
        foreach (DialogueLineSerial sLine in baseState.lines)
        {
            DialogueLineRuntime nLine = new DialogueLineRuntime();
            nLine.line = sLine.line;
            nLine.responses = sLine.responses;
            nLine.isHidden = sLine.isHidden;
            nLine.specialPortrait = sLine.specialPortrait;
            nLine.exitDialogue = sLine.exitDialogue;
            lines.Add(nLine);
        }
        //------------SPECIAL LINE SET
        endDialogueLine = baseState.endDialogueLine;


    }*/



}
