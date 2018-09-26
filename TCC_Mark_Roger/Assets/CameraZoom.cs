using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class CameraZoom : MonoBehaviour {
    private PixelPerfectCamera c;
    [Range(1,5)]
    public int zoomLvl = 1;
	// Use this for initialization


    private void OnEnable()
    {
        c = GetComponent<PixelPerfectCamera>();
        if (zoomLvl >= 1) {
            c.refResolutionX = 192 * zoomLvl;
            c.refResolutionY = 108 * zoomLvl;
        }

    }
    // Update is called once per frame
    
    void Update () {
        if (zoomLvl >= 1)
        {
            c.refResolutionX = 192 * zoomLvl;
            c.refResolutionY = 108 * zoomLvl;
        }
    }
}
