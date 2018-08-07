using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



[System.Serializable]
public class SolutionPosition {

    public Vector2 position;

    public bool topConnection = false;

    public bool bottomConnection = false;

    public bool leftConnection = false;

    public bool rightConnection = false;
}


public class DesafioCripta : MonoBehaviour {
    [SerializeField]
    public GamePiece[,] game = new GamePiece[5, 5];
    public SolutionPosition startPosition;
    public List<SolutionPosition> solutionPositions = new List<SolutionPosition>();

    public LayerMask filtro;



    public void DebugGame() {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
               
                Debug.Log(game[i, j].name);
            }
        }
    }

    // Use this for initialization
    void Start () {
        int i, j;

        foreach(Transform child in transform)
        {
            i = Mathf.RoundToInt(child.transform.localPosition.x);
            j = Mathf.RoundToInt(child.transform.localPosition.y);
           // Debug.Log(i + "," + j);
            if (child.GetComponent<GamePiece>() !=null) {
                game[i,j]=child.GetComponent<GamePiece>();
            }

        }
        //DebugGame();
    }
    
    public void ShuffleGame(int iterations) {
        int it = iterations;
        int i, j, k, y;
        GamePiece aux;
        Vector3 auxT;
        i = Random.Range(0, 5);
        j = Random.Range(0, 5);
        k = Random.Range(0, 5);
        y = Random.Range(0, 5);

        StopAllCoroutines();
        for (int a = 0; a < 5; a++) {
            for (int b = 0; b < 5; b++)
            {
                game[a, b].Reset();

            }
        }


        while (it > 0) {
            it--;
            auxT = game[i, j].transform.localPosition;
            aux = game[i, j];
            game[i, j].transform.localPosition = game[k, y].transform.localPosition;
            game[i, j] = game[k, y];
            game[k, y].transform.localPosition = auxT;
            game[k, y] = aux;
            i = Random.Range(0, 5);
            j = Random.Range(0, 5);
            k = Random.Range(0, 5);
            y = Random.Range(0, 5);

        }
        


        DebugGame();

    }

    // Update is called once per frame

    public void GameMove(int i, int j) {

        if ((i >= 0 && i <= 4) && (j >= 0 && j <= 4)) {
            GamePiece aux;
            Vector3 auxT;
            //direita
            if (i < 4 && game[i + 1, j].GetBlank()) {
                auxT = game[i, j].transform.localPosition;
                aux = game[i, j];
                game[i, j].transform.localPosition = game[i + 1, j].transform.localPosition;
                game[i, j] = game[i + 1, j];
                game[i + 1, j].transform.localPosition = auxT;
                game[i + 1, j] = aux;
            }
            //esquerda
            if (i > 0 && game[i - 1, j].GetBlank())
            {
                auxT = game[i, j].transform.localPosition;
                aux = game[i, j];
                game[i, j].transform.localPosition = game[i - 1, j].transform.localPosition;
                game[i, j] = game[i - 1, j];
                game[i - 1, j].transform.localPosition = auxT;
                game[i - 1, j] = aux;
            }
            //cima
            if (j < 4 && game[i, j+1].GetBlank())
            {
                auxT = game[i, j].transform.localPosition;
                aux = game[i, j];
                game[i, j].transform.localPosition = game[i, j + 1].transform.localPosition;
                game[i, j] = game[i, j + 1];
                game[i, j + 1].transform.localPosition = auxT;
                game[i, j + 1] = aux;
            }
            //baixo
            if (j > 0 && game[i, j - 1].GetBlank())
            {
                auxT = game[i, j].transform.localPosition;
                aux = game[i, j];
                game[i, j].transform.localPosition = game[i, j - 1].transform.localPosition;
                game[i, j] = game[i, j - 1];
                game[i, j - 1].transform.localPosition = auxT;
                game[i, j - 1] = aux;
            }


        }
    }

    IEnumerator Flood(int i, int j) {
        game[i,j].Flood();
        yield return new WaitForSeconds(1);
        //direita
        if (i < 4 && game[i, j].RightConnection && game[i + 1, j].LeftConnection) {
            if (!game[i + 1, j].GetFlooded())
            {
                StartCoroutine(Flood(i + 1, j)) ;
            }
        }
        //acima
        if (j < 4 && game[i, j].TopConnection && game[i , j + 1].BottomConnection)
        {
            if (!game[i, j + 1].GetFlooded())
            {
                StartCoroutine(Flood(i, j + 1));
            }             
        }
        //Esquerda
        if (i > 0 && game[i, j].LeftConnection && game[i - 1, j].RightConnection)
        {
            if (!game[i - 1, j].GetFlooded())
            {
                StartCoroutine(Flood(i - 1, j));
            }
        }
        //Baixo
        if (j > 0 && game[i, j].BottomConnection && game[i, j - 1].TopConnection)
        {
            if (!game[i, j - 1].GetFlooded())
            {
                StartCoroutine(Flood(i, j - 1));
            }
        }


        bool solution = CheckSolution();
        if (solution) {
            Debug.Log("Desafio Completo");
        }
    }

    public void StartFlood() {

        bool f = true;
        int i, j;
        i = Mathf.RoundToInt(startPosition.position.x);
        j = Mathf.RoundToInt(startPosition.position.y);

        if (game[i, j].LeftConnection != startPosition.leftConnection && startPosition.leftConnection)
        {
            f = false;
        }
        if (game[i, j].RightConnection != startPosition.rightConnection && startPosition.rightConnection)
        {
            f = false;
        }
        if (game[i, j].TopConnection != startPosition.topConnection && startPosition.topConnection)
        {
            f = false;
        }
        if (game[i, j].BottomConnection != startPosition.bottomConnection && startPosition.bottomConnection)
        {
            f = false;
        }

        if (f) {
            StartCoroutine(Flood(Mathf.RoundToInt(startPosition.position.x), Mathf.RoundToInt(startPosition.position.y)));
        }
        
    }
    public bool CheckSolution() {
        
        foreach (SolutionPosition pos in solutionPositions) {
            int i, j;
            i = Mathf.RoundToInt(pos.position.x);
            j = Mathf.RoundToInt(pos.position.y);
            if (!game[i, j].GetFlooded()) {
                return false;
            }
            if (game[i, j].LeftConnection != pos.leftConnection && pos.leftConnection) {
                return false;
            }
            if (game[i, j].RightConnection != pos.rightConnection && pos.rightConnection)
            {
                return false;
            }
            if (game[i, j].TopConnection != pos.topConnection && pos.topConnection)
            {
                return false;
            }
            if (game[i, j].BottomConnection != pos.bottomConnection && pos.bottomConnection)
            {
                return false;
            }
        }

        return true;
    }

    void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(screenPosition, Vector3.forward, Mathf.Infinity, filtro);
            Debug.DrawRay(screenPosition, Vector3.forward, Color.red, 5f);
            if (hit)
            {
                Debug.Log("hit");
                if (hit.transform.gameObject.GetComponent<GamePiece>() != null) {
                    GameMove(Mathf.RoundToInt(hit.transform.localPosition.x), Mathf.RoundToInt(hit.transform.localPosition.y));
                }

            }
            else {
                Debug.Log("miss");
            }
            
        }



    }
}
