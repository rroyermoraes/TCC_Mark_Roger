using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float moveSpeed = 1;
    public Sprite hitArrow;
    private AudioSource audioS;

    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveSpeed = 0;
        GetComponent<SpriteRenderer>().sprite = hitArrow;
        audioS.pitch = Random.Range(0.9f, 1.1f);
        audioS.Play();
        Destroy(this.gameObject, 0.5f);

    }


}
