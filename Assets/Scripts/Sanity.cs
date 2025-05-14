using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanity : MonoBehaviour
{
   [Header("Sanity Settings")]
    [SerializeField] private float maxSanity = 100f;
    [SerializeField] private float currentSanity;
    [SerializeField] private float baseDecayRate = 0.5f; // Default sanity decay per second
    [SerializeField] private float monsterDecayRate = 2f; // Extra decay when near monsters
    [SerializeField] private float detectionRadius = 15f; // Distance for monster sanity effect

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask monsterLayer;

    void Start()
    {
        currentSanity = maxSanity;
    }

    void Update()
    {
        float decayRate = baseDecayRate;

        // Check if any monsters are nearby
        Collider[] monsters = Physics.OverlapSphere(player.position, detectionRadius, monsterLayer);
        if (monsters.Length > 0)
        {
            decayRate += monsterDecayRate; // Increase sanity loss when monsters are close
        }

        currentSanity -= decayRate * Time.deltaTime;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);

        HandleSanityEffects();
    }

    private void HandleSanityEffects()
    {
        if (currentSanity <= 30)
        {
            Debug.Log("Sanity is low! You're starting to lose grip on reality...");
        }

        if (currentSanity <= 0)
        {
            Debug.Log("Sanity fully depleted! Player has lost their mind.");
        }
    }
}
