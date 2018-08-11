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
        ListrasVerticais,
        ListrasHorizontais,
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
