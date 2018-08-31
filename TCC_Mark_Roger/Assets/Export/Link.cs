using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class Link : MonoBehaviour {
    // public LineRenderer linePrefab;
    public UILineRenderer lr;
    [SerializeField]
    private GameObject activeLine;
    private GameObject source;
    private GameObject destination;
    private bool locked=false;
    private Vector3 mousePosition = new Vector3();
    private Vector3 screenPosition;
    public int siblingOffset = 1;



    public void TestRightClick(GameObject x) {
        Debug.Log(x.name);
        if (locked)
        {
            Debug.Log("hit1");
            activeLine.GetComponent<CaseLink>().b = x.GetComponent<CaseNode>();
            activeLine.GetComponent<UILineConnector>().transforms[1] = x.GetComponent<RectTransform>();
            locked = false;
            FindObjectOfType<CaseBoard>().AddLink(activeLine.GetComponent<CaseLink>());
            activeLine = null;

        }
        else
        {

            Debug.Log("hit2");
            activeLine = Instantiate(lr.gameObject, this.transform);
            activeLine.transform.SetSiblingIndex(siblingOffset);
            activeLine.GetComponent<UILineRenderer>().enabled = false;
            activeLine.GetComponent<CaseLink>().a = x.GetComponent<CaseNode>();
            activeLine.GetComponent<CaseLink>().b = x.GetComponent<CaseNode>();
            activeLine.GetComponent<UILineConnector>().transforms[0] = x.GetComponent<RectTransform>();
            activeLine.GetComponent<UILineConnector>().transforms[1] = x.GetComponent<RectTransform>();
            activeLine.GetComponent<UILineRenderer>().Points[1] = new Vector2(mousePosition.x, mousePosition.y);
            locked = true;

        }
    }

    public void ClearLink() {
        if(activeLine!=null)
        Destroy(activeLine.gameObject);
        activeLine = null;
        locked = false;

    }

    void Update()
    {
        if (locked)
        {
            activeLine.GetComponent<UILineRenderer>().enabled = true;
            

        }

    }

    /*
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
                    
                    
                    Debug.Log("hit1");
                    activeLine.GetComponent<CaseLink>().b = hit.collider.gameObject.GetComponent<CaseNode>();
                    locked = false;
                    FindObjectOfType<CaseBoard>().AddLink(activeLine.GetComponent<CaseLink>());
                    activeLine = null;

                }
                else {

                    Debug.Log("hit2");
                    activeLine = Instantiate(linePrefab.gameObject, hit.transform.parent);
                    activeLine.GetComponent<LineRenderer>().enabled = false;
                    activeLine.GetComponent<CaseLink>().a = hit.collider.gameObject.GetComponent<CaseNode>();
                    activeLine.GetComponent<CaseLink>().b = hit.collider.gameObject.GetComponent<CaseNode>();
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



    }*/
}
