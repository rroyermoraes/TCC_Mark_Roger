using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour {
    public GameObject player;
    public Transform tpoint;
    public float interactionDistance;
    public LayerMask mask;
    float d;

    // Use this for initialization

    public void LateUpdate()
    {
        
        if (Input.GetMouseButtonDown(1))
        {


            d = Vector3.Magnitude(player.transform.position - transform.position);
            if (d <= interactionDistance)
            {
                Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                Vector3 sPosition = Camera.main.ScreenToWorldPoint(mPosition);
                RaycastHit2D hit = Physics2D.Raycast(sPosition, Vector3.forward, Mathf.Infinity,mask);

                if (hit && hit.transform == transform )
                {
                    player.GetComponent<NavMeshAgent>().Warp(tpoint.position);
                    Camera.main.GetComponent<SnapToPixel>().enabled = false;
                    Camera.main.transform.position = new Vector3(tpoint.position.x, tpoint.position.y, Camera.main.transform.position.z);
                    Camera.main.GetComponent<SnapToPixel>().enabled = true;

                }

                
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(tpoint.position, 0.1f);

#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, interactionDistance);
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, 0.1f);
#endif
    }

}
