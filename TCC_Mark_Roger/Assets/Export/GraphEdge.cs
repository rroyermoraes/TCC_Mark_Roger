using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEdge : MonoBehaviour {

    public GraphNode a;
    public GraphNode b;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<LineRenderer>().SetPosition(0, a.transform.position);
        GetComponent<LineRenderer>().SetPosition(1, b.transform.position);

    }
}
