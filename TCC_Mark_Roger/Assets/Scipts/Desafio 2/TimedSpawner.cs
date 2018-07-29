using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimedObject
{
    public float time;
    public GameObject objectToSpawn;
    public Transform spawnPositon;

    public TimedObject(float t,GameObject x,Transform p)
    {
        time = t;
        objectToSpawn = x;
        spawnPositon = p;
    }


}

[System.Serializable]
public class SpawnWave{

    [SerializeField]
    public List<TimedObject> toBeSpawned = new List<TimedObject>();


}


public class TimedSpawner : MonoBehaviour {

    [SerializeField]
    public List<SpawnWave> waves = new List<SpawnWave>();
    public float delayTime = 0;
    private int activeWave = 0;
    public float randomFactor = 0;
    public ArrowShooter manager;
    private bool ended = false;

    public void StartSpawnCicle(){
        
        StartCoroutine(SpawnCicle());

    }

    IEnumerator SpawnCicle() {
        
        activeWave = 0;
        Queue<TimedObject> q = new Queue<TimedObject>();
        while (activeWave < waves.Count) {
            q.Clear();
            foreach (TimedObject t in waves[activeWave].toBeSpawned)
            {
                q.Enqueue(t);
            }
            TimedObject obj = new TimedObject();

            Debug.Log("Spawning wave: " + (activeWave + 1) + " in " + delayTime.ToString() + " seconds.");
            yield return new WaitForSeconds(delayTime);
            
            while (q.Count > 0)
            {
                obj = q.Dequeue();
                yield return new WaitForSeconds(obj.time+Random.Range(-randomFactor,randomFactor));
                Vector3 t = obj.spawnPositon.position;
                Quaternion r = this.transform.rotation;
                Instantiate(obj.objectToSpawn, t, r);
            }
            activeWave++;
        }
        yield return new WaitForSeconds(delayTime);
        ended = true;
    }

    private void Update()
    {
        if (ended) {
            manager.EndChallenge();
            ended = false;

        }
    }

}
