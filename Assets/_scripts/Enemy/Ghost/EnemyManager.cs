using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class EnemyManager : MonoBehaviour
    {
        // reference code from https://www.youtube.com/watch?v=NWNH9XRtuIc
        public Transform[] m_SpawnPoints;
        public GameObject m_EnemyPrefab;

        // Start is called before the first frame update
        void Start()
        {
            SpawnNewEnemy();
        }

        void OnEnable()
        {   
            // When an ghost is killed, triggered the SpawnNewEnemy function to spawn a new ghost
            Ghost.OnEnemyKilled += SpawnNewEnemy;
        }

        void SpawnNewEnemy()
        {
            // get a random number from the range of 0 to the maximun number of spawnpoints
            int randomNumber = Mathf.RoundToInt(Random.Range(0f, m_SpawnPoints.Length - 1));
            // instantiate an enemy in a random spawnpoint
            Instantiate(m_EnemyPrefab, m_SpawnPoints[randomNumber].transform.position, Quaternion.identity);
        }
    }

}
