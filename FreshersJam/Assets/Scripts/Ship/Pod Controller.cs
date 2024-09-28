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

        pods[2].FillPod(new Scrap(4));
        pods[4].FillPod(new Person("Bob", "He digs"));
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
