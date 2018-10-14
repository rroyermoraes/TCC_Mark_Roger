using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    public new string name;
    public float interactionDistance = 3f;
    public Texture2D mouseOverCursor;
    [SerializeField]
    protected List<ButtonInteraction> bInteractions = new List<ButtonInteraction>();

    public virtual void Take()
    {
        string[] s = new string[] { "Pegando um(a) " + name };
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        Debug.Log(s);
        

    }
    public virtual void Read()
    {
        string[] s = new string[] { "... " };
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        Debug.Log(s);

    }
    public virtual void Inspect()
    {
        string[] s = new string[] { "Isso é um(a) " + name };
        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(s);
        Debug.Log(s);

    }
    public virtual void Speak()
    {
        Debug.Log("Estou falando com " + name);

    }
    public List<ButtonInteraction> GetButtonInteractions()
    {
        return bInteractions;
    }

    private void OnMouseOver()
    {
        Cursor.SetCursor(CustomCursor.ContextMenuCursor, Vector2.zero,CursorMode.Auto);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(CustomCursor.NormalCursor, Vector2.zero, CursorMode.Auto);
    }
}
