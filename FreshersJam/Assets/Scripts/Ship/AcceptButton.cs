using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptButton : MonoBehaviour, interactClass
{
    [SerializeField] ManifestController controller;

    public void useClick()
    {
        controller.AcceptSpaceObject();
    }

    public void useHold()
    {
        
    }
}
