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
    public Image portraitImage;
    public bool richTextFix = true;
    public float typingSpeed = 1f;
    private string richTextString;
    private bool isNPCSpeaking = false;
    //private Queue<string> playerSentences = new Queue<string>();
    private Queue<SpecialDialogueLine> playerSentences = new Queue<SpecialDialogueLine>();
    [SerializeField]
 //   private float time = 0f;
    private float startTime;
    //private bool startCountdown = false;
    // Use this for initialization
    public UnityEvent whenDialogueEnds;
    private SpecialDialogueLine activeLine;
    private AudioSource aSource;

    // Update is called once per frame

    private void OnEnable()
    {
        aSource = GetComponent<AudioSource>();
    }


    public void NPCSpeak(List<SpecialDialogueLine> nPCLines)
    {
        /*
        string[] sentences;

        
        sentences = new string[nPCLines.Count];
        for (int i = 0; i < nPCLines.Count; i++)
        {
            sentences[i] = nPCLines[i].line;

        }
        */
        activeLine = null;

        playerSentences.Clear();
        foreach (SpecialDialogueLine sentence in nPCLines)
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

        activeLine = playerSentences.Dequeue();
        if (activeLine.contextPortrait)
        {
            portraitImage.sprite = activeLine.contextPortrait;
        }
        

        string toDisplaySentece = activeLine.line;
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
                aSource.Play();
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
            activeLine.specialEvent.Invoke();
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
            activeLine.specialEvent.Invoke();
            DisplayNextNPCSentece();
        }
        
    }
    public void SkipSentence()
    {
        nextSentence = true;
        if (playerSentences.Count == 0 && !isNPCSpeaking)
        {
            activeLine.specialEvent.Invoke();
            whenDialogueEnds.Invoke();
            talkBallon.SetActive(false);
        }

    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return)){
            SkipSentence();
        }
    }
}