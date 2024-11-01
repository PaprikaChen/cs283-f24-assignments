using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject collectablePrefab; 
    public float spawnRange = 5f;       
    public int maxObjects = 10;     
    private GameObject[] spawnedObjects; 

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
        // Generate a random position within the spawn range
        Vector3 randomPosition = GetRandomSpawnPosition();

        // If this is the first spawn, instantiate a new object; otherwise, reposition an existing one
        if (spawnedObjects[index] == null)
        {
            spawnedObjects[index] = Instantiate(collectablePrefab, randomPosition, Quaternion.identity);
        }
        else
        {
            spawnedObjects[index].transform.position = randomPosition;
            spawnedObjects[index].transform.localScale = new Vector3(2,2,2);
            spawnedObjects[index].SetActive(true);
        }

        PositionOnTerrain(spawnedObjects[index]);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomZ = Random.Range(-spawnRange, spawnRange);
        
        return new Vector3(randomX, 0, randomZ) + transform.position;
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
