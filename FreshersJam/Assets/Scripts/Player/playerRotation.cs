using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    [Header("SETTINGS")]
    [Tooltip("Enable or disable camera movement")] public bool cameraToggle = true;
    [Tooltip("Enable or disable resetting horizontal clamp at max or min value")] public bool resetHorClamp = true;

    [Header("CAMERA SENSITIVITY SETTINGS")]
    [Tooltip("Sensitivity for x axis")] public float xSens = 10f;
    [Tooltip("Sensitivity for y axis")] public float ySens = 10f;

    [Header("CAMERA CLAMP SETTINGS")]
    [Tooltip("Clamp for horizontal input")] public int horClamp = 361;
    [Tooltip("Clamp for vertical input")] public int verClamp = 81;

    int defaultHorClamp, defaultVerClamp;

    // values
    [HideInInspector] public Camera playerCam;
    Vector2 inputMouse, rotationValues = Vector2.zero;

    // ensures that playerCam is assigned
    private void Awake() { playerCam = GetComponentInChildren<Camera>(); }

    private void Start()
    {
        // assigns default clamp values on start
        defaultHorClamp = horClamp;
        defaultVerClamp = verClamp;

        // hides and locks mouse in place
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // mouse inputs will not be gotten if disabled
        if (cameraToggle) { getMouseInput(); }
        
        // assigns rotation of vertical and horizontal rotation
        transform.eulerAngles = new Vector3(0, rotationValues.x, 0);
        playerCam.transform.eulerAngles = new Vector3(rotationValues.y, rotationValues.x, 0);
    }

    void getMouseInput()
    {
        // get input
        inputMouse = Input.mousePositionDelta;

        // add input to current rotation values
        rotationValues.x += inputMouse.x * xSens * Time.deltaTime;
        rotationValues.y += -inputMouse.y * ySens * Time.deltaTime;

        // lock vertical and horizontal rotation to specific values
        rotationValues.x = Mathf.Clamp(rotationValues.x, -horClamp, horClamp);
        rotationValues.y = Mathf.Clamp(rotationValues.y, -verClamp, verClamp);

        // sets horizontal rotation to 0 if camera has made full horizontal rotation
        if ((rotationValues.x == horClamp || rotationValues.x == -horClamp) && resetHorClamp) { rotationValues.x = 0f; }
    }

    public void forceCamera(Vector2 newAngle)
    {
        // forces the camera to look at specified vector2 values
        rotationValues.x = newAngle.x;
        rotationValues.y = newAngle.y;

    }

    public void changeClamp(int newHorClamp, int newVerClamp)
    {
        // can be used to limit player camera rotation (doesn't change if new value is 0)
        horClamp = (newHorClamp == 0) ? defaultHorClamp : newHorClamp;
        verClamp = (newHorClamp == 0) ? defaultVerClamp : newVerClamp;
    }

    public void revertClamp()
    {
        // can be used to reset clamp values without using changeClamp
        horClamp = defaultHorClamp;
        verClamp = defaultVerClamp;
    }
}
