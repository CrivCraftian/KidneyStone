using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Vector3 shipPosition {  get; private set; }
    public int FuelCount { get; private set; }
    public int HullIntegrity { get; private set; }

    float fuelBarWidth = 0;

    [SerializeField] private RectTransform fuelBar;
    [SerializeField] private RectTransform HullBar;

    // Start is called before the first frame update
    void Start()
    {
        FuelCount = 10;
        HullIntegrity = 10;

        fuelBarWidth = fuelBar.localScale.y/10f;
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
        bar.localScale = new Vector3(bar.localScale.x, fuelBarWidth*value, bar.localScale.z);
    }

    void AlterPosition(Vector3 position)
    {
        shipPosition += position;
    }

    void AlterFuel(int fuel)
    {
        FuelCount = Mathf.Clamp(fuel, 0, 10);
    }

    void AlterHull(int hullintegrity)
    {
        FuelCount = Mathf.Clamp(hullintegrity, 0, 10);
    }
}
