using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectButton : MonoBehaviour, interactClass
{
    [SerializeField] PodController controller;
    [SerializeField] int podNum;

    public void useClick()
    {
        controller.EjectPod(podNum);
    }

    public void useHold() { }

}
