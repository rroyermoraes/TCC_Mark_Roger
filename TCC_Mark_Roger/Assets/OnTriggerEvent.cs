using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour {

    public UnityEvent onTriggerEnterEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Triggered");
        onTriggerEnterEvents.Invoke();
    }
}
