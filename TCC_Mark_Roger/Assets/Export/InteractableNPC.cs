using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractableNPC : InteractableObject
{
    DialogueManager dManager;
    ContextMenuContainer cMContainer;
    // Use this for initialization
    void Start()
    {
        dManager = FindObjectOfType<DialogueManager>();
        cMContainer = FindObjectOfType<ContextMenuContainer>();
        bInteractions.Add(new ButtonInteraction("Falar", Speak));

    }


    public override void Speak()
    {
        dManager.StartDialogue(GetComponent<DialogueNPC>());
        cMContainer.HideContextMenu();

    }






}
