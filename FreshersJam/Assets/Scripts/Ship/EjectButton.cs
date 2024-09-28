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

    public void useHold()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
