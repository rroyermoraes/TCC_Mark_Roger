using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShooter : MonoBehaviour {

    public TimedSpawner spawner;
    public Button restartButton;
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
    


	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).x,-bounds,bounds), transform.position.y);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        if (Input.GetMouseButtonDown(0) && !cooldown) {
            Vector3 t = new Vector3(this.transform.position.x+spawnPoint.x, this.transform.position.y+spawnPoint.y, this.transform.position.z);
            Quaternion q = this.transform.rotation;
            Instantiate(arrow,t,q);
            cooldown = true;
            StartCoroutine(Cooldown());
        }
        
    }
    

    IEnumerator Cooldown() {

        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;

    }

    public static void AddScore(int sc) {
        score += sc;
    }

    public void EndChallenge() {
        if (score >= minScore)
        {
            Debug.Log("Desafio concluido");
        }
        else {
            Debug.Log("Voce falhou, tente novamente");
            restartButton.gameObject.SetActive(true);

        }
    }

    public void StartChallenge() {
        spawner.StartSpawnCicle();
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
