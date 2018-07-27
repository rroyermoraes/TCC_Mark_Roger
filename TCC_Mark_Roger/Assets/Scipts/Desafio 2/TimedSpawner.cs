using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimedObject
{
    public float time;
    public GameObject objectToSpawn;

    public TimedObject(float t,GameObject x)
    {
        time = t;
        objectToSpawn = x;
    }


}

public class TimedSpawner : MonoBehaviour {

    [SerializeField]
    public List<TimedObject> toBeSpawned = new List<TimedObject>();


    public void StartSpawnCicle(){
        Queue<TimedObject> objectsQueue = new Queue<TimedObject>();
        foreach (TimedObject t in toBeSpawned) {
            objectsQueue.Enqueue(t);
        }
        StartCoroutine(SpawnCicle(objectsQueue));

    }

    IEnumerator SpawnCicle(Queue<TimedObject> q) {
        TimedObject obj;
        while (q.Count > 0) {
            obj = q.Dequeue();
            yield return new WaitForSeconds(obj.time);
            Instantiate(obj.objectToSpawn);

        }
        
    }


}
