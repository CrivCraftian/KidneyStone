using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Enable or disable interaction")] [SerializeField] bool interactionToggle = true;
    [Tooltip("Enable or disable raycast debug")][SerializeField] bool debugRayToggle = false;
    [Tooltip("Sets the length of the raycast")] [SerializeField] float raycastRange = 2.5f;

    // values
    Camera playerCamera;
    RaycastHit hit;
    bool mouseClickActive = false;

    // ensures that playerCamera is assigned
    private void Awake() { playerCamera = GetComponentInChildren<Camera>(); }

    void Update()
    {
        // gets mouse input
        getMouseClick();
        
        // if debug is on, draw a ray to represent the raycast (i dont know why i needed to multiply the range by 1.1f to be accurate)
        if (debugRayToggle) { Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * (raycastRange * 1.1f), Color.red); }

        // if mouseClickActive is currently true (aka pressed/held down) 

        // IF YOU ARE LOOKING AT THIS BRANCH (hi by the way), YOU CAN TEST THE DIFFERENCE BETWEEN
        // mouseClickActive AND getMouseInputState() BY SWAPPING IT IN THIS IF STATEMENT
        if (getMouseInputState())
        {
            // create a ray that converts the position of the mouse into a ray in world space 
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            // if the raycast collides with something inside a limited range, get the object
            if (Physics.Raycast(ray, out hit, raycastRange))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit.name);      
            }
        }
    }

    // use mouseClickActive if you want something to be triggered over time (holding down the mouse button)
    void getMouseClick()
    {
        if (interactionToggle)
        {
            if (Input.GetMouseButtonDown(0)) { mouseClickActive = true; }
            if (Input.GetMouseButtonUp(0)) { mouseClickActive = false; }
        }

        else { mouseClickActive = false; }
    }

    // use getMouseInput if you want something to be triggered ONCE
    bool getMouseInputState()
    {
        if (interactionToggle)
        {
            if (Input.GetMouseButtonDown(0)) { return true; } 
            if (Input.GetMouseButtonUp(0)) { return false; } 
            else { return false; }
        }

        else { return false; }
    }
}
