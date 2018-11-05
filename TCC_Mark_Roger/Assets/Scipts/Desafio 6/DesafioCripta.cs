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
    public SpriteRenderer fundo2;
    public AudioSource gameMoveSound;
    public AudioSource solutionSound;
    public AudioSource failSound;
    public GamePiece[,] game = new GamePiece[4, 4];
    public SolutionPosition startPosition;
    public List<SolutionPosition> solutionPositions = new List<SolutionPosition>();
    public bool autostart = false;
    public LayerMask filtro;
    private bool flooding = false;
    private bool complete = false;



    public void DebugGame() {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
               
               // Debug.Log(game[i, j].name);
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
        if (autostart)
        {
            ShuffleGame(2000);
        }
        
        //DebugGame();
    }

    public void ResetPieces() {
        StopAllCoroutines();
        
        //StartCoroutine(BackgroundShine(false));
        for (int a = 0; a < 4; a++)
        {
            for (int b = 0; b < 4; b++)
            {
                game[a, b].Reset();

            }
        }
    }

    public void ShuffleGame(int iterations) {
        int it = iterations;
        int i, j, k, y;
        GamePiece aux;
        Vector3 auxT;
        i = Random.Range(0, 4);
        j = Random.Range(0, 4);
        k = Random.Range(0, 4);
        y = Random.Range(0, 4);

        ResetPieces();


        while (it > 0) {
            it--;
            auxT = game[i, j].transform.localPosition;
            aux = game[i, j];
            game[i, j].transform.localPosition = game[k, y].transform.localPosition;
            game[i, j] = game[k, y];
            game[k, y].transform.localPosition = auxT;
            game[k, y] = aux;
            i = Random.Range(0, 4);
            j = Random.Range(0, 4);
            k = Random.Range(0, 4);
            y = Random.Range(0, 4);

        }

        for (int a = 0; a < 4; a++)
        {
            for (int b = 0; b < 4; b++)
            {
               if(game[a, b].GetBlank())
                {
                    auxT = game[0, 3].transform.localPosition;
                    aux = game[0, 3];
                    game[0, 3].transform.localPosition = game[a, b].transform.localPosition;
                    game[0, 3] = game[a, b];
                    game[a, b].transform.localPosition = auxT;
                    game[a, b] = aux;
                    break;
                }

            }
        }



       // DebugGame();

    }

    // Update is called once per frame

    public void GameMove(int i, int j) {
        ResetPieces();
        
        if ((i >= 0 && i <= 3) && (j >= 0 && j <= 3)) {
            GamePiece aux;
            Vector3 auxT;
            //direita
            if (i < 3 && game[i + 1, j].GetBlank()) {
                auxT = game[i, j].transform.localPosition;
                aux = game[i, j];
                game[i, j].transform.localPosition = game[i + 1, j].transform.localPosition;
                game[i, j] = game[i + 1, j];
                game[i + 1, j].transform.localPosition = auxT;
                game[i + 1, j] = aux;
               // gameMoveSound.pitch = Random.Range(0.9f, 1.1f);
                gameMoveSound.Play();
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
                //gameMoveSound.pitch = Random.Range(0.9f, 1.1f);
                gameMoveSound.Play();
            }
            //cima
            if (j < 3 && game[i, j+1].GetBlank())
            {
                auxT = game[i, j].transform.localPosition;
                aux = game[i, j];
                game[i, j].transform.localPosition = game[i, j + 1].transform.localPosition;
                game[i, j] = game[i, j + 1];
                game[i, j + 1].transform.localPosition = auxT;
                game[i, j + 1] = aux;
                //gameMoveSound.pitch = Random.Range(0.9f, 1.1f);
                gameMoveSound.Play();
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
               // gameMoveSound.pitch = Random.Range(0.9f, 1.1f);
                gameMoveSound.Play();
            }


        }
    }

    IEnumerator Flood(int i, int j) {
        flooding = true;
        game[i,j].Flood();
        // yield return new WaitForSeconds(0.25f);
        //yield return new WaitForEndOfFrame();
        yield return null;
        //direita
        if (i < 3 && game[i, j].RightConnection && game[i + 1, j].LeftConnection) {
            if (!game[i + 1, j].GetFlooded())
            {
                StartCoroutine(Flood(i + 1, j)) ;
                
            }
        }
        //acima
        if (j < 3 && game[i, j].TopConnection && game[i , j + 1].BottomConnection)
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

        flooding = false;
        bool solution = CheckSolution();
        if (solution) {
            Debug.Log("Desafio Completo");
            if (!solutionSound.isPlaying){
                solutionSound.Play();
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(4, 0.06f));
                
            }

            complete = true;
         //   StartCoroutine(Shine(Mathf.RoundToInt(startPosition.position.x), Mathf.RoundToInt(startPosition.position.y)));
            
        }
    }


    /*
    public void Shine()
    {
        for (int a = 0; a < 5; a++)
        {
            for (int b = 0; b < 5; b++)
            {
                if (game[a, b].GetFlooded()) {
                    game[a, b].Shine();
                }
            }
        }
    }
*/
    IEnumerator Shine(int i, int j)
    {
        game[i, j].Shine();
         yield return new WaitForSeconds(0.25f);
        
        //direita
        if (i < 3 && game[i, j].RightConnection && game[i + 1, j].LeftConnection)
        {
            if (game[i + 1, j].GetFlooded())
            {
                StartCoroutine(Shine(i + 1, j));

            }
        }
        //acima
        if (j < 3 && game[i, j].TopConnection && game[i, j + 1].BottomConnection)
        {
            if (game[i, j + 1].GetFlooded())
            {
                StartCoroutine(Shine(i, j + 1));
            }
        }
        //Esquerda
        if (i > 0 && game[i, j].LeftConnection && game[i - 1, j].RightConnection)
        {
            if (game[i - 1, j].GetFlooded())
            {
                StartCoroutine(Shine(i - 1, j));
            }
        }
        //Baixo
        if (j > 0 && game[i, j].BottomConnection && game[i, j - 1].TopConnection)
        {
            if (game[i, j - 1].GetFlooded())
            {
                StartCoroutine(Shine(i, j - 1));
            }
        }
    }

    IEnumerator InvalidStartPositions() {

        Debug.Log("tilt");
        Color c = new Color(1,1,1,0);
        fundo2.color = c;
        while (c.a < 1)
        {
            c.a += 0.1f;
            fundo2.color = c;
            yield return new WaitForEndOfFrame();
        }
        while (c.a > 0)
        {
            c.a -= 0.1f;
            fundo2.color = c;
            yield return new WaitForEndOfFrame();
        }

    }
    IEnumerator BackgroundShine(bool t) {
        if (t){
            Color c = new Color(1, 1, 1, 0);
            fundo2.color = c;
            while (c.a < 1)
            {
                c.a += 0.1f;
                fundo2.color = c;
                yield return new WaitForEndOfFrame();
            }
        }
        else{
            Color c = new Color(1, 1, 1, 1);
            fundo2.color = c;
            while (c.a > 0)
            {
                c.a -= 0.1f;
                fundo2.color = c;
                yield return new WaitForEndOfFrame();
            }
        }


    }


    public void StartFlood() {
        
        bool f = true;
        int i, j;
        foreach(SolutionPosition s in solutionPositions){
            i = Mathf.RoundToInt(s.position.x);
            j = Mathf.RoundToInt(s.position.y);

            if (game[i, j].LeftConnection != s.leftConnection && s.leftConnection)
            {
                f = false;
                break;
            }
            if (game[i, j].RightConnection != s.rightConnection && s.rightConnection)
            {
                f = false;
                break;
            }
            if (game[i, j].TopConnection != s.topConnection && s.topConnection)
            {
                f = false;
                break;
            }
            if (game[i, j].BottomConnection != s.bottomConnection && s.bottomConnection)
            {
                f = false;
                break;
            }
        }
        
        


        if (f) {
            StartCoroutine(BackgroundShine(true));
            StartCoroutine(Flood(Mathf.RoundToInt(solutionPositions[0].position.x), Mathf.RoundToInt(solutionPositions[0].position.y)));
            StartCoroutine(Flood(Mathf.RoundToInt(solutionPositions[1].position.x), Mathf.RoundToInt(solutionPositions[1].position.y)));
            StartCoroutine(Flood(Mathf.RoundToInt(solutionPositions[2].position.x), Mathf.RoundToInt(solutionPositions[2].position.y)));
            StartCoroutine(Flood(Mathf.RoundToInt(solutionPositions[3].position.x), Mathf.RoundToInt(solutionPositions[3].position.y)));
        }
        else{
            
            StartCoroutine(InvalidStartPositions());
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
            if (flooding) {
                return;
            }
            if (complete) {
                return;
            }
            RaycastHit2D hit = Physics2D.Raycast(screenPosition, Vector3.forward, Mathf.Infinity, filtro);
           // Debug.DrawRay(screenPosition, Vector3.forward, Color.red, 5f);
            if (hit)
            {
               // Debug.Log("hit");
                if (hit.transform.gameObject.GetComponent<GamePiece>() != null) {
                    GameMove(Mathf.RoundToInt(hit.transform.localPosition.x), Mathf.RoundToInt(hit.transform.localPosition.y));
                }

            }
            else {
               // Debug.Log("miss");
            }
            
        }



    }
}
