using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class GamePiece : MonoBehaviour {
    [SerializeField]
    private Sprite pieceSprite;
    [SerializeField]
    private bool topConnection = false;
    [SerializeField]
    private bool bottomConnection = false;
    [SerializeField]
    private bool leftConnection = false;
    [SerializeField]
    private bool rightConnection = false;
    [SerializeField]
    private bool blank = false;
    private SpriteRenderer r;
    private bool flooded = false;

    public bool LeftConnection
    {
        get
        {
            return leftConnection;
        }

        set
        {
            leftConnection = value;
        }
    }

    public bool BottomConnection
    {
        get
        {
            return bottomConnection;
        }

        set
        {
            bottomConnection = value;
        }
    }

    public bool TopConnection
    {
        get
        {
            return topConnection;
        }

        set
        {
            topConnection = value;
        }
    }

    public bool RightConnection
    {
        get
        {
            return rightConnection;
        }

        set
        {
            rightConnection = value;
        }
    }

    public bool GetFlooded() {
        return flooded;
    }
    public void Flood() {
        flooded = true;
        r.color = Color.blue;
    }



    public bool GetBlank() {
        return blank;
    }

    private void OnEnable()
    {
        r = GetComponent<SpriteRenderer>();
       
    }
    private void Update()
    {
        r.sprite = pieceSprite;
    }

}

