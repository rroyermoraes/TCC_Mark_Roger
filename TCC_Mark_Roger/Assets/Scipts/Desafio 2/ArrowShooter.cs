using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour {

    private float cooldown = 1;
    [SerializeField]
    private Vector2 bounds = new Vector2();
    public GameObject arrow;
    
    private Vector2 mousePosition;
    public float moveSpeed = 0.5f;
    


	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(mousePosition).x,bounds.x,bounds.y), transform.position.y);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);



	}
}
