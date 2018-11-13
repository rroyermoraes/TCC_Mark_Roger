using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class CutsceneTeleporter : MonoBehaviour {

    public NavMeshAgent player;
    public Transform tPoint;
    public float eventDelayTime=0f;
    public UnityEvent postTPEventTriggers;


    // Use this for initialization


    public void Teleport() {
        player.GetComponent<NavMeshAgent>().Warp(tPoint.position);
        Camera.main.GetComponent<SnapToPixel>().enabled = false;
        Camera.main.transform.position = new Vector3(tPoint.position.x, tPoint.position.y, Camera.main.transform.position.z);
        Camera.main.GetComponent<SnapToPixel>().enabled = true;
        StartCoroutine(Events());
    }


    IEnumerator Events() {
        yield return new WaitForSecondsRealtime(eventDelayTime);
        postTPEventTriggers.Invoke();

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(tPoint.position, 0.1f);

#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, 0.1f);
#endif
    }
}
