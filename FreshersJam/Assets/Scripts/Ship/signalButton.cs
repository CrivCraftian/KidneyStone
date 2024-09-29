using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class signalButton : MonoBehaviour, interactClass 
{
    public GameObject signalOBJ;
    public GameObject player;
    public GameObject endScreen;
    public Slider sliderRef;
    public List<Joystick> joysticks;

    float value = 0;
    bool isHolding = true;
    bool canUseSignal = true;

    playerRotation playerRotationRef;
    playerMovement playerMovementRef;
    playerInteract playerInteractRef;

    public void useClick() 
    {
        if (canUseSignal) { signalOBJ.SetActive(true);  }
    }
    public void useHold() 
    { 
        if (canUseSignal) { isHolding = true;  }
    }

    private void Awake()
    {
        signalOBJ.SetActive(false);
        endScreen.SetActive(false);
        playerRotationRef = player.GetComponent<playerRotation>();
        playerMovementRef = player.GetComponent<playerMovement>();
        playerInteractRef = player.GetComponent<playerInteract>();
    }

    private void Update()
    {
        if (canUseSignal)
        {
            if (isHolding && playerInteractRef.mouseClickActive) { value += 0.0015f; }

            if (isHolding && !playerInteractRef.mouseClickActive)
            {
                value -= 0.0025f;
                if (value <= 0) 
                { 
                    isHolding = false;
                    signalOBJ.SetActive(false);
                }
            }

            value = Mathf.Clamp(value, 0f, 1f);
        }

        sliderRef.value = value;

        if (value >= 1)
        {
            signalOBJ.SetActive(false);            
            canUseSignal = false;
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

            endScreen.SetActive(true);
        }
    }
}

