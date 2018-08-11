using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGame : MonoBehaviour {

    
    public Frog targetFrog = new Frog();

    public bool CompareFrogs(Frog b) {
        if(b.color1==targetFrog.color1 && b.pattern == targetFrog.pattern && b.color2 == targetFrog.color2)
        {
            return true;
        }
        return false;
    }
}
