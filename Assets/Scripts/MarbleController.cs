using System;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
    // Variables for movement and camera control
    private Vector3 move;           // Desired move direction
    private Transform cam;          // Reference to the main camera
    private Vector3 camForward;     // Current forward direction of the camera
    private bool jump;              // Flag indicating if the jump button is pressed

    // Movement parameters
    [SerializeField] private float movePower = 5;           // Force added to the ball for movement
    [SerializeField] private bool useTorque = true;         // Whether to use torque for ball rotation
    [SerializeField] private float maxAngularVelocity = 25; // Maximum angular velocity for the ball's rotation
    [SerializeField] private float jumpPower = 2; 
    [SerializeField] private GameObject crown;           // Force added to the ball when jumping

    // Ground detection parameters
    private const float groundRayLength = 1f; // Length of the ray to check if the ball is grounded

    public Rigidbody rigidbody;

    private void Awake()
    {
        // Get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
    }

    private void Start()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        // Set the maximum angular velocity for the ball
        rigidbody.maxAngularVelocity = maxAngularVelocity;

        GetComponent<MeshRenderer>().material.color = Color.white;

        crown.SetActive(true);
    }

    private void Update()
    {
        // Get input for movement and jumping
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");

        // Calculate move direction based on camera orientation
        if (cam != null)
        {
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = (verticalInput * camForward + horizontalInput * cam.right).normalized;
        }
        else
        {
            // Use world-relative directions if no main camera is present
            move = (verticalInput * Vector3.forward + horizontalInput * Vector3.right).normalized;
        }

        if(transform.position.y < 3f){

            Debug.Log("Game Over: You fell to your death");

            if(!GameManager.Instance.gameHasEnded){

                GameManager.Instance.gameHasEnded = true;
                GameManager.Instance.GameOverRestart();
            }
            
        }
    }

    private void FixedUpdate()
    {
        // Move the ball based on the calculated move direction and jump input
        Move(move, jump);
        jump = false; // Reset jump input
    }

    public void Move(Vector3 moveDirection, bool jump)
    {
        // Apply torque for rotation or force for movement based on configuration
        if (useTorque)
        {
            rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * movePower);
        }
        else
        {
            rigidbody.AddForce(moveDirection * movePower);
        }

        // If on the ground and jump is pressed, add upward force
        if (Physics.Raycast(transform.position, -Vector3.up, groundRayLength) && jump)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    // Handle collisions with obstacles
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Obstacle") || other.collider.CompareTag("Enemy"))
        {
            Debug.Log("Game Over: You hit an obstacle");

            GetComponent<MeshRenderer>().material.color = Color.black;
            crown.SetActive(false);
            GameManager.Instance.GameOverRestart();
        }
    }

    // Handle triggers for collecting coins
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Jackpot! You collected a coin");

            GameObject coin = other.gameObject;

            DisableCoin(coin);
            GameManager.Instance.AddPoint();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EndGame"))
        {
            Debug.Log("Congratulations! You completed the maze");
            GameManager.Instance.GameCompleted();
        }
    }
    
    // Disable the collected coin
    private void DisableCoin(GameObject coin)
    {
        coin.SetActive(false);
    }
}