using UnityEngine;

public abstract class AbstractSObject : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected string Description;
    [SerializeField] protected string AltValue;
    [SerializeField] protected int Value;
    [SerializeField] protected bool DisplayAltValue;

    public string GetName()
    {
        return Name;
    }

    public string GetDescription()
    {
        return Description;
    }

    public int GetValue()
    {
        return Value;
    }

    public string GetAltValue()
    {
        return AltValue;
    }

    public bool GetAltCheck()
    {
        return DisplayAltValue;
    }
}
