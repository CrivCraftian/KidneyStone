using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodController
{
    List<Pod> pods = new List<Pod>(6);

    public PodController() 
    {
        for (int i = 0; i < 6; i++)
        {
            pods.Add(new Pod());
        }
    }

    // pod 1 left, pod 2 right



}
