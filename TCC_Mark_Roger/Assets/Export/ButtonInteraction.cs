using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public delegate void Interaction();

[System.Serializable]
public class ButtonInteraction
{

    public string name;

    public Interaction x;

    public ButtonInteraction(string nName, Interaction nX)
    {
        name = nName;
        x = nX;

    }
    public ButtonInteraction()
    {
        name = "";
        x = null;

    }
}
