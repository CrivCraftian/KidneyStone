using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : AbstractSObject 
{
    public endStateScreen failState;
    public bool dontDestroy = false;
    public void triggerFailState()
    {
        failState.endStateText = "The hull could not withstand any more damage.";
        failState.gameObject.SetActive(true);
    }

}
