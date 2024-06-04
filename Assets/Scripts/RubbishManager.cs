using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbishManager : MonoBehaviour
{
    public GameObject[] rubbishPrefabs; // Array to store different rubbish prefabs
    public Transform factoryLocation; // The initial factory location
    public float spawnRadius = 50f; // Radius within which rubbish can spawn
    public float parabolaHeight = 10f; // The peak height of the parabola
    public float spawnInterval = 2f; // Time interval between spawns
    public int maxSpawnedRubbish; // Current maximum number of spawned rubbish elements
    public List<Collider> restrictedAreas; // List of colliders specifying restricted spawn areas

    private List<GameObject> spawnedRubbishList = new List<GameObject>(); // List to track spawned rubbish
    private GameObject spawnedTool1;
    private GameObject spawnedTool2;

    // List to hold game objects to enable visibility
    public List<GameObject> gameObjectsToEnable; // Set this in the Inspector

    private int currentRound = 0;

    public GameObject confetti;
    public GameObject stain;

    public bool noSpawn = false;

    public AudioSource confettiSound;


    void Start()
    {
        confetti.SetActive(false);
        stain.SetActive(true);
        // Disable all game objects that are the upgrades of the old factory
        foreach (var obj in gameObjectsToEnable)
        {
            obj.SetActive(false);
        }
        maxSpawnedRubbish = 5;
        StartCoroutine(SpawnRubbish());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CollectAllRubbish();
            GoToNextRound();
        }
    }

    void CollectAllRubbish()
    {
        foreach (GameObject rubbish in spawnedRubbishList)
        {
            if (rubbish != null)
            {
                Destroy(rubbish);
            }
        }
        spawnedRubbishList.Clear();
    }

    void GoToNextRound()
    {
        OnRoundCompleted();
    }
    IEnumerator SpawnRubbish()
    {

        while (true)
        {
            if(noSpawn) {
                yield break;
            }
            if (spawnedRubbishList.Count < maxSpawnedRubbish)
            {
                // Randomly select a rubbish prefab
                GameObject rubbishPrefab = rubbishPrefabs[Random.Range(0, rubbishPrefabs.Length - 2)];

                // Instantiate the rubbish at the factory location
                GameObject spawnedRubbish = Instantiate(rubbishPrefab, factoryLocation.position, Quaternion.identity);
                spawnedRubbishList.Add(spawnedRubbish);

                // Calculate a random target position within the spawn radius
                Vector3 randomPosition;
                do
                {
                    randomPosition = factoryLocation.position + (Random.insideUnitSphere * spawnRadius);
                    randomPosition.y = factoryLocation.position.y; // Keep the y position the same as the factory
                } while (IsInRestrictedArea(randomPosition));

                // Start the parabolic movement coroutine
                StartCoroutine(MoveRubbishParabolically(spawnedRubbish, factoryLocation.position, randomPosition));
            }

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveRubbishParabolically(GameObject rubbish, Vector3 startPoint, Vector3 endPoint)
    {
        float elapsedTime = 0f;
        float duration = 2f; // Duration of the parabolic movement

        Vector3 midpoint = (startPoint + endPoint) / 2;
        midpoint.y += parabolaHeight; // Raise the midpoint to create a parabolic arc

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Calculate parabolic position
            Vector3 currentPosition = Vector3.Lerp(Vector3.Lerp(startPoint, midpoint, t), Vector3.Lerp(midpoint, endPoint, t), t);
            rubbish.transform.position = currentPosition;

            yield return null;
        }

        // Ensure the rubbish reaches the exact end position
        rubbish.transform.position = endPoint;
    }

    bool IsInRestrictedArea(Vector3 position)
    {
        foreach (Collider area in restrictedAreas)
        {
            if (area.bounds.Contains(position))
            {
                return true;
            }
        }
        return false;
    }

        public void OnRoundCompleted()
    {
        // Spawn tools
        SpawnTools();
    }

    void SpawnTools()
    {
        // Spawn tool 1
        spawnedTool1 = Instantiate(rubbishPrefabs[rubbishPrefabs.Length - 2], factoryLocation.position, Quaternion.identity);
        StartCoroutine(MoveRubbishParabolically(spawnedTool1, factoryLocation.position, GetRandomSpawnPosition()));

        // Spawn tool 2
        spawnedTool2 = Instantiate(rubbishPrefabs[rubbishPrefabs.Length - 1], factoryLocation.position, Quaternion.identity);
        StartCoroutine(MoveRubbishParabolically(spawnedTool2, factoryLocation.position, GetRandomSpawnPosition()));
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;
        do
        {
            randomPosition = factoryLocation.position + (Random.insideUnitSphere * spawnRadius);
            randomPosition.y = factoryLocation.position.y; // Keep the y position the same as the factory
        } while (IsInRestrictedArea(randomPosition));
        return randomPosition;
    }

    public void OnToolCollected(GameObject tool)
    {
        if (tool == spawnedTool1)
        {
            spawnedTool1 = null;
        }
        else if (tool == spawnedTool2)
        {
            spawnedTool2 = null;
        }

        // Check if both tools are collected
        if (AreAllToolsCollected())
        {
            Debug.Log("Next Round!");

            // Increase the max number of spawned rubbish by 3
            maxSpawnedRubbish += 3;

            gameObjectsToEnable[currentRound].SetActive(true);
            currentRound++;


            // Clear the current spawned rubbish list
            foreach (GameObject rubbish in spawnedRubbishList)
            {
                if (rubbish != null)
                {
                    Destroy(rubbish);
                }
            }
            spawnedRubbishList.Clear();

            // Reset the counter
            CounterController.Instance.ResetCounter();

            // Enable the next game object on the list of renewables energy sources
            // EnableNextGameObject();

            foreach (Transform child in stain.transform)
            {
                child.localScale = new Vector3(child.localScale.x * 0.9f, child.localScale.y, child.localScale.z * 0.9f);
            }
            // Start spawning rubbish again
            if(currentRound < gameObjectsToEnable.Count) {
                StartCoroutine(SpawnRubbish());
            } else {
                noSpawn = true;
                StartConfetti();
            }
            


        }
    }

    void StartConfetti()
    {
        if (confettiSound != null)
        {
            confettiSound.Play();
        }
        // Start the confetti particle system
        if (confetti != null)
        {
            confetti.SetActive(true);
            
        }
        if(stain != null)
        {
            stain.SetActive(false);
        }
    }

    public bool AreAllToolsCollected()
    {
        return spawnedTool1 == null && spawnedTool2 == null;
    }

    public int GetMaxSpawnedRubbish()
    {
        return maxSpawnedRubbish;
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

}