using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour, interactClass
{
    [SerializeField] private List<Joystick> joysticks = new List<Joystick>();

    public void useClick()
    {
        foreach (var joystick in joysticks) {
            joystick.ResetPosition();    
        }
    }

    public void useHold()
    {
        
    }

}
