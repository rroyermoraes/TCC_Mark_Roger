using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : InteractableObject{

    // Use this for initialization
    void Start()
    {
        bInteractions.Add(new ButtonInteraction("Ler", Read));
        bInteractions.Add(new ButtonInteraction("Inspecionar", Inspect));
        bInteractions.Add(new ButtonInteraction("Pegar", Take));
    }




    public override void Take()
    {
        base.Take();
        string[] s = new string[] { "Não consigo pegar isso, é muito pesado" };
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        //Debug.Log(s);
        
    }


}
