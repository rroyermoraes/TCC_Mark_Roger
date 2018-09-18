using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimator : MonoBehaviour {

    public float speed=2f;

    public void AnimateSlide(float v)
    {

        StartCoroutine(Slide(v));
    }

    IEnumerator Slide(float v)
    {
        float elapsedTime=0;
        float initialValue = GetComponent<Slider>().value;
        
        while(elapsedTime<speed)
        {
            GetComponent<Slider>().value = Mathf.Lerp(initialValue, v, elapsedTime/speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        
        Debug.Log("ACE");
        
    }

     void Update()
    {
        
    }


}
