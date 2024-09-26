using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("SETTINGS")]
    [Tooltip("Enable or disable movement")] public bool movementToggle = true;
    [Tooltip("Set movement speed")] public float movementSpeed = 100f;

    // values
    [HideInInspector] public Rigidbody playerRB;
    [HideInInspector] public Vector2 inputMovement = Vector2.zero;
    [HideInInspector] public Vector3 velocity = Vector3.zero;

    // ensures that playerRB is assigned
    private void Awake() { playerRB = GetComponent<Rigidbody>(); }

    void Update()
    {
        // movement inputs will not be gotten if disabled
        if (movementToggle) { getKeyboardInput(); }
    }

    private void FixedUpdate()
    {
        //convert velocity so that it is in the direction the player is facing
        velocity = transform.TransformDirection(movementSpeed * inputMovement.x * Time.deltaTime, 0, movementSpeed * inputMovement.y * Time.deltaTime);
        
        // sets player velocity
        playerRB.velocity = velocity;
    }

    void getKeyboardInput()
    {
        // get input value of y
        if (Input.GetKeyDown(KeyCode.W)) { inputMovement.y = 1; }
        if (Input.GetKeyDown(KeyCode.S)) { inputMovement.y = -1; }

        // reset input value of x if there is no input
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) { inputMovement.y = 0; }

        // get input value of y
        if (Input.GetKeyDown(KeyCode.D)) { inputMovement.x = 1; }
        if (Input.GetKeyDown(KeyCode.A)) { inputMovement.x = -1; }

        // reset input value of y if there is no input
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) { inputMovement.x = 0; }
    }

    public void toggleMovement(bool newStatus)
    {
        // toggles movement and resets values relating to movement
        movementToggle = newStatus;
        playerRB.velocity = Vector3.zero;
        velocity = Vector3.zero;
        inputMovement = Vector3.zero;
    }
}
