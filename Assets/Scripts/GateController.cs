// GateController.cs
using UnityEngine;

public class GateController : MonoBehaviour
{
    // Open the gate
    public void OpenGate()
    {
        // Implement gate opening animation, translation, or any other desired behavior
        // For example, disable the collider to allow the marble to pass through
        
        Debug.Log("Opening gate!");
        
        GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }
}
