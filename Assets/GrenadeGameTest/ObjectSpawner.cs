using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to be spawned
    public Transform spawnPoint; // The position where the object will be spawned

    void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        // Check if the object to spawn is set
        if (objectToSpawn != null)
        {
            // Instantiate the object at the spawn point's position and rotation
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Optionally, you can do additional configurations or modifications to the spawned object here
        }
        else
        {
            Debug.LogWarning("Object to spawn is not set!");
        }
    }
}
