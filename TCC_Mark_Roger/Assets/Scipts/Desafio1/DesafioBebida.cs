using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesafioBebida : MonoBehaviour {


    private List<int> a = new List<int>(4);
    private List<int> b = new List<int>(4);
    private int moves = 0;
    private int maxMoves = 0;

    public List<DesafioBebidaTurn> turnos = new List<DesafioBebidaTurn>(1);
    public int activeTurn = 0;


    //Turn Preparations
    private void SetA(List<int> nA)
    {
        a = nA;
    }

    private void SetB(List<int> nB)
    {
        b = nB;
    }

    private void SetMaxMoves(int i)
    {
        maxMoves = i;
    }

    public bool CommitTurn()
    {
        List<int> c = new List<int>(4);
        for (int i = 0; i < 4; i++) {
            c[i] = a[i] + b[i];
        }
        foreach (int i in c) {
            if (i > 10) {
                return false;
            }
        }
        return true;
    }

    public void SetTurn()
    {
        SetA(turnos[activeTurn].iA);
        SetB(turnos[activeTurn].iB);
        SetMaxMoves(turnos[activeTurn].iMaxMoves);
        moves = 0;
    }

    public void Exchange(int pos1, int pos2)
    {
        int aux = 0;
        aux = b[pos1];
        b[pos1] = b[pos2];
        b[pos2] = aux;
        moves++;
        if (moves > maxMoves)
        {
            // MUDA ISSO POR FAVOR=================================================================
            GameOver();
        }

    }
    //MUDA ISSO POR FAVOR=====================================================================
    private void GameOver() {

        Debug.LogError("GAMEOVER");
    }


}

[System.Serializable]
public class DesafioBebidaTurn {

    public List<int> iA = new List<int>(4);
    public List<int> iB = new List<int>(4);

    public int iMaxMoves = 0;

}
