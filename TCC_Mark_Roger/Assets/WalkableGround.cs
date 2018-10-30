using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableGround : MonoBehaviour {

    private void OnMouseOver()
    {
        Cursor.SetCursor(CustomCursor.WalkCursor, Vector2.zero, CursorMode.Auto);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(CustomCursor.NormalCursor, Vector2.zero, CursorMode.Auto);
    }
}
