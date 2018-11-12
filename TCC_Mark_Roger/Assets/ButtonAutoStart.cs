using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonAutoStart : MonoBehaviour {
	void Start () {
                ExecuteEvents.Execute(GetComponent<Button>().gameObject, new BaseEventData(FindObjectOfType<EventSystem>()), ExecuteEvents.submitHandler);
    }
}
