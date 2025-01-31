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

        // Flashlight starts off
        lightObject.SetActive(false);
        lensObject.GetComponent<Renderer>().material = offMaterial;
    }
    private void Update()
    {
        // monster detection
        if (c == 1)
        {
            RaycastHit hit;
            if (Physics.Raycast(lightObject.transform.position, lightObject.transform.forward, out hit) && hit.collider.gameObject.tag == "Monster")
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void On (ActivateEventArgs arg)
    {
        // c keeps count of the status of 
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
  
}

