// SwitchController.cs
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private bool isActivated = false;

    // Activate the switch
    public void ActivateSwitch()
    {
        if (!isActivated)
        {
            isActivated = true;

            // When the switch is activated
            // For example, open the gate
            GateController gateController = transform.parent.GetComponentInChildren<GateController>();
            gateController.OpenGate();
        }
    }

    // Check if the switch is activated
    public bool IsActivated()
    {
        return isActivated;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player")){
            
            ActivateSwitch();
        }
    }
}
