using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    // Reference to the BoxCollider that defines the spawn area
    private BoxCollider box;

    // Called before Start() - initializes the BoxCollider reference
    private void Awake()
    {
        // Get the BoxCollider component attached to this GameObject
        box = GetComponent<BoxCollider>();
    }

    // Returns a random point within the bounds of the BoxCollider
    public Vector3 GetRandomPointInside()
    {
        // Get the world-space bounds of the BoxCollider
        Bounds bounds = box.bounds;

        // Randomly pick a point within the bounds on X, Y, and Z axes
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        // Return the randomly generated point
        return new Vector3(x, y, z);
    }

}
