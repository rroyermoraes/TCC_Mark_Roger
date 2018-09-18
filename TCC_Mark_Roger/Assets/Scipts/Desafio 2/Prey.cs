using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour {

    [SerializeField]
    private Vector3 direction = new Vector3();
    public int value = 1;
    public float moveSpeed = 1;

	// Use this for initialization
	void Start () {
        Vector3.Normalize(direction);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * Time.deltaTime * moveSpeed,Space.World);
		

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Arrow") {
            Debug.Log("Matou o Alvo");
            GetComponent<Animator>().SetBool("Dead", true);
            moveSpeed = 0;
            ArrowShooter.AddScore(value);
            
            Destroy(this.gameObject, 0.5f);
        }
        
    }
}
