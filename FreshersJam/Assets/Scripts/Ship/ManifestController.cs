using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManifestController : MonoBehaviour
{
    [SerializeField] PodController podController;
    [SerializeField] ShipController shipController;

    [SerializeField] TextMeshProUGUI MName;
    [SerializeField] TextMeshProUGUI MDesc;
    [SerializeField] TextMeshProUGUI MValue;

    AbstractSObject storedObject;

    ISender lastMessager;

    public void SendToManifest(ISender origin, AbstractSObject SpaceObject)
    {
        storedObject = SpaceObject;
        lastMessager = origin;
        MName.text = SpaceObject.GetName();
        MDesc.text = SpaceObject.GetDescription();
        if (SpaceObject.GetAltCheck()) { MValue.text = "" + SpaceObject.GetAltValue(); }
        else { MValue.text = "" + SpaceObject.GetValue(); }
        
    }

    public void ClearManifest()
    {
        storedObject = null;
        lastMessager = null;
        MName.text = "";
        MDesc.text = "";
        MValue.text = "";
    }

    public void AcceptSpaceObject()
    {
        if(storedObject == null || storedObject.GetType() == typeof(Debris))
        {
            return;
        }

        if (storedObject.GetType() == typeof(Fuel)) 
        {
            shipController.AlterFuel(shipController.FuelCount + storedObject.GetValue());
            lastMessager.SpaceObjectProcessed();
            ClearManifest();
            return;
        }

        else
        {
            if (podController.FillPod(storedObject))
            {
                lastMessager.SpaceObjectProcessed();
                ClearManifest();
                return;
            }
        }

    }

    public void DenySpaceObject()
    {
        if (storedObject == null)
        {
            lastMessager.SpaceObjectProcessed();
            return;
        }

        ClearManifest();
    }
}
