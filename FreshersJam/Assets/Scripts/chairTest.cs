using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chairTest : MonoBehaviour, interactClass
{
    [SerializeField] Transform sitDownTransform;
    [SerializeField] Transform sitUpTransform;
    [SerializeField] Vector2 defaultCameraAngle;
    bool sitState = false;

    GameObject player;
    playerMovement playerMoveRef;
    playerRotation playerRotationRef;
    playerInteract playerInteractRef;

    public void useHold() { }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerMoveRef = player.GetComponent<playerMovement>();
        playerRotationRef = player.GetComponent<playerRotation>();
        playerInteractRef = player.GetComponent<playerInteract>();
    }

    public void useClick()
    {
        switch (sitState)
        {
            case true:
                sitState = false;

                playerMoveRef.movementToggle = true;
                playerMoveRef.playerRB.velocity = Vector3.zero;
                playerMoveRef.velocity = Vector3.zero;
                playerMoveRef.inputMovement = Vector3.zero;

                player.transform.position = sitUpTransform.position;
                GetComponent<BoxCollider>().isTrigger = false;
                break;
            case false:
                GetComponent<BoxCollider>().isTrigger = true;
                sitState = true;
                playerMoveRef.movementToggle = false;
                playerMoveRef.playerRB.velocity = Vector3.zero;
                playerMoveRef.velocity = Vector3.zero;
                playerMoveRef.inputMovement = Vector3.zero;

                player.transform.position = sitDownTransform.position;

                transform.eulerAngles = new Vector3(0, defaultCameraAngle.y, 0);
                playerRotationRef.playerCam.transform.eulerAngles = new Vector3(defaultCameraAngle.x, defaultCameraAngle.y, 0);
                break;
        }
    }


    private void Update()
    {
        
    }

}
