using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesafioBebida : MonoBehaviour {


    private List<int> a = new List<int>(new int[4]);
    private List<int> b = new List<int>(new int[4]);
    private int moves = 0;
    private int maxMoves = 0;
    [SerializeField]
    private int maxFails = 1;
    private int turnFails = 0;
    public bool oneMissing = false;
    public int missingIndex;
    public List<DesafioBebidaTurn> turnos = new List<DesafioBebidaTurn>(1);
    public int activeTurn = 0;
    private bool newTurnReady = false;
    #region EditorFields
    [Space(10f)]

    public List<Text> aF = new List<Text>(4);
    public List<Text> bF = new List<Text>(4);
    public List<Text> cF = new List<Text>(4);
    public List<Slider> bDrink = new List<Slider>(4);

    public SliderAnimator b1Slider;
    public SliderAnimator b2Slider;
    public SliderAnimator b3Slider;
    public SliderAnimator b4Slider;

    public SliderAnimator a1Slider;
    public SliderAnimator a2Slider;
    public SliderAnimator a3Slider;
    public SliderAnimator a4Slider;

    public Animator d1Animator;
    public Animator d2Animator;
    public Animator d3Animator;



    public Text maxMovesF;
    public Text moveCountF;

    public List<Button> exchangeButtons = new List<Button>(new Button[4]);
    public Button confirmB;
    public Button nextTurnB;



    #endregion


    //Turn Preparations
    IEnumerator SetA(List<int> nA)
    {
        EnableDisableExchange(false);
        confirmB.interactable = false;
        confirmB.GetComponentInChildren<SpriteOutline>().outlineSize = 0;
        a1Slider.AnimateSlide(0);
        a2Slider.AnimateSlide(0);
        a3Slider.AnimateSlide(0);
        a4Slider.AnimateSlide(0);
        yield return new WaitForSeconds(2);
        

        for (int j = 0; j < 4; j++)
        {
            a[j] = nA[j];
            bDrink[j].value = 0;

        }

        //fill
        b1Slider.AnimateSlide(a[3]);
        yield return new WaitForSeconds(1);
        b2Slider.AnimateSlide(a[0]);
        yield return new WaitForSeconds(1);
        b3Slider.AnimateSlide(a[1]);
        yield return new WaitForSeconds(1);
        b4Slider.AnimateSlide(a[2]);
        yield return new WaitForSeconds(1);
        //pause
        yield return new WaitForSeconds(1.5f);
        //drink
        d1Animator.SetBool("Drink", true);
        d2Animator.SetBool("Drink", true);
        d3Animator.SetBool("Drink", true);
        b1Slider.AnimateSlide(0);
        a1Slider.AnimateSlide(a[3]);
        b2Slider.AnimateSlide(0);
        a2Slider.AnimateSlide(a[0]);
        b3Slider.AnimateSlide(0);
        a3Slider.AnimateSlide(a[1]);
        b4Slider.AnimateSlide(0);
        a4Slider.AnimateSlide(a[2]);


        //wait
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 4; i++)
        {
            cF[i].text = a[i].ToString();

        }
        d1Animator.SetBool("Drink", false);
        d2Animator.SetBool("Drink", false);
        d3Animator.SetBool("Drink", false);
        SetTurnB();

    }

    IEnumerator SetB(List<int> nB)
    {
        yield return new WaitForSeconds(2);
        //b = nB;
        for (int j = 0; j < 4; j++)
        {
            b[j] = nB[j];

        }

        b1Slider.AnimateSlide(b[3]);
        yield return new WaitForSeconds(1);
        b2Slider.AnimateSlide(b[0]);
        yield return new WaitForSeconds(1);
        b3Slider.AnimateSlide(b[1]);
        yield return new WaitForSeconds(1);
        b4Slider.AnimateSlide(b[2]);
        yield return new WaitForSeconds(1);





        for (int i = 0; i < 4; i++)
        {
            bF[i].text = b[i].ToString();
            //bDrink[i].value = b[i];

        }
        EnableDisableExchange(true);
        confirmB.interactable = true;
        confirmB.GetComponentInChildren<SpriteOutline>().outlineSize = 1;
    }

    private void SetMaxMoves(int i)
    {
        maxMoves = i;
        maxMovesF.text = maxMoves.ToString();
    }
    private void EnableDisableExchange(bool f)
    {
        if (f)
        {
            foreach (Button b in exchangeButtons)
            {
                b.interactable = true;
            }

        }
        else {
            foreach (Button b in exchangeButtons)
            {
                b.interactable = false;
            }
        }

    }

    private void CommitTurn()
    {
        bool result = true;
        b1Slider.AnimateSlide(0);

        b2Slider.AnimateSlide(0);

        b3Slider.AnimateSlide(0);

        b4Slider.AnimateSlide(0);
        List<int> c = new List<int>(new int[4]);
        EnableDisableExchange(false);
        confirmB.interactable = false;
        confirmB.GetComponentInChildren<SpriteOutline>().outlineSize = 0;
        for (int i = 0; i < 4; i++) {
            c[i] = a[i] + b[i];
            if (c[i] > 10)
            {
                result = false;
            }

        }
        for (int i = 0; i < 4; i++)
        {
            cF[i].text = c[i].ToString();
            if (c[3] <= 10){
               
            }
            if (c[0] <= 10)
            {
               
            }
            if (c[1] <= 10)
            {
               
            }

        }
        StartCoroutine(LastDrink(c));



        CheckTurn(result);
    }

    IEnumerator LastDrink(List<int> c){
        d1Animator.SetBool("Drink", true);
        d2Animator.SetBool("Drink", true);
        d3Animator.SetBool("Drink", true);
        a1Slider.AnimateSlide(c[3]);
        a2Slider.AnimateSlide(c[0]);
        a3Slider.AnimateSlide(c[1]);
        a4Slider.AnimateSlide(c[2]);
        yield return new WaitForSeconds(1);
        d1Animator.SetBool("Drink", false);
        d2Animator.SetBool("Drink", false);
        d3Animator.SetBool("Drink", false);

    }


    public void ConfirmButton() {
        CommitTurn();


    }
    private void SetTurnA()
    {
        
        //StartCoroutine(SetA(turnos[activeTurn].iA));
        for (int i = 0; i < 4; i++)
        {
            bF[i].text = " ";

        }
        SetMaxMoves(turnos[activeTurn].iMaxMoves);

        moves = 0;
        moveCountF.text = moves.ToString();

        StartCoroutine(SetA(turnos[activeTurn].iA));
        

    }

    private void SetTurnB()
    {
        StartCoroutine(SetB(turnos[activeTurn].iB));

        

    }

    public void Exchange(int pos1, int pos2)
    {
        moves++;
        moveCountF.text = moves.ToString();
        if (moves > maxMoves)
        {
            CommitTurn();
            return;
        }
        int aux = 0;
        aux = b[pos1];
        b[pos1] = b[pos2];
        b[pos2] = aux;

        bF[pos1].text = b[pos1].ToString();
        bF[pos2].text = b[pos2].ToString();

        bDrink[pos1].value = b[pos1];
        bDrink[pos2].value = b[pos2];


    }

    public void ButtonExchange(int i) {

        switch (i) {
            case 1:
                Exchange(0, 1);
                break;
            case 2:
                Exchange(1, 2);
                break;
            case 3:
                Exchange(2, 3);
                break;
            case 4:
                Exchange(3, 0);
                break;
        }

    }



    public void CheckTurn(bool result) {

        if (!result)
        {
            turnFails++;
            if (turnFails > maxFails)
            {
                Debug.LogError("Fim do desafio, voce falhou demais.");
            }
            else {
                Debug.Log("VoceFalhou Nesse turno." + "(" + activeTurn.ToString() + ")");
                
                newTurnReady = true;
                nextTurnB.gameObject.SetActive(true);
                
            }

        }
        else {
            Debug.Log("Parabens voce concluiu esse turno."+"(" + activeTurn.ToString() + ")");
            activeTurn++;
            newTurnReady = true;
            nextTurnB.gameObject.SetActive(true);

        }
        EnableDisableExchange(false);
        confirmB.interactable = false;
        confirmB.GetComponentInChildren<SpriteOutline>().outlineSize = 0;


    }

    public void NextTurnButton() {
        if (newTurnReady) {
            StartNewTurn();
        }
        nextTurnB.gameObject.SetActive(false);
    }


    public void StartGame() {
        activeTurn = 0;
        newTurnReady = false;
        SetTurnA();

    }
    public void StartNewTurn() {
        
        newTurnReady = false;
        if (activeTurn < turnos.Count){
            SetTurnA();
            foreach (Text t in cF)
            {
                t.text = "";
            }
        }
        else{

            Debug.Log("COMCLUIDO");
        }
        

    }




}

[System.Serializable]
public class DesafioBebidaTurn {

    public List<int> iA = new List<int>(4);
    public List<int> iB = new List<int>(4);

    public int iMaxMoves = 0;

}
