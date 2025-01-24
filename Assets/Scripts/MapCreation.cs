using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCreation : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private List<int> previousObjects;


    [Header("Transforms")]
    [SerializeField] private Transform currentObject;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 target;


    [Header("Other")]
    [SerializeField] private int random;

    [SerializeField] private Vector3 currentPos;
    [SerializeField] private int currentSelection;
    [SerializeField] private bool locker = false;



    // Start is called before the first frame update
    void Start()
    {
        currentObject = objects[0].transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.position == currentObject.position && locker == false)
        {
            calculatePos();
            
        } else if (player.position != currentObject.position)
        {
            locker = false;
        }
    }

    private void calculatePos()
    {
        // When the player's position = current pos, find the next piece to use
        Debug.Log("Equal");
        random = Random.Range(0, objects.Count);
        
        // make sure same piece isn't moved
        while (random == currentSelection)
        {
            random = Random.Range(0, objects.Count);
        } 
        
        // store random for later
        previousObjects.Add(random);
        currentSelection = random;
        
        // move the object
        currentPos = currentObject.position;
        target = new Vector3(currentPos.x, (currentPos.y) + (currentPos.y)/2, currentPos.z);
        objects[random].transform.position = target;
        locker = true;
        
        

    }
}
