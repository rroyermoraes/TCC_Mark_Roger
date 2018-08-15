using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {
    public LineRenderer linePrefab;
    private GameObject activeLine;
    private GameObject source;
    private GameObject destination;
    private bool locked=false;



	void Update () {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (locked)
        {
            activeLine.GetComponent<LineRenderer>().enabled = true;
            activeLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(screenPosition.x, screenPosition.y, 0));
            
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            
            RaycastHit2D hit = Physics2D.Raycast(screenPosition, Vector3.forward, Mathf.Infinity);
            Debug.DrawRay(screenPosition, Vector3.forward, Color.red, 20f);
            Debug.Log("hey");
            if (hit)
            {
                if (locked)
                {
                    Debug.Log("hit");
                    activeLine.GetComponent<GraphEdge>().b = hit.collider.gameObject.GetComponent<GraphNode>();
                    locked = false;
                    activeLine = null;
                }
                else {
                    Debug.Log("hit");
                    activeLine = Instantiate(linePrefab.gameObject, hit.transform.parent);
                    activeLine.GetComponent<LineRenderer>().enabled = false;
                    activeLine.GetComponent<GraphEdge>().a = hit.collider.gameObject.GetComponent<GraphNode>();
                    activeLine.GetComponent<GraphEdge>().b = hit.collider.gameObject.GetComponent<GraphNode>();
                    activeLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(screenPosition.x, screenPosition.y, 10f));
                    
                    locked = true;
                    
                }
                
            }
            else
            {
                activeLine.GetComponent<LineRenderer>().SetPosition(1, activeLine.GetComponent<LineRenderer>().GetPosition(0));
                locked = false;
                if (activeLine != null) {
                    Destroy(activeLine.gameObject);
                }
                activeLine = null;
                Debug.Log("miss");
            }
        }



    }
}
