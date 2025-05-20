using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrawlerMonsterController : MonoBehaviour
{
    [Header("Spawning Parameters")]
    [SerializeField] private float timer = 0f; 
    [SerializeField] private float min = 60f; 
    [SerializeField] private float max = 180f; 
    [SerializeField] private float duration = 2f;
    [SerializeField] private float radius;
    private float angle;
    
    private float elapsedTime = 0f;
    
    [Header("Objects and Transforms")]
    [SerializeField] private GameObject Crawler;
    [SerializeField] private Transform Player; 
    
    private GameObject spawnedCrawler; 
    private Vector3 initialPos; 
    private Vector3 finalPos;
    private Vector3 constant;
    private bool isMoving = false;

    void Start()
    {
        // Set the random initial timer before spawning
        timer = Random.Range(min, max);

        constant = new Vector3(0, 10, 0);
        
        // Set the spawn position above the player and final position at the player's location
        //initialPos = Player.position - constant; 
        //finalPos = Player.position + constant; 
    }

    void FixedUpdate()
    {
        // Count down the spawn timer
        timer -= Time.deltaTime;
        
        // If timer runs out and no monster is moving, spawn one
        if (timer <= 0f && !isMoving)
        {
            Spawn();
        }

        // If a monster is moving, update its movement
        if (isMoving)
        {
            MoveMonster();
        }
    }

    private void Spawn()
    {
        // Get a random angle around the circle
        angle = Random.Range(0f, 2f * Mathf.PI);
        
        // Calculate X and Z on the circle
        float x = Player.position.x + radius * Mathf.Cos(angle);
        float z = Player.position.z + radius * Mathf.Sin(angle);

        // Set spawn position (below player)
        float spawnY = Player.position.y - 10f;
        initialPos = new Vector3(x, spawnY, z);

        // Set final position directly above
        float riseHeight = 20f;
        finalPos = new Vector3(x, spawnY + riseHeight, z);

        // Calculate the outward direction from player to spawn point
        Vector3 outward = (initialPos - Player.position).normalized;

        // Create a rotation that looks in the outward direction
        Quaternion lookRotation = Quaternion.LookRotation(outward, Vector3.up);

        // Apply an additional -90 degree X rotation to make it crawl up the wall
        Quaternion crawlRotation = lookRotation * Quaternion.Euler(-170f, 0f, 0f);

        // Spawn with calculated rotation
        spawnedCrawler = Instantiate(Crawler, initialPos, crawlRotation);

        // Reset and flag
        elapsedTime = 0f;
        isMoving = true;
    }

    private void MoveMonster()
    {
        // Move monster gradually using Lerp for smooth movement
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            spawnedCrawler.transform.position = Vector3.Lerp(initialPos, finalPos, elapsedTime / duration);
        }
        else
        {
            // Destroy the monster after it reaches the destination
            Destroy(spawnedCrawler);
            
            // Reset spawn timer for the next monster
            timer = Random.Range(min, max);
            
            // Stop movement logic until next spawn
            isMoving = false;
        }
    }
}
