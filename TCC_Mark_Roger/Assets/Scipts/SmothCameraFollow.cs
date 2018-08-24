using UnityEngine;

public class SmothCameraFollow : MonoBehaviour {

    public Transform target;
    public float smothSpeed=0.5f;

    private void LateUpdate()
    {
        transform.position = target.position + new Vector3(0,0,-10);
    }


}
