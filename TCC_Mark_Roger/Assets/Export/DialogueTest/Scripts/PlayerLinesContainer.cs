using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLinesContainer : MonoBehaviour {
    public bool useCustomLineColors=false;

    public Button[] uITextContainers;//Ui buttons that have Ui text as childs to interact in the conversation

    public Button scrollDown;//Ui button to scroll down in the conversation optins

    public Button scrollUp;//Ui button to scroll down in the conversation options

    private List<DialogueLineRuntime> currentLines= new List<DialogueLineRuntime>();//Container of the DialogueLines that must fill the containers

    public bool[] textConteinersUsability;

    [SerializeField]
    public int topPointer = 0;
    [SerializeField]
    public int bottomPointer = 0;

    private void OnEnable()
    {
        scrollDown.interactable = false;
        scrollUp.interactable = false;
        UpdatePointers(0);
        textConteinersUsability = new bool[uITextContainers.Length];

    }

    //These pointers keep track of the position that has been scrolled trought
    private void UpdatePointers(int i)
    {
        topPointer += i;
        bottomPointer = topPointer + uITextContainers.Length - 1;
        
    }

    public void ResetPointer()
    {
        topPointer = 0;
        bottomPointer = topPointer + uITextContainers.Length - 1;
        scrollDown.interactable = false;
        scrollUp.interactable = false;

    }

    //Fill the containers with the lines givem, if are more lines than containers the extra lines will not apear,
    // if there are less lines than the number of containers it will fill with a black an disable the button

    public void FillConteiners(List<DialogueLineRuntime> incomingLines)
    {
        
        currentLines.Clear();
        currentLines.AddRange(incomingLines);
      //  print(incomingLines.Count+"In Containers");

        
        for(int i = 0; i < uITextContainers.Length; i++){
            if (i<currentLines.Count)
            {
                uITextContainers[i].GetComponentInChildren<Text>().text = currentLines[i].line;
                uITextContainers[i].GetComponent<Button>().interactable = true;
                textConteinersUsability[i] = true;

            }
            else {
                uITextContainers[i].GetComponentInChildren<Text>().text = "";
                uITextContainers[i].GetComponent<Button>().interactable = false;
                textConteinersUsability[i] = false;
            }

        }
        if ((bottomPointer < currentLines.Count) && (currentLines.Count > uITextContainers.Length))
        {
            scrollDown.interactable = true;

        }

    }

    //scroll the text data from the Dialogue Lines inside the containers enabling and disabling the scroll buttons

    public void ScrollDown() {

        
        UpdatePointers(1);
        for (int i = 0; i < uITextContainers.Length; i++)
        {
            if ((i < currentLines.Count)&&(bottomPointer < currentLines.Count))
            {
                uITextContainers[i].GetComponentInChildren<Text>().text = currentLines[i+topPointer].line;
                uITextContainers[i].GetComponent<Button>().interactable = true;
                textConteinersUsability[i] = true;
                scrollUp.interactable = true;
            }
            else
            {

                scrollDown.interactable = false;
            }

        }
        if(bottomPointer>= currentLines.Count-1){
            scrollDown.interactable = false;
        }
    }

    public void ScrollUp()
    {
        UpdatePointers(-1);

        for (int i = 0; i < uITextContainers.Length; i++)
        {
            if ((i < currentLines.Count) && (bottomPointer >0))
            {
                uITextContainers[i].GetComponentInChildren<Text>().text = currentLines[i + topPointer].line;
                uITextContainers[i].GetComponent<Button>().interactable = true;
                textConteinersUsability[i] = true;
                scrollDown.interactable = true;
            }
            else
            {

                scrollUp.interactable = false;
            }

        }
        if (topPointer == 0)
        {
            scrollUp.interactable = false;
        }
    }

    private void Update()
    {
        if (!DialogueManager.isNPCSpeaking) {
            if ((Input.GetAxis("Mouse ScrollWheel") > 0) && (scrollUp.interactable))
            {
                ScrollUp();
            }
            if ((Input.GetAxis("Mouse ScrollWheel") < 0) && (scrollDown.interactable))
            {
                ScrollDown();
            }

        }
        
    }


}
