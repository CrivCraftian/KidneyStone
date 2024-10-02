using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class failStateScreen : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TMP_Text failResult;
    [SerializeField] List<Joystick> joysticks;
    public string failText;

    playerRotation playerRotationRef;
    playerMovement playerMovementRef;
    playerInteract playerInteractRef;

    private void Awake()
    {
        playerRotationRef = player.GetComponent<playerRotation>();
        playerMovementRef = player.GetComponent<playerMovement>();
        playerInteractRef = player.GetComponent<playerInteract>();

        failResult.text = failText;

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
}
