using System.Collections.Generic;

public class Pod
{
    private AbstractSObject store;

    public AbstractSObject PodContents()
    {
        return store;
    }

    public void FillPod(AbstractSObject store)
    { this.store = store; }

    public void EmptyPod()
    {
        store = null;
    }
}
