using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Enable or disable camera movement")] [SerializeField] bool cameraToggle = true;

    [Header("Camera Sensitivity")]
    [Tooltip("Sensitivity for x axis")] [SerializeField] float xSens = 10f;
    [Tooltip("Sensitivity for y axis")] [SerializeField] float ySens = 10f;

    [Header("Camera Clamp")]
    [Tooltip("Clamp for horizontal input")] [SerializeField] int horClamp = 361;
    [Tooltip("Clamp for vertical input")] [SerializeField] int verClamp = 81;

    // values
    [HideInInspector] public Camera playerCam;
    Vector2 inputMouse = Vector2.zero;
    float verticalRotation, horizontalRotation = 0f;

    // ensures that playerCam is assigned
    private void Awake() { playerCam = GetComponentInChildren<Camera>(); }

    private void Start()
    {
        // hides and locks mouse in place
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // mouse inputs will not be gotten if disabled
        if (cameraToggle) { getMouseInput(); }
        
        // assigns rotation of vertical and horizontal rotation
        transform.eulerAngles = new Vector3(0, horizontalRotation, 0);
        playerCam.transform.eulerAngles = new Vector3(verticalRotation, horizontalRotation, 0);
    }

    void getMouseInput()
    {
        // get input
        inputMouse = Input.mousePositionDelta;

        // add input to current rotation values
        horizontalRotation += inputMouse.x * xSens * Time.deltaTime;
        verticalRotation += -inputMouse.y * ySens * Time.deltaTime;

        // lock vertical and horizontal rotation to specific values
        horizontalRotation = Mathf.Clamp(horizontalRotation, -horClamp, horClamp);
        verticalRotation = Mathf.Clamp(verticalRotation, -verClamp, verClamp);

        // sets horizontal rotation to 0 if camera has made full horizontal rotation
        if (horizontalRotation == horClamp || horizontalRotation == -horClamp) { horizontalRotation = 0f; }
    }
}
