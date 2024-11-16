using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject collectablePrefab; 
    public float spawnRange = 5f;     
    public int maxObjects = 10;  
    private GameObject[] spawnedObjects; 
    public float navMeshSampleRadius = 2f; 

    private void Start()
    {
        spawnedObjects = new GameObject[maxObjects];

        for (int i = 0; i < maxObjects; i++)
        {
            SpawnCollectable(i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            if (spawnedObjects[i] != null && !spawnedObjects[i].activeInHierarchy)
            {
                SpawnCollectable(i);
            }
        }
    }

    private void SpawnCollectable(int index)
    {
        Vector3 randomPosition;


        int maxAttempts = 10; 
        int attempts = 0;
        bool validPositionFound = false;
        do
        {
            randomPosition = GetRandomSpawnPosition();
            validPositionFound = TryGetPositionOnNavMesh(randomPosition, out randomPosition);
            attempts++;
        } 
        while (!validPositionFound && attempts < maxAttempts);

        if (!validPositionFound)
        {
            Debug.LogWarning("Could not find a valid position on the NavMesh after multiple attempts.");
            return;
        }

        if (spawnedObjects[index] == null)
        {
            spawnedObjects[index] = Instantiate(collectablePrefab, randomPosition, Quaternion.identity);
        }
        else
        {
            spawnedObjects[index].transform.position = randomPosition;
            spawnedObjects[index].SetActive(true);
        }

        spawnedObjects[index].transform.localScale = collectablePrefab.transform.localScale;

        PositionOnTerrain(spawnedObjects[index]);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(randomX, 0, randomZ) + transform.position;
    }

    private bool TryGetPositionOnNavMesh(Vector3 position, out Vector3 navMeshPosition)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, navMeshSampleRadius, NavMesh.AllAreas))
        {
            navMeshPosition = hit.position;
            return true;
        }
        navMeshPosition = Vector3.zero;
        return false;
    }

    private void PositionOnTerrain(GameObject collectable)
    {
        Collider collider = collectable.GetComponent<Collider>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            Vector3 position = collectable.transform.position;
            position.y += bounds.extents.y; 
            collectable.transform.position = position;
        }
    }
}
