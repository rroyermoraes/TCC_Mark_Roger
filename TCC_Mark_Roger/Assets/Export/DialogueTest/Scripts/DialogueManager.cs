using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public GameObject dialogueScreen;
    public DialogueNPC activeNPCDialogue;
    public PlayerLinesContainer linesContainer;
    public Text npcLineContainer;
    public float typingSpeed = 1f;
    private bool lockedOptions = false;
    [SerializeField]
    private Image npcPortrait;
    public Sprite defaultEmptyPortrait;
    private Queue<string> npcSentences = new Queue<string>();
    private bool isTyping = false;
    public static bool isNPCSpeaking = false;
    private List<DialogueLineSerial> activeLines= new List<DialogueLineSerial>();
    private GameObject givenItem;
    public GameObject GivenItem { get { return givenItem; } }
    private string richTextString = "";
    public bool richTextFix = true;
    private bool closingDialogue = false;



    // Start the conversation
    public void StartDialogue(DialogueNPC npc)
    {
        activeNPCDialogue = npc;
        if (activeNPCDialogue.activeNPCState) {
            dialogueScreen.SetActive(true);
            linesContainer.ResetPointer();
            closingDialogue = false;
            activeNPCDialogue.activeDialogueState = activeNPCDialogue.activeNPCState.defualtDialogueState;
            npcLineContainer.text = "";
            if (activeNPCDialogue.activeNPCState.portrait != null) {
                npcPortrait.sprite = activeNPCDialogue.activeNPCState.portrait;
            }
            else {
                if (activeNPCDialogue.DefaltNPCPortrait!=null) {
                    npcPortrait.sprite = activeNPCDialogue.DefaltNPCPortrait;
                }
                else {
                    npcPortrait.sprite = defaultEmptyPortrait;
                }
                    
            }
            NPCSpeak(new string[] { activeNPCDialogue.activeNPCState.defualtDialogueState.greeting });
            if (!activeNPCDialogue.activeDialogueState.blankState)
            {
                RefreshActiveLines();
                lockedOptions = false;
                //activeLines.Add(activeNPCDialogue.activeDialogueState.endDialogueLine);
                linesContainer.FillConteiners(activeLines);
                StartCoroutine(ShowResponses());
            }
            else {
                foreach (Button button in linesContainer.uITextContainers)
                {
                    button.interactable = false;
                    button.GetComponentInChildren<Text>().enabled = false;
                }
                StartCoroutine(EndConversation(1f));

            }
            


        }

    }


    private void RefreshActiveLines() {
        activeLines.Clear();
        foreach (DialogueLineSerial line in activeNPCDialogue.activeDialogueState.lines)
        {
            if (!line.isHidden) {
                activeLines.Add(line);
            }
        }
        activeLines.Add(activeNPCDialogue.activeDialogueState.endDialogueLine);
    }



    private void NPCSpeak(string[] sentences)
    {
        npcLineContainer.text = "";
        richTextString = "";
        isNPCSpeaking = true;
       // Debug.LogError(isNPCSpeaking);
        npcSentences.Clear();
        foreach (string sentence in sentences)
        {
            npcSentences.Enqueue(sentence);
        }
        DisplayNextNPCSentece();
    }

    public void DisplayNextNPCSentece()
    {
        if (npcSentences.Count == 0)
        {
            isNPCSpeaking = false;
            return;
        }
        string toDisplaySentece = npcSentences.Dequeue();
        if (isTyping)
        {
            StopAllCoroutines();
            npcLineContainer.text = "";
            richTextString = "";
            npcLineContainer.text = toDisplaySentece;

        }
        StartCoroutine(TypeSentence(toDisplaySentece));
    }

    IEnumerator TypeSentence(string sentence) {
        
        if (richTextFix){
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

                npcLineContainer.text += letter;

                yield return new WaitForSecondsRealtime(typingSpeed);

            }
            richTextString += "\n";
            npcLineContainer.text = richTextString;
            yield return new WaitForSeconds(0.25f);
            DisplayNextNPCSentece();
        }
        else{
            foreach (char letter in sentence.ToCharArray())
            {

                npcLineContainer.text += letter;

                yield return new WaitForSecondsRealtime(typingSpeed);

            }
            npcLineContainer.text += "\n";
            yield return new WaitForSeconds(0.25f);
            DisplayNextNPCSentece();
        }
        
        

    }
    /*
    IEnumerator TypeSentence(string sentence)
    {

        

    }
    */
    IEnumerator ShowResponses()
    {
        while (true){
            for (int i = 0; i < linesContainer.uITextContainers.Length; i++)
            {
                linesContainer.uITextContainers[i].interactable = linesContainer.textConteinersUsability[i] & false;
                linesContainer.uITextContainers[i].GetComponentInChildren<Text>().enabled = linesContainer.textConteinersUsability[i] & false;
            }

            while (isNPCSpeaking)
            {
                yield return null;
            }

            for (int i = 0; i < linesContainer.uITextContainers.Length; i++)
            {
                linesContainer.uITextContainers[i].interactable = linesContainer.textConteinersUsability[i] & true;
                linesContainer.uITextContainers[i].GetComponentInChildren<Text>().enabled = linesContainer.textConteinersUsability[i] & true;
            }
            yield return null;
        }
        
    }


    IEnumerator EndConversation(float time)
    {
        while (isNPCSpeaking) {
            yield return null;
        }
        isNPCSpeaking = true;
        yield return new WaitForSecondsRealtime(time);
        StopCoroutine(ShowResponses());
        dialogueScreen.SetActive(false);
    }


    public void CloseDialogue() {
        DialogueLineSerial endLine=null;
        endLine = activeNPCDialogue.activeDialogueState.endDialogueLine;
        if (endLine!=null&&!closingDialogue&&!isNPCSpeaking) {
            NPCSpeak(endLine.responses);
            closingDialogue = true;
            StartCoroutine(EndConversation(1f));
        }else
            {
                return;
            }
    }

    public void UnlockDialogueLine(DialogueState state,int i) {
        state.lines[i].isHidden = false;
        //Debug.Log(state.lines[i].line + state.lines[i].isHidden.ToString());
    }

    public void SelectDialogueOption(int opt)
    {

        if (!lockedOptions){
            //show the selected line responses texts, sending the strings to the NPCSpeak method to be writen. 
            NPCSpeak(activeLines[opt + linesContainer.topPointer].responses);

            //check if you need to change the NPC Portrait for the response of the selected line
            if (activeLines[opt + linesContainer.topPointer].specialPortrait != null)
            {
                npcPortrait.sprite = activeLines[opt + linesContainer.topPointer].specialPortrait;
            }


            //check if this option calls for a change to a diferente NPCState
            if (activeLines[opt + linesContainer.topPointer].targetNPCDialogue != null)
            {
                activeNPCDialogue.ChageNPCState(activeLines[opt + linesContainer.topPointer].targetNPCDialogue);
            }

            //check if this option calls for a change to another dialogueState
            if (activeLines[opt + linesContainer.topPointer].nextState != null)
            {
                activeNPCDialogue.ChangeDialogueState(activeLines[opt + linesContainer.topPointer].nextState);
                RefreshActiveLines();
                linesContainer.ResetPointer();
                activeLines.Add(activeNPCDialogue.activeDialogueState.endDialogueLine);
                linesContainer.FillConteiners(activeLines);
                StartCoroutine(ShowResponses());
            }






            //check if this option will unlock another ones, if so, unlocks them, and if one of them is within the same dialogue state, it will reset the options containers
            if (activeLines[opt + linesContainer.topPointer].linesToUnlock != null)
            {
                foreach (UnlockableLine line in activeLines[opt + linesContainer.topPointer].linesToUnlock)
                {
                    UnlockDialogueLine(line.state, line.index);
                    if (line.state == activeNPCDialogue.activeDialogueState)
                    {
                        RefreshActiveLines();
                        linesContainer.ResetPointer();
                        linesContainer.FillConteiners(activeLines);
                        StartCoroutine(ShowResponses());
                    }
                }

            }


            //---------------------------------------------------------------------------------
            //---------------Call a method to give the player the iten-------------------------
            //---------------------------------------------------------------------------------


            activeLines[opt + linesContainer.topPointer].eventsX.Invoke();


            if(activeLines[opt + linesContainer.topPointer].specialEventTrigger) {
                activeNPCDialogue.GetComponent<CustomEventTrigger>().action.Invoke();
            }


            //check if this option will close de dialogue
            if (activeLines[opt + linesContainer.topPointer].exitDialogue|| activeLines[opt+linesContainer.topPointer]==activeNPCDialogue.activeDialogueState.endDialogueLine)
            {
                lockedOptions = true;
                StartCoroutine(EndConversation(0.5f));
            }
        }
        
    }

}
