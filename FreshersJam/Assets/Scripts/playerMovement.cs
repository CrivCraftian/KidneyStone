using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Enable or disable movement")] [SerializeField] bool movementToggle = true;
    [Tooltip("Set movement speed")] [SerializeField] float movementSpeed = 100f;

    // values
    Rigidbody playerRB;
    Vector2 inputMovement = Vector2.zero;
    Vector3 velocity = Vector3.zero;

    // ensures that playerRB is assigned
    private void Awake() { playerRB = GetComponent<Rigidbody>(); }

    void Update()
    {
        // movement inputs will not be gotten if disabled
        if (movementToggle) { getInputValue(); }
    }

    private void FixedUpdate()
    {
        //convert velocity so that it is in the direction the player is facing
        velocity = transform.TransformDirection(movementSpeed * inputMovement.x * Time.deltaTime, 0, movementSpeed * inputMovement.y * Time.deltaTime);
        
        // sets player velocity
        playerRB.velocity = velocity;
    }

    void getInputValue()
    {
        // get input value of y
        if (Input.GetKeyDown("w")) { inputMovement.y = 1; }
        if (Input.GetKeyDown("s")) { inputMovement.y = -1; }

        // reset input value of x if there is no input
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s")) { inputMovement.y = 0; }

        // get input value of y
        if (Input.GetKeyDown("d")) { inputMovement.x = 1; }
        if (Input.GetKeyDown("a")) { inputMovement.x = -1; }

        // reset input value of y if there is no input
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")) { inputMovement.x = 0; }
    }
}
