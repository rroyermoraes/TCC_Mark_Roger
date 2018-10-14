using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;



public class ContextMenuContainer : MonoBehaviour {

    public static bool contextMenuActive = false;

    public enum DefaultPosition
    {
        UpperLeft,
        UpperCenter,
        UpperRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerCenter,
        LowerRight
    }

    public DefaultPosition defaultOffsetPosition;

    public Camera main;

    public int borderTolerance = 0;

    private bool isContextMenuActive = false;

    private Vector2 containerOffset;

    public RectTransform contextMenu;
    private RectTransform rectTransform { get { return transform as RectTransform; } }

    public GameObject ButtonPrefab;

    public List<GameObject> optionButtons;

    public LayerMask mask = -1;

    public Text label;

    public PlayerController player;

    [HideInInspector]
    public List<ButtonInteraction> actions = new List<ButtonInteraction>();
    private List<ButtonInteraction> blankActions = new List<ButtonInteraction>();



    private void Awake()
    {
       // UpdateOffset();
       // actions.Add(new ButtonInteraction("SP1", SP1));
        //actions.Add(new ButtonInteraction("SP2", SP2));
        //actions.Add(new ButtonInteraction("SP3", SP3));

       
    }


    public void SP1()
    {
        Debug.Log("SP1");
    }
    public void SP2()
    {
        Debug.Log("SP2");
    }
    public void SP3()
    {
        Debug.Log("SP3");
    }





    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            if (isContextMenuActive)
            {
                
                HideContextMenu();
                
            }
            else
            {
                Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                Vector3 sPosition = main.ScreenToWorldPoint(mPosition);
                RaycastHit2D hit = Physics2D.Raycast(sPosition, Vector3.forward, Mathf.Infinity, mask);
                Debug.DrawRay(sPosition, Vector3.forward, Color.red, 5f);
                if (hit)
                {
                    if (Vector3.Magnitude(hit.transform.position - player.transform.position) <= hit.transform.gameObject.GetComponent<InteractableObject>().interactionDistance) {
                        actions = blankActions;
                        actions = hit.transform.gameObject.GetComponent<InteractableObject>().GetButtonInteractions();
                        UpdateInteractions();
                        label.text = hit.transform.gameObject.GetComponent<InteractableObject>().name;
                        ShowContextMenu();
                        contextMenuActive = true;
                    }
                   // Debug.Log("hit");
                }

            }

        }


    }
    public void ShowContextMenu(/*OBJECT TO GIVE THE OPTIONS*/) {
        //Get the ObjectOptions into an array

        isContextMenuActive = true;
        contextMenu.gameObject.SetActive(true);
        UpdateOffset();
        Vector2 pos = CalculteQuadrantPosition();
        contextMenu.anchoredPosition = pos;
        
       

    }
    public void HideContextMenu() {
        foreach (GameObject item in optionButtons)
        {
            Destroy(item);
        }
        optionButtons.Clear();
        optionButtons.TrimExcess();
        isContextMenuActive = false;
        contextMenuActive = false;
        contextMenu.gameObject.SetActive(false);
    }


    void UpdateInteractions() {
        
        for (int i = 0; i < actions.Count; i++)
        {
            GameObject button = Instantiate(ButtonPrefab, transform.Find("VerticalContextMenu"));
            button.GetComponentInChildren<Text>().text = actions[i].name;
            button.GetComponent<ContextMenuButton>().SetButtonIteraction(actions[i].x);
            optionButtons.Add(button);
        }

    }



    #region Positioning

    public void UpdateOffset()
    {
        switch (defaultOffsetPosition)
        {
            case DefaultPosition.UpperLeft:
                containerOffset.x = -contextMenu.rect.width / 2;
                containerOffset.y = contextMenu.rect.height / 2;
                break;
            case DefaultPosition.UpperCenter:
                containerOffset.x = 0;
                containerOffset.y = contextMenu.rect.height / 2;
                break;
            case DefaultPosition.UpperRight:
                containerOffset.x = contextMenu.rect.width / 2;
                containerOffset.y = contextMenu.rect.height / 2;
                break;
            case DefaultPosition.MiddleLeft:
                containerOffset.x = -contextMenu.rect.width / 2;
                containerOffset.y = 0;
                break;
            case DefaultPosition.MiddleCenter:
                containerOffset.x = 0;
                containerOffset.y = 0;
                break;
            case DefaultPosition.MiddleRight:
                containerOffset.x = contextMenu.rect.width / 2;
                containerOffset.y = 0;
                break;
            case DefaultPosition.LowerLeft:
                containerOffset.x = -contextMenu.rect.width / 2;
                containerOffset.y = -contextMenu.rect.height / 2;
                break;
            case DefaultPosition.LowerCenter:
                containerOffset.x = 0;
                containerOffset.y = -contextMenu.rect.height / 2;

                break;
            case DefaultPosition.LowerRight:
                containerOffset.x = contextMenu.rect.width / 2;
                containerOffset.y = -contextMenu.rect.height / 2;
                break;
        }
        
    }

    public Vector2 CalculteQuadrantPosition() {
        Vector2 originalClick;
        Vector2 pos = MouseToRectPointPosition();
        
        originalClick = pos;
        pos += containerOffset;



        if (pos.x - contextMenu.rect.width/2 < 0) {
            pos.x += (contextMenu.rect.width/2) + Mathf.Abs(originalClick.x-pos.x);
        }
        if (pos.x + contextMenu.rect.width/2  > rectTransform.rect.width) {
            pos.x -= (contextMenu.rect.width/2) + Mathf.Abs(originalClick.x - pos.x);
        }
        if (pos.y - contextMenu.rect.height/2 < 0) {
            pos.y += (contextMenu.rect.height / 2) + Mathf.Abs(originalClick.y - pos.y) ;
        }
        if (pos.y + contextMenu.rect.height/2 > rectTransform.rect.height) {
            pos.y -= (contextMenu.rect.height / 2) + Mathf.Abs(originalClick.y - pos.y);
        }
            

        return pos;
    }

    public Vector2 MouseToRectAnchorPosition() {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out localpoint);
        Vector2 normalizedPoint = Rect.PointToNormalized(rectTransform.rect, localpoint);
       // Debug.Log("X:" + normalizedPoint.x + ", " + "Y:" + normalizedPoint.y);
        return normalizedPoint;
    }
    public Vector2 MouseToRectPointPosition()
    {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out localpoint);
        localpoint += new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2);
       // Debug.Log("X:" + localpoint.x + ", " + "Y:" + localpoint.y);
        return localpoint;

    }

#endregion
}
