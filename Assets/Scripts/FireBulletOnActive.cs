using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class FireBulletOnActive : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        // On start, get vr grab component and connect it to the "activated" event
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FireBullet(ActivateEventArgs arg)
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
