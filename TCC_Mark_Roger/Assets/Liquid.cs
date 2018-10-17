using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Desafio8 { 

public class Liquid : InteractableObject {

    void Start()
    {
        bInteractions.Add(new ButtonInteraction("Inspecionar", Inspect));
        bInteractions.Add(new ButtonInteraction("Pegar", Take));
    }

}

}
