using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapContoller : MonoBehaviour
{
    [Header("SETTINGS")]
    public ShipController shipControllerRef;
    public RectTransform mapScreen;
    public ScrollRect mapRect;

    [Header("NORMALISED POSITION CLAMP SETTINGS")]
    [Tooltip("USE TO LIMIT MOVEMENT ON MAP (ALSO DETERMINES MIN/MAX OF NORMALISED POSITIONS)")] public Vector3 minNormPosClamp;
    [Tooltip("USE TO LIMIT MOVEMENT ON MAP (ALSO DETERMINES MIN/MAX OF NORMALISED POSITIONS)")] public Vector3 maxNormPosClamp;

    [Header("MIN/MAX POSITION SETTINGS")]
    [Tooltip("MIN. POSITION OF X Y AND Z OF MAP")] public Vector3 minMapPos;
    [Tooltip("MAX POSITION OF X Y AND Z  OF MAP")] public Vector3 maxMapPos;

    // values
    [HideInInspector] public Vector3 normalisedPosition;

    // NOTE: i would've made this be done automatically but im idk how i'd get it accurately (and its not a big enough priority to worry about)
    // can be assigned manually by going around the edges of the map and use the x and y values that show up in "mapMain" (at least thats what its called in the prefab)
    [Tooltip("Set max (local) position a map object can be in any direction" +
        "\nX = TOP | Y = BOTTOM | Z = LEFT | W = RIGHT")] [SerializeField] Vector4 boxEdgePos = new Vector4(-90,90,90,-90);
    
    private void Awake()
    {
        // assigns normalisedPosition the converted default position
        normalisedPosition = mapPosToNormPos(shipControllerRef.shipPosition);

        // sets the position of the ship on start
        mapRect.horizontalNormalizedPosition = normalisedPosition.x;
        mapRect.verticalNormalizedPosition = normalisedPosition.y;
    }

    private void Update()
    {
        Vector3 clampShip = shipControllerRef.shipPosition;
        clampShip.x = Mathf.Clamp(clampShip.x, minMapPos.x, maxMapPos.x);
        clampShip.y = Mathf.Clamp(clampShip.y, minMapPos.y, maxMapPos.y);
        clampShip.z = Mathf.Clamp(clampShip.z, minMapPos.z, maxMapPos.z);

        shipControllerRef.ForcePosition(clampShip);

        normalisedPosition = mapPosToNormPos(shipControllerRef.shipPosition);

        // clamps normalisedPosition so it stays within chosen area
        // seems to work without this but im not taking any chances
        normalisedPosition.x = Mathf.Clamp(normalisedPosition.x, minNormPosClamp.x, maxNormPosClamp.x);
        normalisedPosition.y = Mathf.Clamp(normalisedPosition.y, minNormPosClamp.y, maxNormPosClamp.y);
        normalisedPosition.z = Mathf.Clamp(normalisedPosition.z, minNormPosClamp.z, maxNormPosClamp.z);

        // updates position on screen
        mapRect.horizontalNormalizedPosition = normalisedPosition.x;
        mapRect.verticalNormalizedPosition = normalisedPosition.y;
    }

    // converts (fake) map position to normal position based on map size
    public Vector3 mapPosToNormPos(Vector3 mapPosition)
    {
        Vector3 normalisedPosition = Vector3.zero;

        // converts map position to normalised position
        normalisedPosition.x = maxNormPosClamp.x + ((mapPosition.x - minMapPos.x) * (minNormPosClamp.x - maxNormPosClamp.x) / (maxMapPos.x - minMapPos.x));
        normalisedPosition.y = maxNormPosClamp.y + ((mapPosition.y - minMapPos.y) * (minNormPosClamp.y - maxNormPosClamp.y) / (maxMapPos.y - minMapPos.y));
        normalisedPosition.z = maxNormPosClamp.z + ((mapPosition.z - minMapPos.z) * (minNormPosClamp.z - maxNormPosClamp.z) / (maxMapPos.z - minMapPos.z));

        return normalisedPosition;
    }

    // used to convert normal position to position on (screen) map (for map objects)
    public Vector3 normPosToMapScreenPos(Vector3 normPosition)
    {
        Vector3 mapScreenPosition = Vector3.zero;

        // converts map position to normalised position
        mapScreenPosition.x = boxEdgePos.w + ((normPosition.x - minNormPosClamp.x) * (boxEdgePos.z - boxEdgePos.w) / (maxNormPosClamp.x - minNormPosClamp.x));
        mapScreenPosition.y = boxEdgePos.x + ((normPosition.y - minNormPosClamp.y) * (boxEdgePos.y - boxEdgePos.x) / (maxNormPosClamp.y - minNormPosClamp.y));
        mapScreenPosition.z = 0f;

        return mapScreenPosition;
    }
}
