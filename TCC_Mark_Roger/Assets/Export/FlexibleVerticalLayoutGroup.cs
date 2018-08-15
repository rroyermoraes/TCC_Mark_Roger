using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class FlexibleVerticalLayoutGroup : MonoBehaviour {

    public float verticalMinimalCellSize;
    public float verticalMaximalCellSize;
    public int ignoreLayoutCount=0;

    RectTransform rt;

    // Use this for initialization
    void OnEnable () {

        rt = GetComponent<RectTransform>();
        
        rt.sizeDelta = new Vector2(rt.rect.width,(transform.childCount - ignoreLayoutCount) * verticalMinimalCellSize);
	}

    



}
