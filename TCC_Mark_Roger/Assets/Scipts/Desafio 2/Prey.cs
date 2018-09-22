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
        StartCoroutine(IA());
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * Time.deltaTime * moveSpeed,Space.World);
		

    }
    
    IEnumerator IA()
    {
        float originalSpeed = moveSpeed;
        yield return new WaitForSeconds(0.2f);
        ArrowShooter a = FindObjectOfType<ArrowShooter>();
            if (Random.Range(0, 10) > 3)
            {
                
                GetComponent<Animator>().SetBool("Idle", true);
                yield return new WaitForSeconds(0.3f);
                moveSpeed = 0;
                yield return new WaitForSeconds(Random.Range(0.5f,1f));
                
                GetComponent<Animator>().SetBool("Idle", false);
                //yield return new WaitForSeconds(0.3f);
                moveSpeed = originalSpeed;



        }

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Arrow") {
            Debug.Log("Matou o Alvo");
            GetComponent<Animator>().SetBool("Dead", true);
            moveSpeed = 0;
            FindObjectOfType<ArrowShooter>().AddScore(value);
            
            Destroy(this.gameObject, 0.5f);
        }
        
    }
}
