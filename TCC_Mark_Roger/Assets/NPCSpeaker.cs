using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NPCSpeaker : MonoBehaviour
{
    
    private bool nextSentence=false;
    public GameObject talkBallon;
    public Text spechText;
    public bool richTextFix = true;
    public float typingSpeed = 1f;
    private string richTextString;
    public bool isNPCSpeaking = false;
    private Queue<string> playerSentences = new Queue<string>();
    [SerializeField]
 //   private float time = 0f;
    private float startTime;
    //private bool startCountdown = false;
    // Use this for initialization
    public UnityEvent whenDialogueEnds;


    // Update is called once per frame


    public void NPCSpeak(List<SpecialDialogueLine> nPCLines)
    {
        string[] sentences;

        
        sentences = new string[nPCLines.Count];
        for (int i = 0; i < nPCLines.Count; i++)
        {
            sentences[i] = nPCLines[i].line;

        }
        


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
            return;
        }

        string toDisplaySentece = playerSentences.Dequeue();
        StartCoroutine(TypeSentence(toDisplaySentece));
    }

    IEnumerator TypeSentence(string sentence)
    {
        spechText.text = "";
        richTextString = "";
        
        if (richTextFix)
        {
            isNPCSpeaking = true;
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
                if (nextSentence)
                {
                    nextSentence = false;
                    break;
                    
                }
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
            spechText.text = richTextString;


            isNPCSpeaking = false;
            while (!nextSentence)
            {
                yield return new WaitForEndOfFrame();
            }
            nextSentence = false;
            
            DisplayNextNPCSentece();
        }
        else
        {
            isNPCSpeaking = true;
            foreach (char letter in sentence.ToCharArray())
            {
                spechText.text += letter;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
            isNPCSpeaking = false;
            while (!nextSentence)
            {
                yield return new WaitForEndOfFrame();
            }
            nextSentence = false;
            
            DisplayNextNPCSentece();
        }
        
    }
    public void SkipSentence()
    {
        nextSentence = true;
        if (playerSentences.Count == 0 && !isNPCSpeaking)
        {
            whenDialogueEnds.Invoke();
            talkBallon.SetActive(false);
        }

    }
}