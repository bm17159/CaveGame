using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class MultiUseGun : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private bool isGrappling;
    [SerializeField] private bool mode; //Grappling = true, and flare = false
    [SerializeField] private bool lockTarget;
    [SerializeField] private ActionBasedContinuousMoveProvider moveProvider;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private InputActionProperty switchAction;

    private int c = 0;


    [Header("Transforms")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spawnPoint;

    [Header("Grappling Info")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask layerMask;

    [Header("Flare Gun Info")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireSpeed = 20;


    private void Start()
    {
        // On start, get vr grab component and connect it to the "activated" event
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(StartGrapple);
        grabInteractable.deactivated.AddListener(StopGrapple);
        grabInteractable.activated.AddListener(FireBullet);

        moveProvider = playerTransform.GetComponent<ActionBasedContinuousMoveProvider>();
    }

    private void Update()
    {
        if (mode == true)
        {
            Grapple();
        }
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

    #region Switching
    private void Switcher(InputAction.CallbackContext context)
    {
        Debug.Log("Button Pressed");
        if (c == 0) {
            mode = true;
            c++;
            Debug.Log("true");
        } else if (c == 1)
        {
            mode = false;
            c = 0;
            Debug.Log("false");
        }
    }

    #endregion

    #region Grappling
    private void StartGrapple(ActivateEventArgs arg)
    {
       isGrappling = true;
    }

    private void StopGrapple(DeactivateEventArgs arg)
    {
        isGrappling= false;
    }

    private void Grapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit, maxDistance, layerMask) && lockTarget == false)
        {
            // The Ray hit something
            targetPoint = hit.point;
        }

        if (isGrappling == true)
        {
            lockTarget = true;
            moveProvider.useGravity = false;
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPoint, speed * Time.deltaTime);
        }
        else if (isGrappling == false)
        {
            moveProvider.useGravity = true;
            lockTarget = false;
        }

    }
    #endregion
    public void FireBullet(ActivateEventArgs arg)
    {
        if (mode == false)
        {
            // Set what the spawned bullet is
            GameObject spawnedBullet = Instantiate(bullet);
            // Set the position of the spawned bullet to the position of the spawn point
            spawnedBullet.transform.position = spawnPoint.position;
            // Set the velocity of the bullet, ensuring it fires forward
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            // Destroy the bullet after 5 seconds of travel
            Destroy(spawnedBullet, 5);
        }
    }
}