using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeaker : MonoBehaviour {

    public GameObject talkBallon;
    public Text spechText;
    public bool richTextFix = true;
    public float typingSpeed = 1f;
    private string richTextString;
   // private bool isPlayerSpeaking = false;
    private Queue<string> playerSentences = new Queue<string>();
    [SerializeField]
    private float time=0f;
    private float startTime;
    //private bool startCountdown = false;
    // Use this for initialization

	
	// Update is called once per frame
	void Update () {
        time = time - Time.deltaTime;
        if (time < 0){
            talkBallon.SetActive(false);
        }
	}


    public void PlayerSpeak(string[] sentences)
    {
        talkBallon.SetActive(true);
        time = 1;
       // startCountdown = true;
        spechText.text = "";
        richTextString = "";
       // isPlayerSpeaking = true;
        playerSentences.Clear();
        foreach (string sentence in sentences)
        {
            playerSentences.Enqueue(sentence);
        }
        DisplayNextNPCSentece();
    }

    public void DisplayNextNPCSentece()
    {
        if (playerSentences.Count == 0)
        {
           // isPlayerSpeaking = false;
            return;
        }
        string toDisplaySentece = playerSentences.Dequeue();
        StartCoroutine(TypeSentence(toDisplaySentece));
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (richTextFix)
        {
            string dialogueString = "";
            bool ignoreChar = false;

            foreach (char letter in sentence.ToCharArray())
            {
                if (letter.Equals('<'))
                {
                    ignoreChar = true;
                }

                if (!ignoreChar)
                {

                    dialogueString += letter;

                }
                if (letter.Equals('>'))
                {
                    ignoreChar = false;
                }

            }
            richTextString += sentence;

            foreach (char letter in dialogueString.ToCharArray())
            {

                spechText.text += letter;
                
                yield return new WaitForSecondsRealtime(typingSpeed);
                time = 2;

            }
           // richTextString += "\n";
            spechText.text = richTextString;
            DisplayNextNPCSentece();
        }
        else
        {
            foreach (char letter in sentence.ToCharArray())
            {

                spechText.text += letter;

                yield return new WaitForSecondsRealtime(typingSpeed);

            }
            //spechText.text += "\n";

            DisplayNextNPCSentece();
        }
    }

}

