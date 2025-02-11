using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MapCreation : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<GameObject> previousObjects;


    [Header("Transforms")]
    [SerializeField] private GameObject currentObject;
    [SerializeField] private Transform player;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private float offset;


    [Header("Other")]
    [SerializeField] private int random;
    [SerializeField] private float tolerance = 10f;
    private int lastObject;
    [SerializeField] private bool locker = false;



    // Start is called before the first frame update
    void Start()
    {
        currentObject = previousObjects[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.y - currentObject.transform.position.y) <= tolerance && locker == false)
        {
            calculatePos();
        }
        else if (Mathf.Abs(player.position.y - currentObject.transform.position.y) > tolerance)
        {
            locker = false;
        }
    }

    

    public void calculatePos()
    {
        // When the player's position = current pos, find the next piece to use
        Debug.Log("Equal");
        random = Random.Range(0, objects.Count);

        // Instantiate the next object and add it to the list
        GameObject newObject = Instantiate(objects[random], parentTransform);
        previousObjects.Add(newObject);
        lastObject = previousObjects.Count - 1;

        // Set new object's position directly below the last one
        Vector3 lastObjectPos = previousObjects[lastObject - 1].transform.position;
        Vector3 newPos = new Vector3(lastObjectPos.x, lastObjectPos.y - offset, lastObjectPos.z);
        newObject.transform.position = newPos;

        // Log the new object for debugging
        Debug.Log(previousObjects[lastObject]);

        // Update current object and lock the process
        currentObject = newObject;
        //locker = true;
    }
}
