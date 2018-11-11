using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepAssistant : MonoBehaviour {

    public float speed=0.1f;
    public List<AudioClip> footstepsSounds= new List<AudioClip>();
    private AudioSource a;
    private bool f=false;
    

    private void Start()
    {
        a = GetComponent<AudioSource>();
        StartCoroutine(Walk());
    }

    public void StartWalking()
    {
        f = true;

    }
    public void StopWalking()
    {
        f = false;

    }

    // Use this for initialization

    IEnumerator Walk()
    {
        while (true)
        {
            if (f) { 
            a.Stop();
            a.clip = footstepsSounds[Random.Range(0, footstepsSounds.Count)];
            a.Play();
            yield return new WaitForSeconds(speed);
            }
            else{
                yield return new WaitForEndOfFrame();
            }

        }
        
    }



}
