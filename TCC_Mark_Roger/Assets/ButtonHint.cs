
using UnityEngine;

public class ButtonHint : MonoBehaviour {
    [TextArea]
    [SerializeField]
    public string[] hints;


    public void DisplayHint()
    {

        FindObjectOfType<PlayerSpeaker>().PlayerSpeak(hints);
    }


}
