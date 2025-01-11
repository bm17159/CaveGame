using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttached : XRGrabInteractable
{

    public Transform leftAttachedTransform;
    public Transform rightAttachedTransform;

    public override Transform GetAttachTransform(IXRInteractor interactor)
    {
        Debug.Log("GetAttachTransform");

        Transform i_attachTransform = null;


        // Determine whether it's the left or right hand
        if (interactor.transform.CompareTag("Left Hand"))
        {
            i_attachTransform = leftAttachedTransform;
        }
        if (interactor.transform.CompareTag("Right Hand"))
        {
            i_attachTransform = rightAttachedTransform;
        }
        return i_attachTransform != null ? i_attachTransform : base.GetAttachTransform(interactor);
    }
}