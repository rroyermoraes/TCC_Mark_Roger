using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;
	
	// Update is called once per frame
	void Update () {
        if ((EventSystem.current.IsPointerOverGameObject()) || (ContextMenuContainer.contextMenuActive))
        {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {

            Ray ray =cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                //move agent
                agent.SetDestination(hit.point);
            }                       
        }
	}
}
