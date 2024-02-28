using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Reference to the enemy prefab to be spawned
    public Transform spawnPoint;    // The position where enemies will be spawned
    bool spawnedOnce;

    void Start()
    {
        spawnedOnce = false;
    }

    void OnTriggerEnter(Collider other) //Spawn new ememies when a player passes through
    {   
        if(other.CompareTag("Player") && !spawnedOnce){

            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedOnce = true;
        }
        
    }

}
