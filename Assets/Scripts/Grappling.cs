using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grappling : MonoBehaviour
{
    private Vector3 grapplingPoint;
    public Transform Player;
    public float speed = 5f;
    private bool isGrappling = false;
    private bool isLocked = false;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireGrapple);
        grabbable.deactivated.AddListener(StopGrapple);
    }

    void Update()
    {
        // Perform a raycast and draw a debug line
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }

        // Move player towards grappling point if grappling is active and not locked
        if (isGrappling && !isLocked)
        {
            Player.position = Vector3.Lerp(Player.position, grapplingPoint, speed * Time.deltaTime);
            if (Vector3.Distance(Player.position, grapplingPoint) < 0.1f)
            {
                isLocked = true;  // Lock the player's position when close enough to the grappling point
            }
        }
    }

    public void FireGrapple(ActivateEventArgs arg)
    {
        // Set grappling point to hit point if raycast hits
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            grapplingPoint = hit.point;
            isGrappling = true;
            isLocked = false;  // Reset the lock state when a new grapple is fired
        }
    }

    public void StopGrapple(DeactivateEventArgs arg)
    {
        isGrappling = false;
        isLocked = false;  // Reset the lock state when grappling is stopped
    }
}
