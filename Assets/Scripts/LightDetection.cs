using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    public Light flashlight;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(flashlight.transform.position, flashlight.transform.forward, out hit) && hit.collider.gameObject.tag == "Monster")
        {
            Disappear(hit.collider.gameObject);
            Debug.Log("Hit"+ hit.collider.gameObject.name);
        }
    }

    private void Disappear(GameObject obj)
    {
        Destroy(obj);
    }
}