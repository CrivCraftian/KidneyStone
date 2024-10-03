using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodV2 : MonoBehaviour
{
    [SerializeField] AbstractSObject store;

    public AbstractSObject PodContents()
    {
        return store;
    }

    public void FillPod(AbstractSObject store)
    { this.store = store; }

    public void EmptyPod()
    {
        store = null;
    }

    public bool IsEmpty()
    {
        return store == null ? true : false;
    }
}
