using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CustomCursor : MonoBehaviour {

    [SerializeField]
    Texture2D normalCursor;
    [SerializeField]
    Texture2D inverseCursor;
    [SerializeField]
    Texture2D contextMenuCursor;


    
    static Texture2D nCursor;
    
    static Texture2D iCursor;
    
    static Texture2D cMenuCursor;

    private void OnEnable()
    {
        nCursor = normalCursor;
        iCursor = inverseCursor;
        cMenuCursor = contextMenuCursor;
    }

    public static Texture2D NormalCursor { get { return nCursor; } }
    public static Texture2D InverseCursor { get { return iCursor; } }
    public static Texture2D ContextMenuCursor { get { return cMenuCursor; } }
    

}
