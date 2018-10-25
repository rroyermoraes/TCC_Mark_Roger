using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripta : InteractableObject {
    public LoadScene loadSc;
    void Start()
    {
        bInteractions.Add(new ButtonInteraction("Inspecionar", Inspect));
        //bInteractions.Add(new ButtonInteraction("Pegar", Take));
    }




    public override void Inspect()
    {
        base.Inspect();
        string[] s = new string[] { "Deixe me ver..." };
        loadSc.LoadLevel("Desafio 6");
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        //Debug.Log(s);

    }
}
