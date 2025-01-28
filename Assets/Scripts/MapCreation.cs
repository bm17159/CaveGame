using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCreation : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<GameObject> previousObjects;


    [Header("Transforms")]
    [SerializeField] private GameObject currentObject;
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawn;
    [SerializeField] private Vector3 offset;


    [Header("Other")]
    [SerializeField] private int random;

    private int lastObject;
    [SerializeField] private bool locker = false;



    // Start is called before the first frame update
    void Start()
    {
        currentObject = objects[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y == currentObject.transform.position.y && locker == false)
        {
            calculatePos();
            
        } else if (player.position.y != currentObject.transform.position.y)
        {
            locker = false;
        }
    }

    private void calculatePos()
    {
        // When the player's position = current pos, find the next piece to use
        Debug.Log("Equal");
        random = Random.Range(0, objects.Count);
        // add next tunnel
        previousObjects.Add(Instantiate(objects[random], spawn));
        lastObject = previousObjects.Count - 1;
        offset = new Vector3(previousObjects[lastObject].transform.position.x, (previousObjects[lastObject].transform.position.y - ((previousObjects[lastObject].transform.position.y) * 2)), previousObjects[lastObject].transform.position.z);
        previousObjects[lastObject].transform.position =  offset;
        currentObject = previousObjects[lastObject];
        locker = true;
    }
}
