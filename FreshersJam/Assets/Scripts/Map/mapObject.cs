using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class mapObject : MonoBehaviour, ISender
{
    [SerializeField] ManifestController manifestController;
    [SerializeField] AbstractSObject spaceObject;

    [Header("COMPONENTS")]
    public mapContoller mapContollerRef;
    public ShipController shipControllerRef;

    [Header("SETTINGS")]
    public Vector3 position;
    [Tooltip("Determines how close the player must be on the Z axis to be in range\n\n" +
        "(lower number = player must be closer to z position)\n\n" +
        "(higher number = player doesnt have to be so close to z position)\n\n" +
        "EXAMPLE: if Z is 200 and range is 100, player can be between 100 and 300 and be in range to interact with it")] public float zAxisRange = 100;

[SerializeField] bool showPosition = true;
    [Tooltip("when player is in range")] public Color inRangeColor;
    [Tooltip("when player is in z axis range")] public Color inZRangeColor;
    [Tooltip("when player is outside range")] public Color outRangeColor;

    //values
    BoxCollider areaRef;
    TMP_Text coordsRef;
    bool inZAxisRange = false; // -- use when implementing z axis checking
    bool inRange = false;

    private void OnValidate()
    {
        coordsRef = GetComponentInChildren<TMP_Text>();
        GetComponent<RawImage>().enabled = true;
        coordsRef.text = null;
    }

    private void Start()
    {
        coordsRef = GetComponentInChildren<TMP_Text>();
        GetComponent<RawImage>().enabled = true;

        // assigns the text above the object its position
        // assigns the text above the object its position
        if (spaceObject.GetType() == typeof(Debris))
        {
            if (spaceObject.GetComponent<Debris>().showWarning) { coordsRef.text = "[DEBRIS - AVOID]"; }
            else
            { 
                if (showPosition) { coordsRef.text = string.Format("({0}, {1}, {2})", position.x, position.y, position.z); } 
            }

        }

        else
        {
            if (showPosition) { coordsRef.text = string.Format("({0}, {1}, {2})", position.x, position.y, position.z); }
            else { coordsRef.text = null; }
        }


        // puts the object in its position on the map and sets its color to be out of range
        Vector3 mapPos = mapContollerRef.normPosToMapScreenPos(mapContollerRef.mapPosToNormPos(position));
        transform.localPosition = new Vector3(mapPos.x, mapPos.y, transform.localPosition.z);
        GetComponent<RawImage>().color = outRangeColor;
    }

    private void Awake()
    {
        coordsRef = GetComponentInChildren<TMP_Text>();
        GetComponent<RawImage>().enabled = true;

        // assigns the text above the object its position
        if (spaceObject.GetType() == typeof(Debris)) 
        {
            if (spaceObject.GetComponent<Debris>().showWarning)  { coordsRef.text = "[DEBRIS - AVOID]"; }
            else
            {
                if (showPosition) { coordsRef.text = string.Format("({0}, {1}, {2})", position.x, position.y, position.z); }
            }
        }

        else
        {
            if (showPosition) { coordsRef.text = string.Format("({0}, {1}, {2})", position.x, position.y, position.z); }
            else { coordsRef.text = null; }
        }

        // puts the object in its position on the map and sets its color to be out of range
        Vector3 mapPos = mapContollerRef.normPosToMapScreenPos(mapContollerRef.mapPosToNormPos(position));
        transform.localPosition = new Vector3(mapPos.x, mapPos.y, transform.localPosition.z);
        GetComponent<RawImage>().color = outRangeColor;
    }

    private void Update()
    {
        // checks for z axis range
        if ((shipControllerRef.shipPosition.z <= position.z + zAxisRange && shipControllerRef.shipPosition.z >= position.z - zAxisRange) && !inZAxisRange)
        {
            inZAxisRange = true;
            if (!inRange)
            {
                determineColor(false);
            }
        }

        if (!(shipControllerRef.shipPosition.z <= position.z + zAxisRange && shipControllerRef.shipPosition.z >= position.z - zAxisRange) && inZAxisRange)
        { 
            inZAxisRange = false; 
            determineColor(false); 
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("shipScanPos") && inZAxisRange && !inRange)
        {
            inRange = true;
            onEnter();
        }

        if (collision.CompareTag("shipScanPos") && !inZAxisRange && inRange)
        {
            onExit();
            inRange = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("shipScanPos") && inRange)
        {
            onExit();
            inRange = false;
        }
    }

    private void onEnter()
    {
        if (spaceObject.GetType() == typeof(Debris))
        {
            shipControllerRef.AlterHull(shipControllerRef.HullIntegrity - spaceObject.GetValue());
            manifestController.SendToManifest(this, spaceObject);

            if (shipControllerRef.HullIntegrity < 1)
            {
                spaceObject.GetComponent<Debris>().triggerFailState();
            }

            if (!spaceObject.GetComponent<Debris>().dontDestroy)
            {
                Destroy(this.gameObject);
            }
        }

        else { manifestController.SendToManifest(this, spaceObject); }
        
        determineColor(true);
    }

    private void onExit()
    {
        manifestController.ClearManifest();
        determineColor(false);
    }

    private void determineColor(bool status)
    {
        if (status) { GetComponent<RawImage>().color = inRangeColor; }
        else 
        { 
            if (inZAxisRange)
            {
                GetComponent<RawImage>().color = inZRangeColor;
            }

            else { GetComponent<RawImage>().color = outRangeColor; }          
        }
    }

    public void SpaceObjectProcessed()
    {
        Debug.Log("Object Getting Destroyed");
        Destroy(this.gameObject);
    }
}
