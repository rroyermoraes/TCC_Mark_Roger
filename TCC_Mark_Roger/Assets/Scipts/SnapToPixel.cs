using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToPixel : MonoBehaviour {
	//private PixelCamera cam;
    public int pixelPerUnit = 16;
    public GameObject followTarget;
    private Vector3 targetPos;
    public Vector2 offset;
    public float moveSpeed;

    float d;
	
	void Start() {
		//cam = GetComponentInChildren<PixelCamera>();
		
		d = 1f / pixelPerUnit;
	}

	void LateUpdate() {
        
        targetPos = new Vector3(followTarget.transform.position.x +offset.x, followTarget.transform.position.y+offset.y, transform.position.z);
       // Vector3 velocity = targetPos - transform.position;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 5.0f, moveSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);


    
        Vector3 pos = transform.position;
		Vector3 camPos = new Vector3 (pos.x - pos.x % d, pos.y - pos.y % d, pos.z);
		transform.position = camPos;
	}
}
