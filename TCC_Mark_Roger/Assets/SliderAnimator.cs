using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimator : MonoBehaviour {

    public float speed=2f;
    public bool fadeSlide = false;
    private Slider sl;

    public void AnimateSlide(float v)
    {
        sl = GetComponent<Slider>();
        if (fadeSlide) {
            StartCoroutine(FadeSlide(v));
        }
        else {
            StartCoroutine(Slide(v));
        }
        
    }

    IEnumerator FadeSlide(float v)
    {
        float elapsedTime=0;
        float initialValue = sl.value;
        Color fillColor = new Color(1, 1, 1, 0);
        sl.fillRect.GetComponent<Image>().color = fillColor;
        sl.handleRect.GetComponent<Image>().color = fillColor;
        
        while (fillColor.a <= 0.5f) {
            fillColor.a += 0.02f;
            sl.fillRect.GetComponent<Image>().color = fillColor;
            sl.handleRect.GetComponent<Image>().color = fillColor;
            yield return new WaitForEndOfFrame();
        }

        while(elapsedTime<speed)
        {
            sl.value = Mathf.Lerp(initialValue, v, elapsedTime/speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        sl.value = v;
        while (fillColor.a >= 0)
        {
            fillColor.a -= 0.02f;
            sl.fillRect.GetComponent<Image>().color = fillColor;
            sl.handleRect.GetComponent<Image>().color = fillColor;
            yield return new WaitForEndOfFrame();
        }

    }


    IEnumerator Slide(float v)
    {
        float elapsedTime = 0;
        float initialValue = sl.value;
        while (elapsedTime < speed)
        {
            sl.value = Mathf.Lerp(initialValue, v, elapsedTime / speed);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        sl.value = v;

    }

    void Update()
    {
        
    }


}
