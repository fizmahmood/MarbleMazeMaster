using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMarbleController : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    public Transform target;  // Reference to the player
    
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        target = GameManager.Instance.playerController.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Set destination for navigation
        enemyAgent.SetDestination(target.position);

    }
}
