using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class RandomTimeSpawner : MonoBehaviour
    {
        //reference code from https://answers.unity.com/questions/898380/spawning-an-object-at-a-random-time-c.html
        //Spawn this object
        public GameObject prefab;
        private GameObject currentObject;
    
        private Vector3 SpawnTransform;
    
        public float maxTime = 120;
        public float minTime = 60;
    
        //current time
        private float time;
    
        //The time to spawn the object
        private float spawnTime;
    
        void Start()
        {
            SetRandomTime();
            time = 0;
        }
    
        void FixedUpdate()
        {
            //Counts up
            time += Time.deltaTime;
            //Check if its the right time to spawn the object
            if (time >= spawnTime)
            {
                SpawnObject();
                SetRandomTime();
            }
        }
    
        //Spawns the object and resets the time
        void SpawnObject()
        {
            time = 0;
            SpawnTransform.x = transform.position.x + Random.Range(-5, 5);
            SpawnTransform.y = 0;
            SpawnTransform.z = transform.position.z + Random.Range(-5, 5);
            currentObject = Instantiate(prefab, SpawnTransform, prefab.transform.rotation);
            SoundManager.PlaySound(SoundManager.Sound.hello, currentObject.gameObject.transform.position);
            StartCoroutine(SelfDestruct(currentObject));
        }
    
        //Sets the random time between minTime and maxTime
        void SetRandomTime()
        {
            spawnTime = Random.Range(minTime, maxTime);
        }
    
        IEnumerator SelfDestruct(GameObject obj)
        {
            yield return new WaitForSeconds(7f);
            if (obj != null)
            {
                SoundManager.PlaySound(SoundManager.Sound.evilLaugh, obj.transform.position);
                BatteryBar.changeBatteries(-1);
                SoundManager.PlaySound(SoundManager.Sound.decreaseBattery);
                Destroy(obj);
            }
        }
    }

}
