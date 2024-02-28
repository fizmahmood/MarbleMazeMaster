using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.gameHasEnded)
        {

            GameManager.Instance.gameHasEnded = true;
            Debug.Log("Congratulations! You completed the maze");
            GameManager.Instance.CheckIfGameIsCompleted();
            
        }
    }
}
