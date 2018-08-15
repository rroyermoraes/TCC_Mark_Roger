using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerControllerOld : MonoBehaviour {



//private Vector3 angleUpRight = new Vector3(0.894f, 0.447f, 0);
//private Vector3 angleUpLeft = new Vector3(0.894f, -0.447f, 0);
// private Vector3 angleDownRight = new Vector3(-0.894f, 0.447f, 0);
//private Vector3 angleDownLeft = new Vector3(-0.894f, -0.447f, 0);

    public LayerMask filtro;
    private Vector3 targetPosition;
    public float speed = 10.0F;
    private bool move = false;

    void FixedUpdate()
    {


        if ((EventSystem.current.IsPointerOverGameObject())||(ContextMenuContainer.contextMenuActive)){
            return;
        }

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);



        float htranslation = Input.GetAxis("Horizontal") * speed;
        float vtranslation = Input.GetAxis("Vertical") * speed;
        //float h;
       // float v;

        htranslation *= Time.deltaTime;
        vtranslation *= Time.deltaTime;
      //  h = htranslation - vtranslation;
      //  v = (htranslation + vtranslation) / 2;

        if (!move)
        transform.Translate(htranslation, vtranslation*0.75f, 0);

        if (move)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if ((transform.position - targetPosition).magnitude < 0.1)
            {
                move = false;
               // print("stop");
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit= Physics2D.Raycast(screenPosition, Vector3.forward, Mathf.Infinity,filtro);
            Debug.DrawRay(screenPosition, Vector3.forward, Color.red, 5f);
            if (hit)
            {
               // Debug.Log("hit");
                targetPosition = hit.point;
                move = true;

            }
            else
            {
              //  Debug.Log("miss");
            }
        }









        if (Input.GetKey(KeyCode.RightArrow))
        {

            
            //transform.Translate(htranslation, vtranslation, 0);
            // transform.Translate(Vector3.right * Time.deltaTime * speed);
        }/*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }*/





    }

  

    
  

}
