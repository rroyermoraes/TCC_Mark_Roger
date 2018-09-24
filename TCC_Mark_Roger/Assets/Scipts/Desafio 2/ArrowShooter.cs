using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShooter : MonoBehaviour {

    public TimedSpawner spawner;
    public Button restartButton;
    public Button endButton;
    public Text pointCount;
    [SerializeField]
    private float cooldownTime = 1;
    private bool cooldown = false;
    [SerializeField]
    private float bounds=1;
    [SerializeField]
    private Vector2 spawnPoint = new Vector2();
    public GameObject arrow;
    [SerializeField]
    private int minScore = 100;
    [HideInInspector]
    public static int score=0;
    private Vector2 mousePosition;
    public float moveSpeed = 0.5f;
    private bool enableArrows=false;
    private bool idle = false;

    public bool Idle
    {
        get
        {
            return idle;
        }

        set
        {
            idle = value;
        }
    }





    // Update is called once per frame
    void Update () {
        if (enableArrows) {
            mousePosition = Input.mousePosition;
            mousePosition = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).x, -bounds, bounds), transform.position.y);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
            if (Input.GetMouseButtonDown(0) && !cooldown)
            {
                Vector3 t = new Vector3(this.transform.position.x + spawnPoint.x, this.transform.position.y + spawnPoint.y, this.transform.position.z);
                Quaternion q = this.transform.rotation;
                Instantiate(arrow, t, q);
                GetComponent<Animator>().SetTrigger("Shoot");
                cooldown = true;
                StartCoroutine(Cooldown());
            }
        }
    }

    public void EnableArrows(bool v) {
        if (v)
        {
            enableArrows = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            enableArrows = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator Cooldown() {

        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;

    }

    public void AddScore(int sc) {
        
        StartCoroutine(AddPointText(sc));
        
    }

    IEnumerator AddPointText(int sc) {
        pointCount.text = score.ToString() + " +" + sc.ToString();
        score += sc;
        yield return new WaitForSeconds(0.5f);

        pointCount.text = score.ToString();

    }


    public void EndChallenge() {
        if (score >= minScore)
        {
            ChallengeCompleted();
        }
        else {

            ChallengeFailed();
        }
    }

    void ChallengeCompleted() {
        Debug.Log("Desafio concluido");
        restartButton.gameObject.SetActive(true);
        endButton.gameObject.SetActive(true);
        EnableArrows(false);
        Cursor.visible = true;
    }
    void ChallengeFailed()
    {
        Debug.Log("Voce falhou, tente novamente");
        restartButton.gameObject.SetActive(true);
        EnableArrows(false);
        Cursor.visible = true;

    }


    public void StartChallenge() {
        spawner.StartSpawnCicle();
        pointCount.text = "0";
        score = 0;
        StartCoroutine(IdleCheck());
    }
    IEnumerator IdleCheck() {
        float lateXPosition;
        yield return new WaitForSecondsRealtime(6);
        while (true) {
            lateXPosition = transform.position.x;
            yield return new WaitForSecondsRealtime(2);
            if (Mathf.Abs(transform.position.x - lateXPosition) < 1) {
                Idle = true;
            }
            else
            {
                Idle = false;
            }
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 point = new Vector3(this.transform.position.x + spawnPoint.x, this.transform.position.y + spawnPoint.y, this.transform.position.z);
        Gizmos.DrawLine( point, new Vector3(point.x,point.y + 0.5f, point.z));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(bounds, -10000, this.transform.position.z), new Vector3(bounds, 10000, this.transform.position.z));
        Gizmos.DrawLine(new Vector3(-bounds, -10000, this.transform.position.z), new Vector3(-bounds, 10000, this.transform.position.z));
    }

}
