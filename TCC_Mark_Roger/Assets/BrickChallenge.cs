using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickChallenge : MonoBehaviour {
    public int[] password;
    private int point = 0;
    public Button[] buttons;
	// Use this for initialization
	void Start () {
        
	}
    public void InputCode(int c) {
        if (point < password.Length){
            if (c == password[point])
            {
                Debug.Log(point +"/"+ password.Length);
                if (point == password.Length-1){
                    Debug.Log("Completo");
                    foreach (Button b in buttons)
                    {
                        b.interactable = false;
                        
                    }

                }
                point++;
                bool used = true;
                for (int i = point; i < password.Length; i++)
                {
                    if (c == password[i])
                    {
                        used = false;
                    }
                }
                if (used)
                {
                    buttons[c].interactable = false;
                }
            }
            else
            {
                foreach (Button b in buttons)
                {
                    b.interactable = true;
                    point = 0;
                }
            }
        }

    }

    public void CheckForUse(int g) {
        

    }
	// Update is called once per frame
	void Update () {
		
	}
}
