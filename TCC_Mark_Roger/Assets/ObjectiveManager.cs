using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective {
    [SerializeField]
    private int id;
    [SerializeField]
    private bool enabled = false;
    private bool completed = false;
    [SerializeField]
    [TextArea]
    private string description;

    public int Id{get{return id;}set{id = value;}}

    public bool Enabled{get{return enabled;}set{enabled = value;}}

    public bool Completed{get{return completed;}set{completed = value;}}

    public string Description{get{return description;}set{description = value;}}

    public Objective() {
        enabled = false;
        completed = false;
    }
}


public class ObjectiveManager : MonoBehaviour {

    public List<Objective> objectives = new List<Objective>();


    public void EnableObjective(int id) {
        Objective n = objectives.Find(x => x.Id == id);
        if (n != null) {
            n.Enabled = true;
            Debug.Log(n.Description + " - Enabled: " + n.Enabled.ToString());
        }
        else {
            Debug.Log("Inmpossivel habilitar - Objectivo nao encontrado");
        }
        
    }

    public void CompleteObjetive(int id)
    {
        Debug.Log("Tentanco completar objetivo " + id);
        Objective n = objectives.Find(x => x.Id == id);
        if (n != null)
        {
            if (n.Enabled)
            {
                n.Completed = true;
                Debug.Log(n.Description + " - Completed: " + n.Completed.ToString());
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
