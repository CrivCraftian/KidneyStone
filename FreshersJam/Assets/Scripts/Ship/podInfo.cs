using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class podInfo : MonoBehaviour
{
    [SerializeField] PodController podControllerRef;
    [SerializeField] TMP_Text podIDText;
    [SerializeField] TMP_Text podNameText;
    [SerializeField] TMP_Text podDescriptionText;
    [SerializeField] RawImage status;
    [SerializeField] int podID = -1;

    private void OnValidate()
    {
        if (podIDText != null) { podIDText.text = podID.ToString(); }
    }

    private void Awake()
    {
        if (podIDText != null)  { podIDText.text = podID.ToString(); }
    }

    private void Update()
    {
        if (podNameText == null || podDescriptionText == null) { return; }

        if (podID != -1 && !podControllerRef.GetPod(podID - 1).IsEmpty())
        {
            status.color = Color.red;
            podNameText.text = podControllerRef.GetPod(podID - 1).PodContents().GetName();
            podDescriptionText.text = podControllerRef.GetPod(podID - 1).PodContents().GetDescription(); 
            
        }

        else
        {
            status.color = Color.green;
            podNameText.text = "Empty";
            podDescriptionText.text = "Nothing stored in cargo pod";
        }

    }
}
