using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class signalButton : MonoBehaviour, interactClass 
{
    public GameObject signalOBJ;
    public GameObject player;
    public endStateScreen endScreen;
    public Slider sliderRef;
    [SerializeField] PodController podControllerRef;
    public List<Joystick> joysticks;

    float value = 0;
    bool isHolding = true;
    bool canUseSignal = true;

    int scrapValTotal = -1;

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
        endScreen.gameObject.SetActive(false);
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

            endScreen.endStateText = getSignalEnd();
            endScreen.gameObject.SetActive(true);
        }
    }

    public string getSignalEnd()
    {
        string returnString = "";

        for (int i = 0; i < podControllerRef.podsV2.Count; i++)
        {
            if (!podControllerRef.podsV2[i].IsEmpty())
            {
                switch (podControllerRef.podsV2[i].PodContents().GetSpecialID())
                {
                    // scrap
                    case "scientific":
                        scrapValTotal += podControllerRef.podsV2[i].PodContents().GetValue();
                        break;
                    case "scrap":
                        scrapValTotal += podControllerRef.podsV2[i].PodContents().GetValue();
                        break;
                    case "comp":
                        scrapValTotal += podControllerRef.podsV2[i].PodContents().GetValue();
                        break;
                    case "device":
                        scrapValTotal += podControllerRef.podsV2[i].PodContents().GetValue();
                        break;
                    case "corpse":
                        returnString += "The corpses you retrieved were returned to their families";
                        break;
                    case "storage":
                        scrapValTotal += podControllerRef.podsV2[i].PodContents().GetValue();
                        break;

                    // people
                    case "wound":
                        returnString += string.Format("The wounded man was able to get treatment for his injuries.\n\n");
                        break;
                    case "uncon":
                        returnString += string.Format("The wounded man was able to get treatment for his injuries.\n\n");
                        break;
                    case "multiple":
                        returnString += string.Format("The pod with multiple people turned out to be a family who managed to escape.\n\n");
                        break;
                    case "blocked":
                        returnString += string.Format("The person who left the pod with the blocked signal was never seen again.\n\n");
                        break;
                    case "deadspace":
                        returnString += string.Format("The engineer went insane.\n\n");
                        break;
                    case "camera":
                        returnString += string.Format("The man found inside the pod with the camera footage became a suspect of whatever happened to the ship\n\n");
                        break;
                    case "data":
                        returnString += string.Format("The woman found inside the pod was safe.\nThe data on the pod revealed the company was working on a secret project.\n\n");
                        break;


                }
            }
        }

        if (scrapValTotal > 0) 
        {
            returnString += string.Format("The scrap was sold for a total of {0} units", scrapValTotal);
        }

        else if (scrapValTotal == -1)
        {
            returnString += string.Format("You had nothing of value to sell.", scrapValTotal);
        }

        return returnString;
    }
}

