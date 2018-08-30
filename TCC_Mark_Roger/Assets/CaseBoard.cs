using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseBoard : MonoBehaviour {

    public GameObject blankNode;
    public List<CaseNode> nodes = new List<CaseNode>();
    public List<CaseLink> links = new List<CaseLink>();

    public bool AddLink(CaseLink nl) {
        foreach (CaseLink cl in links) {
            if ((cl.a == nl.a && cl.b == nl.b) || (cl.a == nl.b && cl.b == nl.a)) {
                Destroy(nl.gameObject);
                return false;
            }
        }
        if (nl.a == nl.b) {
            Destroy(nl.gameObject);
            return false;
        }
        links.Add(nl);
        return true;
    }
    public void AddBlank() {
        GameObject g = Instantiate(blankNode,this.transform);
        nodes.Add(g.GetComponent<CaseNode>());
    }

    public void AddItemToBoard(CaseNode cn) {

        nodes.Add(cn);
        Instantiate(cn);
         
    }
    

}
