using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int delayTime = 0;
    private int activeWave = 0;
    public float randomFactor = 0;
    public ArrowShooter manager;
    public Text cooldownText;
    private bool ended = false;
    private AudioSource aSource;

    public void StartSpawnCicle(){
        aSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnCicle());

    }

    IEnumerator SpawnCicle() {
        Cursor.visible = false;
        activeWave = 0;
        Queue<TimedObject> q = new Queue<TimedObject>();
        int waveDelayTime;
        while (activeWave < waves.Count) {
            if (activeWave == 0)
            {
                cooldownText.text = ("Certo se prepare que eu vou atrair eles pra esse lado !");
            }
            else {
                cooldownText.text = ("Se prepare que tem mais vindo !");
            }
            
            yield return new WaitForSeconds(2f);
            Debug.Log("Spawning wave: " + (activeWave + 1) + " in " + delayTime.ToString() + " seconds.");
            
            waveDelayTime = delayTime;
            q.Clear();
            foreach (TimedObject t in waves[activeWave].toBeSpawned)
            {
                q.Enqueue(t);
            }
            TimedObject obj = new TimedObject();
           
            while (waveDelayTime > 0) {
                cooldownText.text = (waveDelayTime.ToString());
                yield return new WaitForSeconds(1);
                waveDelayTime--;

            }
            cooldownText.text = ("");
            yield return new WaitForSeconds(2);


            // yield return new WaitForSeconds(delayTime);

            while (q.Count > 0)
            {
                obj = q.Dequeue();
                yield return new WaitForSeconds(obj.time+Random.Range(-randomFactor,randomFactor));

                Vector3 t = obj.spawnPositon.position;
                Quaternion r = this.transform.rotation;
                while (manager.Idle)
                {
                    yield return new WaitForEndOfFrame();
                }
                aSource.pitch = Random.Range(0.9f, 1.1f);
                aSource.Play();
                yield return new WaitForSecondsRealtime(0.02f);
                Instantiate(obj.objectToSpawn, t, r);

            }
            activeWave++;
            yield return new WaitForSeconds(delayTime+3);
        }
        
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
