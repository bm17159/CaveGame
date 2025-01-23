using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private ArrayList objects;
    [SerializeField] private ArrayList previousObjects;


    [Header("Transforms")]
    [SerializeField] private Transform currentObject;
    [SerializeField] private Transform player;


    [Header("Other")]
    [SerializeField] private int random;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player = currentObject)
        {
            random = Random.Range(0, objects.);
        }
    }
}
