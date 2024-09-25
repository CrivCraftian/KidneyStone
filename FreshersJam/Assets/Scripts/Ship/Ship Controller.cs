using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Vector3 shipPosition {  get; private set; }
    [SerializeField] private int FuelCount;
    [SerializeField] private int HullIntegrity;

    [SerializeField] private RectTransform fuelBar;
    [SerializeField] private RectTransform HullBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        UpdateButton(fuelBar, FuelCount);
        UpdateButton(HullBar, HullIntegrity);
    }

    void UpdateButton(RectTransform bar, float value)
    {
        bar.localScale = new Vector3(bar.localScale.x, 0.036f*value, bar.localScale.z);
        // bar.localPosition = new Vector3(bar.localPosition.x, -170 + ((bar.localScale.y/2)*94.44444f), bar.localPosition.z);
    }

    void AlterPosition(Vector3 position)
    {
        shipPosition += position;
    }
}
