using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PodController : MonoBehaviour
{
    [SerializeField] DisplayController displayController;
    public List<PodV2> podsV2;
    List<Pod> pods = new List<Pod>(6);

    public PodController() 
    {
        for (int i = 0; i < 6; i++)
        {
            pods.Add(new Pod());
        }
    }

    public bool FillPod(AbstractSObject spaceObject)
    {
        for (int i = 0; i < podsV2.Count; i++)
        {
            if (podsV2[i].IsEmpty())
            {
                if (spaceObject.GetType() == typeof(Person))
                {
                    podsV2[i].gameObject.AddComponent<Person>();
                    Person personRef = podsV2[i].gameObject.GetComponent<Person>();

                    personRef.ChangeName(spaceObject.GetName());
                    personRef.ChangeDescription(spaceObject.GetDescription());
                    personRef.ChangeValue(spaceObject.GetValue());
                    personRef.ChangeAltValue(spaceObject.GetAltValue());
                    personRef.ChangeAltCheck(spaceObject.GetAltCheck());
                    personRef.ChangeSpecialID(spaceObject.GetSpecialID());

                    podsV2[i].FillPod(personRef);
                }

                if (spaceObject.GetType() == typeof(Scrap))
                {
                    podsV2[i].gameObject.AddComponent<Scrap>();
                    Scrap scrapRef = podsV2[i].gameObject.GetComponent<Scrap>();

                    scrapRef.ChangeName(spaceObject.GetName());
                    scrapRef.ChangeDescription(spaceObject.GetDescription());
                    scrapRef.ChangeValue(spaceObject.GetValue());
                    scrapRef.ChangeAltValue(spaceObject.GetAltValue());
                    scrapRef.ChangeAltCheck(spaceObject.GetAltCheck());
                    scrapRef.ChangeSpecialID(spaceObject.GetSpecialID());

                    podsV2[i].FillPod(scrapRef);
                }

                displayController.UpdatePodIcons();
                return true;
            }
        }

        return false;
    }

    public PodV2 GetPod(int podnum)
    {
        return podsV2[podnum];
    }

    public void EjectPod(int podnum)
    {
        podsV2[podnum].EmptyPod();
        displayController.UpdatePodIcons();
    }

    // pod 1 left, pod 2 right



}
