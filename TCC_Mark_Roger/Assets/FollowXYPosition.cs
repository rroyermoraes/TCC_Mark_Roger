using UnityEngine;

public class FollowXYPosition : MonoBehaviour {

    public Transform target;
	
	void Update () {
        transform.position = new Vector3(target.position.x,target.position.y,0f);

	}
}
