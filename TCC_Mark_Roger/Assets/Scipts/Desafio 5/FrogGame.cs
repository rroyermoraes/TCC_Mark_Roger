using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGame : MonoBehaviour {

    public GameFrog compare;
    public Frog targetFrog = new Frog();
    private string speach;
    private bool twoColors = false;
    private bool oneColor = false;
    private bool patt = false;
    private bool twoPositions = false;
    private bool onePosition = false;

    public void CompareFrogs() {
       Frog b = compare.frog;
        if(b.color1==targetFrog.color1 && b.pattern == targetFrog.pattern && b.color2 == targetFrog.color2)
        {
            //Success text

            return;
        }
        else
        {
            //Color

            if ((b.color1 == targetFrog.color1 && b.color2 == targetFrog.color2)||(b.color2 == targetFrog.color1 && b.color1 == targetFrog.color2))
            {
                //Debug.Log("Ele tinha essas cores");
                twoColors = true;
                oneColor = false;

            }
            else
            {
                if ((b.color1 == targetFrog.color1) || (b.color1 == targetFrog.color2) || (b.color2 == targetFrog.color1) || (b.color2 == targetFrog.color2))
                {
                    // Debug.Log("Ele tinha uma dessas cores");
                    oneColor = true;
                    twoColors = false;
                }
                else {
                  //  Debug.Log("Ele nao tinha essas cores");
                    twoColors = false;
                    oneColor = false;
                }
            }
            //Pattern
            if (b.pattern == targetFrog.pattern) {
                patt = true;
            }
            else
            {
                patt = false;
            }
            //Positon
            if(b.color1==targetFrog.color1 && b.color2 == targetFrog.color2)
            {
                twoPositions = true;
                onePosition = false;

            }
            else
            {
                if (b.color1 == targetFrog.color1 || b.color2 == targetFrog.color2) {
                    onePosition = true;
                    twoPositions = false;
                }
                else
                {
                    onePosition = false;
                    twoPositions = false;

                }

            }

            /////////////////////////////////////////////////////////////////////////////////
            speach = "";

            if (twoColors) {
                speach += "As cores eram essas";
                if (!patt)
                {
                    speach += ", mas o padrao era diferente.";
                }
                else {
                    speach += ", e esse parece ter o mesmo padrao";
                    if (!onePosition && !twoPositions)
                    {
                        speach += ", mas tem algo que ainda nao esta certo sobre as cores.";
                    }
                }
            }
            if (oneColor) {
                speach += "Ele tinha uma dessas cores com certeza";
                if (!patt)
                {
                    speach += ", mas o padrao era diferente.";
                }
                else
                {
                    speach += ", e esse parece ter o mesmo padrão.";/*
                    if (!onePosition)
                    {
                        speach += ", mas tem algo que ainda nao esta certo sobre as cores";
                    }*/
                }
            }
            if (!oneColor && !twoColors)
            {
                speach = "Ele não tinha essas cores";
            }


            Debug.Log(speach);
            

        }
        return;
    }

    public void PickFrog(GameFrog f)
    {
        compare.transform.position = f.transform.position;
        compare = f ;
        compare.transform.position = new Vector3(-999f, -999f);

    }

}
