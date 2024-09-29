using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [Header("SETTINGS")]
    [Tooltip("Enable or disable interaction")] public bool interactionToggle = true;
    [Tooltip("Enable or disable raycast debug")][SerializeField] bool debugRayToggle = false;
    [Tooltip("Sets the length of the raycast")] [SerializeField] float raycastRange = 2.5f;

    // values
    Camera playerCamera;
    Ray ray;
    RaycastHit hit;
    bool rayDetected, mouseClickActive = false;

    // ensures that playerCamera is assigned
    private void Awake() { playerCamera = GetComponentInChildren<Camera>(); }

    void Update()
    {
        // gets mouse input
        getMouseClick();

        // if debug is on, draw a ray to represent the raycast (i dont know why i needed to multiply the range by 1.1f to be accurate)
        if (debugRayToggle) { Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * (raycastRange * 1.1f), Color.red); }

        // create a ray that converts the position of the mouse into a ray in world space 
        if (getMouseInputState())
        {
            ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            rayDetected = Physics.Raycast(ray, out hit, raycastRange);
        }

        // checks if ray has hit a object and it has a component using interactClass
        if (rayDetected && hit.transform.GetComponent<interactClass>() != null)
        {
            // single button check
            if (getMouseInputState()) { hit.transform.GetComponent<interactClass>().useClick(); }

            // hold button check
            if (mouseClickActive) { hit.transform.GetComponent<interactClass>().useHold(); }
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
