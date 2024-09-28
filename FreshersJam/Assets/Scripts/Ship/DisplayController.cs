using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour
{
    [SerializeField] Color Available;
    [SerializeField] Color Closed;

    [SerializeField] PodController podController;

    [SerializeField] List<RawImage> PodImages;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePodIcons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePodIcons()
    {
        for (int i = 0; i < PodImages.Count; i++)
        {
            if(podController.GetPod(i).IsEmpty())
            {
                PodImages[i].color = Available;
            }
            else
            {
                PodImages[i].color = Closed;
            }
        }
    }
}
