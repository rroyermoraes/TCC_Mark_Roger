using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Objective {
  //  [SerializeField]
   // private int id;
  //  [SerializeField]
//    private bool enabled = false;
  //  private bool completed = false;
    [SerializeField]
    private ObjectiveData data;

    [SerializeField]
    [TextArea]
    private string description;
    [SerializeField]
    private UnityEvent[] eventTrigger;

   // public int Id{get{return id;}set{id = value;}}

    //public bool Enabled{get{return enabled;}set{enabled = value;}}

   // public bool Completed{get{return completed;}set{completed = value;}}

    public string Description{get{return description;}set{description = value;}}

    public UnityEvent[] EventTrigger{get{return eventTrigger;}set{eventTrigger = value;}}

    public ObjectiveData Data{get{return data;}set{data = value;}}

    public void EnableDisable(bool t) {
        data.enabled = t;
    }
    public void CompleteUncomplete(bool t)
    {
        data.completed = t;
    }



    public Objective() {
        data.enabled = false;
        data.completed = false;
      //  enabled = false;
      //  completed = false;
    }
}


public class ObjectiveManager : MonoBehaviour {

    public List<Objective> objectives = new List<Objective>();


    public void EnableObjective(int id) {
        Objective n = objectives.Find(x => x.Data.id == id);
        if (n != null) {
           // 
           // n.Enabled = true;
            n.EnableDisable(true);
            Debug.Log(n.Description + " - Enabled: " +n.Data.enabled.ToString());
        }
        else {
            Debug.Log("Inmpossivel habilitar - Objectivo nao encontrado");
        }
        
    }

    public void CompleteObjetive(int id)
    {
        Debug.Log("Tentanco completar objetivo " + id);
        Objective n = objectives.Find(x => x.Data.id == id);
        if (n != null)
        {
            if (n.Data.enabled)
            {
             //   n.Completed = true;
                n.CompleteUncomplete(true);
                Debug.Log(n.Description + " - Completed: " + n.Data.completed.ToString());
            }
            else
            {
                Debug.Log("Impossivel completar - Objetivo não habilitado");
            }

        }
        else {
            Debug.Log("Impossivel completar - Objetivo não encontrado");
        }

        
        
    }

}
