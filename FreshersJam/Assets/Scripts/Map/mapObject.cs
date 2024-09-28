using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mapObject : MonoBehaviour
{
    [Header("COMPONENTS")]
    public mapContoller mapContollerRef;
    

    [Header("SETTINGS")]
    public Vector3 position;
    [Tooltip("Determines how close the player must be on the Z axis to be in range\n\n" +
        "(lower number = player must be closer to z position)\n\n" +
        "(higher number = player doesnt have to be so close to z position)\n\n" +
        "EXAMPLE: if Z is 200 and range is 100, player can be between 100 and 300 and be in range to interact with it")] public float zAxisRange = 100;

    [Tooltip("when player is in range")] public Color inRangeColor;
    [Tooltip("when player is outside range")] public Color outRangeColor;

    //values
    BoxCollider areaRef;
    TMP_Text coordsRef;
    //bool inZAxisRange = false; // -- use when implementing z axis checking

    private void Awake()
    {
        // gets and assigns the text above the object its position
        coordsRef = GetComponentInChildren<TMP_Text>();
        coordsRef.text = string.Format("({0}, {1}, {2})", position.x, position.y, position.z);

        // puts the object in its position on the map and sets its color to be out of range
        Vector3 mapPos = mapContollerRef.normPosToMapScreenPos(mapContollerRef.mapPosToNormPos(position));
        transform.localPosition = new Vector3(mapPos.x, mapPos.y, transform.localPosition.z);
        GetComponent<RawImage>().color = outRangeColor;
    }

    private void Update()
    {
        // check for z axis range here
    }

    private void OnTriggerEnter(Collider collision)
    {
        // add inZAxisRange when z axis range checking is implemented
        if (collision.CompareTag("shipScanPos"))
        {
            Debug.Log("hi ship");
            determineColor(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // add inZAxisRange when z axis range checking is implemented?
        if (collision.CompareTag("shipScanPos"))
        {
            Debug.Log("bye ship");
            determineColor(false);
        }
    }

    private void determineColor(bool status)
    {
        if (status) { GetComponent<RawImage>().color = inRangeColor; }
        else { GetComponent<RawImage>().color = outRangeColor; }
    }
}
