using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMonsterController : MonoBehaviour
{
    [Header("Spawning Parameters")]
    [SerializeField] private float timer = 0f; 
    [SerializeField] private float min = 60f; 
    [SerializeField] private float max = 180f; 
    [SerializeField] private float spawnRadius = 5f; // Radius of spawning sphere
    [SerializeField] private Transform Decsender;
    
    [Header("Objects and Transforms")]
    [SerializeField] private GameObject EyeMonster;
    [SerializeField] private Transform Player; 

    private GameObject spawnedCrawler; 
    

    void Start()
    {
        timer = Random.Range(min, max); // Set random initial timer
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime; // Countdown spawn timer

        if (timer <= 0f)
        {
            Spawn();
            timer = Random.Range(min, max); // Reset timer for next spawn
        }
    }

    private void Spawn()
    {
        // Generate random position within sphere radius around player
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        
        Vector3 spawnPos = Player.position + randomOffset;

        // Instantiate monster at calculated position
        spawnedCrawler = Instantiate(EyeMonster, spawnPos, Quaternion.identity, Decsender);

        // Rotate monster to face player
        spawnedCrawler.transform.LookAt(Player);
        
        // Apply additional 90-degree rotation around Z axis
        spawnedCrawler.transform.Rotate(0f, 90f, 0f, Space.Self);
    }
}
