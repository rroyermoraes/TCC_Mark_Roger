using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Animator anim;
    public float stopAnimBias = 0;
    public NavMeshAgent agent;
    float angle;
    // Update is called once per frame
    void Update()
    {
        if ((EventSystem.current.IsPointerOverGameObject()) || (ContextMenuContainer.contextMenuActive))
        {
           // return;
        }
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                
                //agent.Move(new Vector3(1, 0));
            }
        }

        if(Input.GetAxis("Vertical") !=0 || Input.GetAxis("Horizontal") != 0)
        {
           // agent.isStopped = true;
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * agent.speed, Input.GetAxis("Vertical") * agent.speed,0);
            Debug.Log(movement);
            movement = movement.normalized;
            agent.SetDestination(transform.position+movement);
        }


        
        angle = Mathf.Atan2(agent.velocity.y, agent.velocity.x)*Mathf.Rad2Deg;

        if (agent.velocity.magnitude > stopAnimBias)
        {
            if ((angle >= 0 && angle <= 30)||(angle <= 0 && angle >= -30))
            {
                anim.SetBool("Right", true);

                anim.SetBool("Left", false);
                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
            }
            if (angle > 30 && angle < 150)
            {
                anim.SetBool("Up", true);

                anim.SetBool("Down", false);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }
            if ((angle >= 150 && angle <= 180)||(angle <= -150 && angle >= -180))
            {
                anim.SetBool("Left", true);

                anim.SetBool("Up", false);
                anim.SetBool("Down", false);
                anim.SetBool("Right", false);
            }
            if (angle < -30 && angle > -150)
            {
                anim.SetBool("Down", true);

                anim.SetBool("Up", false);
                anim.SetBool("Right", false);
                anim.SetBool("Left", false);
            }


        }
        else {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
    
    
        

        /*

        if (agent.velocity.magnitude > stopAnimBias)
        {
            if (Mathf.Abs(agent.velocity.x) > Mathf.Abs(agent.velocity.y / 2))
            {
                if (agent.velocity.x > 0)
                {
                    anim.SetBool("Right", true);

                    anim.SetBool("Left", false);
                    anim.SetBool("Up", false);
                    anim.SetBool("Down", false);
                }
                if (agent.velocity.x < 0)
                {
                    anim.SetBool("Left", true);

                    anim.SetBool("Up", false);
                    anim.SetBool("Down", false);
                    anim.SetBool("Right", false);
                }

            }
            else
            {
                if (agent.velocity.y > 0)
                {
                    anim.SetBool("Up", true);

                    anim.SetBool("Down", false);
                    anim.SetBool("Right", false);
                    anim.SetBool("Left", false);
                }
                if (agent.velocity.y < 0)
                {
                    anim.SetBool("Down", true);

                    anim.SetBool("Up", false);
                    anim.SetBool("Right", false);
                    anim.SetBool("Left", false);
                }

            }


        }
        else
        {
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);

        }*/
    }
}
