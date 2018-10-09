using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class CameraZoom : MonoBehaviour {
    private PixelPerfectCamera c;
    public bool mouseWheelZoom = false;
    [Range(1,6)]
    public int zoomLvl = 1;
	// Use this for initialization


    private void OnEnable()
    {
        c = GetComponent<PixelPerfectCamera>();

        //c.refResolutionX = 192 * zoomLvl;

        //  c.refResolutionY = 108 * zoomLvl;
        c.refResolutionX = Screen.width / 10 * zoomLvl;
        c.refResolutionY = Screen.height / 10 * zoomLvl;


    }
    // Update is called once per frame
    
    void Update () {

        //  c.refResolutionX = 192 * zoomLvl;
        //   c.refResolutionY = 108 * zoomLvl;
        if (Input.mouseScrollDelta.y < 0 && mouseWheelZoom)
        {
            if (zoomLvl < 5) {
                zoomLvl++;

            }
        }
        if (Input.mouseScrollDelta.y > 0 && mouseWheelZoom)
        {
            if (zoomLvl > 1)
            {
                zoomLvl--;
            }
        }

        c.refResolutionX = Screen.width / 10 * zoomLvl;
        c.refResolutionY = Screen.height / 10 * zoomLvl;

    }
}
