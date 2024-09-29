using UnityEngine;

public abstract class AbstractSObject : MonoBehaviour
{
    [SerializeField] protected string Name;
    [SerializeField] public string Description;
    [SerializeField] protected int Value;

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
}
