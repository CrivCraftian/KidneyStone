using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodController : MonoBehaviour
{
    [SerializeField] DisplayController displayController;

    List<Pod> pods = new List<Pod>(6);

    public PodController() 
    {
        for (int i = 0; i < 6; i++)
        {
            pods.Add(new Pod());
        }
    }

    public bool FillPod(AbstractSObject spaceObject)
    {
        for (int i = 0; i < pods.Count; i++)
        {
            if (pods[i].IsEmpty())
            {
                pods[i].FillPod(spaceObject);
                displayController.UpdatePodIcons();
                return true;
            }
        }

        return false;
    }

    public Pod GetPod(int podnum)
    {
        return pods[podnum];
    }

    public void EjectPod(int podnum)
    {
        pods[podnum].EmptyPod();
        displayController.UpdatePodIcons();
    }

    // pod 1 left, pod 2 right



}
