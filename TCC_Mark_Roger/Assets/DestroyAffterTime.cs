using UnityEngine;

public class DestroyAffterTime : MonoBehaviour {
    public float time = 5;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, time);
	}
}
