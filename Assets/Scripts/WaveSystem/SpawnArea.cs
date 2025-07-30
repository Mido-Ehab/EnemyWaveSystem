using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    private BoxCollider box;
    private void Awake()
    {
        box = GetComponent<BoxCollider>();
    }

    public Vector3 GetRandomPointInside()
    {
        Bounds bounds = box.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }

}
