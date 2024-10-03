using UnityEngine;

public abstract class AbstractSObject : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] protected string Description;
    [SerializeField] protected string AltValue;
    [SerializeField] protected int Value;
    [SerializeField] protected bool DisplayAltValue;
    [SerializeField] protected string SpecialID;

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

    public string GetSpecialID()
    {
        return SpecialID;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public void ChangeDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void ChangeValue(int newValue)
    {
        Value = newValue;
    }

    public void ChangeAltValue(string newAltValue)
    {
        AltValue = newAltValue;
    }

    public void ChangeAltCheck(bool newAltCheck)
    {
        DisplayAltValue = newAltCheck;
    }

    public void ChangeSpecialID(string newID)
    {
        SpecialID = newID;
    }
}
