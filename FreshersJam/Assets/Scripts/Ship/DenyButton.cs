using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenyButton : MonoBehaviour, interactClass
{
    [SerializeField] ManifestController manifestController;

    public void useClick()
    {
        manifestController.DenySpaceObject();
    }

    public void useHold()
    {

    }
}
