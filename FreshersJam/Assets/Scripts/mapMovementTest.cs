using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapMovementTest : MonoBehaviour, interactClass
{
    public enum directionEnum { Up, Down, Left, Right }

    public directionEnum direction;
    public float moveValue;
    public ScrollRect mapRect;
    

    private void Awake()
    {
        mapRect.horizontalNormalizedPosition = 1f;
        mapRect.verticalNormalizedPosition = 1f;
    }

    public void useClick() { }
    public void useHold()
    {
        switch(direction)
        {
            case directionEnum.Up:
                mapRect.verticalNormalizedPosition -= moveValue;
                break;
            case directionEnum.Down:
                mapRect.verticalNormalizedPosition += moveValue;
                break;
            case directionEnum.Left:
                mapRect.horizontalNormalizedPosition -= moveValue;
                break;
            case directionEnum.Right:
                mapRect.horizontalNormalizedPosition += moveValue;
                break;
        }
    }
}
