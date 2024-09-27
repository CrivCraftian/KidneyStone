using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class mapMovementController : MonoBehaviour, interactClass
{
    public enum directionEnum { Up, Down, Left, Right, Forward, Backward }

    public mapContoller mapContollerRef;
    public directionEnum direction;
    public float moveValue;
    public ScrollRect mapRect;

    public void useClick() { }
    public void useHold()
    {
        switch(direction)
        {
            case directionEnum.Up:
                mapContollerRef.normalisedPosition.y -= moveValue;
                break;
            case directionEnum.Down:
                mapContollerRef.normalisedPosition.y += moveValue;
                break;
            case directionEnum.Left:
                mapContollerRef.normalisedPosition.x += moveValue; 
                break;
            case directionEnum.Right:
                mapContollerRef.normalisedPosition.x -= moveValue;         
                break;
            case directionEnum.Forward:
                mapContollerRef.normalisedPosition.z += moveValue;
                break;
            case directionEnum.Backward:
                mapContollerRef.normalisedPosition.z -= moveValue;
                break;
        }
    }
}
