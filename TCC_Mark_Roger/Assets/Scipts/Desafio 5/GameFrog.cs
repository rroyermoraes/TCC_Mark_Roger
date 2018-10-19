using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Frog{

    public enum FrogColor
    {
        Azul,
        Verde,
        Vermelho,
        Amarelo,
        Branco,
        Preto
    };

    public enum Pattern
    {
        ListrasLongitudinais,
        ListrasLatitudinais,
        Manchas,
        Pintas
    };

    [Header("Frog properties")]
    public FrogColor color1;
    public Pattern pattern;
    public FrogColor color2;

    public Frog() {
        color1 = FrogColor.Azul;
        pattern = Pattern.Manchas;
        color2 = FrogColor.Branco;

    }
}

public class GameFrog : InteractableObject {
    public Frog frog;
    public string description;

    void Start()
    {
        bInteractions.Add(new ButtonInteraction("Inspecionar", Inspect));
        bInteractions.Add(new ButtonInteraction("Pegar", Take));
    }

    public override void Take()
    {
        base.Take();
        FindObjectOfType<FrogGame>().PickFrog(this);
        string[] s = new string[] { "Peguei o sapo " + description + "." };
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        //Debug.Log(s);

    }
    public override void Inspect()
    {
        base.Inspect();
        string[] s = new string[] { "É um sapo "+ description + "." };
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        //Debug.Log(s);

    }


}
