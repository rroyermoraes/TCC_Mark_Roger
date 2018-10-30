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
    [SerializeField]
    Texture2D walkCursor;


    
    static Texture2D nCursor;
    
    static Texture2D iCursor;
    
    static Texture2D cMenuCursor;

    static Texture2D wCursor;

    private void OnEnable()
    {
        nCursor = normalCursor;
        iCursor = inverseCursor;
        cMenuCursor = contextMenuCursor;
        wCursor = walkCursor;
    }

    public static Texture2D NormalCursor { get { return nCursor; } }
    public static Texture2D InverseCursor { get { return iCursor; } }
    public static Texture2D ContextMenuCursor { get { return cMenuCursor; } }
    public static Texture2D WalkCursor { get { return wCursor; } }


}
