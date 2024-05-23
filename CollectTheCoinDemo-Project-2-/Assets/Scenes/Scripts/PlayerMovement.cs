using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float speed;               // Player movement speed
    public float rotationSpeed;       // Speed at which the player rotates
    public float jumpSpeed;           // Speed/force of the player's jump
    private float ySpeed;             // Vertical speed of the player
    private CharacterController conn; // Reference to the CharacterController component
    public bool isGrounded;           // Boolean to check if the player is on the ground
    public int startingLives = 3; // Initial number of lives
    private int currentLives; // Current number of lives
    public LifeDisplay lifeDisplay; // Reference to the LifeDisplay script

    public int CurrentLives // Public property to access currentLives
    {
        get { return currentLives; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the CharacterController component attached to the player GameObject
        conn = GetComponent<CharacterController>();
        currentLives = startingLives;
        UpdateLifeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from horizontal and vertical axes (typically WASD or arrow keys)
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        // Create a movement direction vector based on input
        Vector3 moveDirection = new Vector3(horizontalMove, 0, verticalMove);
        moveDirection.Normalize(); // Normalize to ensure consistent movement speed
        float magnitude = moveDirection.magnitude; // Get the magnitude of the movement vector
        magnitude = Mathf.Clamp01(magnitude); // Clamp magnitude to 1 to prevent exceeding speed
        conn.SimpleMove(moveDirection * magnitude * speed); // Move the player

        // Apply gravity to the vertical speed
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = -0.5f; // Adjust ySpeed to initiate jump (ground check adjustment)
            isGrounded = false; // Set isGrounded to false because the player is attempting to jump
        }

        // Create a velocity vector including vertical speed for movement
        Vector3 vel = moveDirection * magnitude;
        vel.y = ySpeed;
        conn.Move(vel * Time.deltaTime); // Apply movement to the player

        // Check if the player is grounded
        if (conn.isGrounded)
        {
            ySpeed = 0.5f; // Reset vertical speed when grounded
            isGrounded = true; // Set isGrounded to true

            // Check for jump input when grounded
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed; // Apply jump speed to vertical speed
                isGrounded = false; // Set isGrounded to false as the player is now jumping
            }
        }

        // Rotate the player to face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up); // Determine rotation direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, rotationSpeed * Time.deltaTime); // Smooth rotation
        }
    }

    public void LoseLife()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            int currentScore = CoinCounter.instance.GetCurrentScore();
            HighscoreManager.instance.AddNewScore(currentScore); // Add current score
            Debug.Log("Game Over");
            // Load end scene
            SceneManager.LoadScene("EndScene");
        }
        UpdateLifeDisplay();
    }

    public void HandleDamage()
    {
        LoseLife();
    }

    private void UpdateLifeDisplay()
    {
        if (lifeDisplay != null)
        {
            lifeDisplay.OnLifeCountChanged();
        }
    }
}
