using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Button))]
public class ContextMenuButton : MonoBehaviour {

    private Interaction x;


    public void SetButtonIteraction(Interaction nIter)
    {

        x = nIter;
        GetComponent<Button>().onClick.AddListener(this.ButtonClicked);

    }


    public void ButtonClicked()
    {
        x.Invoke();
    }

    




}

