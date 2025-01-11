using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightToggle : MonoBehaviour
{
    public Material onMaterial;
    public Material offMaterial;
    public GameObject lensObject;
    public GameObject lightObject;

    private int c;

    // Start is called before the first frame update
    void Start()
    {
        // On start, get vr grab component and connect it to the "activated" event
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(On);
        grabbable.deactivated.AddListener(Off);

        // Flashlight starts off
        lightObject.SetActive(false);
        lensObject.GetComponent<Renderer>().material = offMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void On (ActivateEventArgs arg)
    {
        c ++;
        // Toggle system
        if (c == 1)
        {
            // Change material of lens to on
            lensObject.GetComponent<Renderer>().material = onMaterial;
            // Set light to active
            lightObject.SetActive(true);
        } else if (c == 2)
        {
            // Change material of lens to off
            lensObject.GetComponent<Renderer>().material = offMaterial;
            // Set light to !active
            lightObject.SetActive(false);
            c -= 2;
        }
       
        
    }
    public void Off (DeactivateEventArgs arg)
    {
       

    }
}

