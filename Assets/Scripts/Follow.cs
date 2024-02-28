using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;  // Reference to the marble or its holder
    public Vector3 offset;    // Adjust this to set the camera distance from the marble

    void Update()
    {

        transform.position = target.position + offset;
        //transform.LookAt(target);
    }
}
