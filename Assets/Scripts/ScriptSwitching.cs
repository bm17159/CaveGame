using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ScriptSwitching : MonoBehaviour
{
    [SerializeField] private MonoBehaviour Script1;
    [SerializeField] private MonoBehaviour Script2;
    [SerializeField] private InputActionProperty switchAction;
    
    //private InputAction switchAction;

    
    private int c = 0;

    // Start is called before the first frame update
    void Start()
    {
        //XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        //grabbable.selectEntered.AddListener(Switch);

    }
    private void OnEnable()
    {
        switchAction.action.Enable();
        switchAction.action.performed += Switcher;
    }
    private void OnDisable()
    { // Unsubscribe from the performed event and disable the action
      switchAction.action.performed -= Switcher;
      switchAction.action.Disable(); 
    }
        private void Switcher(InputAction.CallbackContext context)
    {
        if (c == 0)
        {
            Script1.enabled = true;
            Script2.enabled = false;
            c++;
        } else if (c == 1)
        {
            Script1.enabled = false;
            Script2.enabled = true;
            c = 0;
        }
       
    }
}
