using TMPro;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Vector3 shipPosition {  get; private set; }

    public int FuelCount { get; private set; }
    public int HullIntegrity { get; private set; }

    [SerializeField] float movementCounterLimit = 750f;
    [HideInInspector] public float movementCounter;

    float fuelBarWidth = 0;

    [SerializeField] ManifestController manifestController;

    [SerializeField] private RectTransform fuelBar;
    [SerializeField] private RectTransform HullBar;

    [SerializeField] private TextMeshProUGUI[] TextDisplays = new TextMeshProUGUI[3];

    // Start is called before the first frame update
    void Start()
    {
        FuelCount = 10;
        HullIntegrity = 10;

        shipPosition = new Vector3(-900, -936, 353);

        fuelBarWidth = fuelBar.localScale.y/10f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplays();
        UpdateButtons();
        movementCheck();
    }

    public void movementCheck()
    {
        movementCounter = Mathf.Clamp(movementCounter, 0, movementCounterLimit);

        if (movementCounter == movementCounterLimit)
        {
            movementCounter = 0;
            AlterFuel(FuelCount-1);
        }
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

    public void AlterPosition(Vector3 position)
    {
        shipPosition += position;
    }

    public void ForcePosition(Vector3 position)
    {
        shipPosition = position;
    }

    void UpdateDisplays()
    {
        UpdatePosDisplay();
    }

    void UpdatePosDisplay()
    {
        TextDisplays[0].text = "X: " + (int)shipPosition.x;
        TextDisplays[1].text = "Y: " + (int)shipPosition.y;
        TextDisplays[2].text = "Z: " + (int)shipPosition.z;
    }

    public void AlterFuel(int fuel)
    {
        FuelCount = Mathf.Clamp(fuel, 0, 10);
    }

    void AlterHull(int hullintegrity)
    {
        FuelCount = Mathf.Clamp(hullintegrity, 0, 10);
    }
}
