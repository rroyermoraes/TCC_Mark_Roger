using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
    private GameObject grabedObject=null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.99f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        if (grabedObject != null)
        {
            grabedObject.transform.position = new Vector3(objPosition.x, objPosition.y, grabedObject.transform.position.z);
        }
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit= Physics2D.Raycast(objPosition, Vector3.forward,Mathf.Infinity);
            Debug.DrawRay(objPosition, Vector3.forward, Color.red, 5f);
            if (hit)
            {
                Debug.Log("hit");

                grabedObject = hit.collider.gameObject;
            }
            else {
                Debug.Log("miss");
            }
        }
        if (Input.GetMouseButtonUp(0)) {
            grabedObject = null;
        }
		
	}


}
