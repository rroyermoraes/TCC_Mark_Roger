using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float moveSpeed = 1;

	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveSpeed = 0;
        Destroy(this.gameObject, 0.5f);

    }


}
