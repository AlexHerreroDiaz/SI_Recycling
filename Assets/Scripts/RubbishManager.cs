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
    public int maxSpawnedRubbish = 10; // Maximum number of spawned rubbish elements
    public List<Collider> restrictedAreas; // List of colliders specifying restricted spawn areas

    private List<GameObject> spawnedRubbishList = new List<GameObject>(); // List to track spawned rubbish

    void Start()
    {
        StartCoroutine(SpawnRubbish());
    }

    IEnumerator SpawnRubbish()
    {
        while (true)
        {
            if (spawnedRubbishList.Count < maxSpawnedRubbish)
            {
                // Randomly select a rubbish prefab
                GameObject rubbishPrefab = rubbishPrefabs[Random.Range(0, rubbishPrefabs.Length)];

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
}
