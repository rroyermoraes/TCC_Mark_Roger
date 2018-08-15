using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToPixel : MonoBehaviour {
	private PixelCamera cam;

    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;

    float d;
	
	void Start() {
		cam = GetComponentInChildren<PixelCamera>();
		
		d = 1f / cam.pixelsPerUnit;
	}

	void LateUpdate() {

        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        Vector3 velocity = targetPos - transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, moveSpeed * Time.deltaTime);



        Vector3 pos = transform.position;
		Vector3 camPos = new Vector3 (pos.x - pos.x % d, pos.y - pos.y % d, pos.z);
		cam.transform.position = camPos;
	}
}
