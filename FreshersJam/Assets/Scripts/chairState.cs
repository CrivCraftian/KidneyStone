using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chairState : MonoBehaviour, interactClass
{
    [Header("PLAYER POSITION SETTINGS")]
    [Tooltip("Used as position when player sits down")] [SerializeField] Transform sitDownTransform;
    [Tooltip("Used as position when player stands up")] [SerializeField] Transform standUpTransform;

    [Header("CAMERA SETTINGS")]
    [Tooltip("Default angle player faces when sitting down and standing up")] [SerializeField] Vector2 defaultCameraAngle;
    [Tooltip("Can be used to limit player camera while sitting (SET TO 0 TO BE IGNORED)")] [SerializeField] Vector2Int changeCameraClamp;
    
    // values
    bool sitState = false;
    GameObject player;
    playerMovement playerMoveRef;
    playerRotation playerRotationRef;

    public void useHold() { }

    private void Awake()
    {
        // gets and assigns values for player and components
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoveRef = player.GetComponent<playerMovement>();
        playerRotationRef = player.GetComponent<playerRotation>();
    }

    public void useClick()
    {
        switch (sitState)
        {
            // STANDING UP
            case true:
                // disables state and enables collision with object
                sitState = false;
                GetComponent<BoxCollider>().isTrigger = false;

                // enables movement and moves player to position for standing up
                playerMoveRef.toggleMovement(true);
                player.transform.position = standUpTransform.position;

                // reverts camera changes
                playerRotationRef.resetHorClamp = true;
                playerRotationRef.revertClamp(); 
                playerRotationRef.forceCamera(defaultCameraAngle);
                break;

            // SITTING DOWN
            case false:
                // enables state and disables collision with object
                sitState = true;
                GetComponent<BoxCollider>().isTrigger = true;
                
                // disables movement and moves player to position for sitting down
                playerMoveRef.toggleMovement(false);
                player.transform.position = sitDownTransform.position;

                // can be used to limit camera to focus on specific area
                playerRotationRef.resetHorClamp = false;
                playerRotationRef.changeClamp(changeCameraClamp.x, changeCameraClamp.y);
                playerRotationRef.forceCamera(defaultCameraAngle);
                break;
        }
    }
}
