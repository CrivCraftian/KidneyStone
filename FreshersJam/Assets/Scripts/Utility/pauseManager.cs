using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    public GameObject pauseMain;
    public GameObject player;
    public List<Joystick> joysticks;

    playerRotation playerRotationRef;
    playerMovement playerMovementRef;
    playerInteract playerInteractRef;

    public bool pauseToggle = true;
    bool currentStatus = false;
    string check;

    private void Awake()
    {
        playerRotationRef = player.GetComponent<playerRotation>();
        playerMovementRef = player.GetComponent<playerMovement>();
        playerInteractRef = player.GetComponent<playerInteract>();

        changeStatus(false);
    }

    private void Update()
    {
        check = getPauseInputState();
        if (check == "true" || check == "false")
        {
            if (check == "true") { currentStatus = true; }
            else if (check == "false") { currentStatus = false; }
            changeStatus(currentStatus);
        }
    }

    public string getPauseInputState()
    {
        if (pauseToggle)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                if (!currentStatus) { return "true"; }
                else { return "false"; }
            }
            else { return "ignore"; }
        }

        else { return "ignore"; }
    }

    public void changeStatus(bool newStatus) 
    {
        currentStatus = newStatus;
        if (newStatus)
        {
            playerRotationRef.cameraToggle = false;
            playerMovementRef.movementToggle = false;
            playerInteractRef.interactionToggle = false;

            Time.timeScale = 0;

            foreach (Joystick joystickRef in joysticks)
            {
                joystickRef.paused = true;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            playerRotationRef.cameraToggle = true;
            playerMovementRef.movementToggle = true;
            playerInteractRef.interactionToggle = true;

            Time.timeScale = 1;

            foreach (Joystick joystickRef in joysticks)
            {
                joystickRef.paused = false;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        pauseMain.SetActive(newStatus); 
    }
}
