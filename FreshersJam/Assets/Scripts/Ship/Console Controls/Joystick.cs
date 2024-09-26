
using UnityEngine;

public enum Axis
{
    X, Y, Z
}

public class Joystick : MonoBehaviour, interactClass
{
    [SerializeField] float MoveSpeed = 0.01f;
    [SerializeField] Camera camera;

    [SerializeField] private ShipController controller;

    [SerializeField] private Axis PhysicalOrientation;
    [SerializeField] private Axis direction;

    [SerializeField] private float BaseRotation = 100f;
    [SerializeField] private float MaxRotation = 60f;

    [SerializeField] private float ResetSpeed = 0.5f;

    [SerializeField] private Transform joyBase;

    [SerializeField] private Transform temp;

    float AlteredPosition = 0f;

    float position;

    [SerializeField] bool reset = false;

    bool clicked = false;

    public void useClick()
    {
        clicked = true;
        reset = false;
    }

    public void useHold()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        switch (PhysicalOrientation)
        {
            case Axis.X:
                position = transform.position.x;
                break;
            case Axis.Y:
                position = transform.position.y;
                break;
            case Axis.Z:
                position = transform.position.z;
                break;
            default:

                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Resetting();
        MoveTowardMouse(Time.deltaTime);
    }

    public void ResetPosition()
    {
        reset = true;
    }

    void Resetting()
    {
        if(reset)
        {
            AlteredPosition = 0;

            Vector3 currentRotation = joyBase.localRotation.eulerAngles;

            float targetRotation = Mathf.LerpAngle(currentRotation.y, 100, ResetSpeed * Time.deltaTime);

            joyBase.localRotation = Quaternion.Euler(new Vector3(0, targetRotation, 0));
        }
    }

    void MoveTowardMouse(float deltaTime)
    {
        switch (direction)
        {
            case Axis.X:
                controller.AlterPosition(new Vector3(AlteredPosition, 0, 0));
                break;
            case Axis.Y:
                controller.AlterPosition(new Vector3(0, AlteredPosition, 0));
                break;
            case Axis.Z:
                controller.AlterPosition(new Vector3(0, 0, AlteredPosition));
                break;
        }

        if (clicked)
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Vector3 worldMousePos = hit.point;

                float horizontalDistance = 0.0f;

                switch (PhysicalOrientation)
                {
                    case Axis.X:
                        horizontalDistance = worldMousePos.x;
                        break;
                    case Axis.Y:
                        horizontalDistance = worldMousePos.y;
                        break;
                    case Axis.Z:
                        horizontalDistance = worldMousePos.z;
                        break;
                    default:

                        break;
                }

                horizontalDistance -= position;
                horizontalDistance *= 5;

                float rotationAngle = BaseRotation - Mathf.Clamp(horizontalDistance * MaxRotation, -MaxRotation, MaxRotation);

                AlteredPosition = -(((rotationAngle - 100) * Time.deltaTime)/4);

                joyBase.localRotation = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
            }

            if (Input.GetMouseButtonUp(0))
            {
                clicked = false;
            }
        }
    }
}
