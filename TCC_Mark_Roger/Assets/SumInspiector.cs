using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumInspiector : MonoBehaviour {
    private bool inspectSum = false;
    public Color baseColor;
    public Color failColor;
    public int i=0;
    private int c;
    private DesafioBebida game;

    public int C {get{return c;}set{c = value;}}



    public void InspectSum(bool v)
    {
        inspectSum = v;
    }




    // Use this for initialization
    void Start () {
        game = FindObjectOfType<DesafioBebida>();
	}
	
	// Update is called once per frame
	void Update () {
        if (inspectSum)
        {
            C = game.A[i] + game.B[i];
            GetComponent<Slider>().value = C;
            if (C > 10)
            {


                GetComponent<Animator>().SetBool("Sum", false);
                GetComponent<Animator>().SetBool("SumFail", true);
            }
            else
            {

                GetComponent<Animator>().SetBool("SumFail", false);
                GetComponent<Animator>().SetBool("Sum", true);
            }
        }
        else
        {
            GetComponent<Slider>().value = 0;

        }

    }
}
