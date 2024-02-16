using UnityEngine;

public class RandomDuplicator : MonoBehaviour
{
    public GameObject objectToDuplicate;
    public int numberOfDuplicates = 10;
    public float innerRadius = 5f;
    public float outerRadius = 10f;
    public float padding = 1f;
    public Vector2 scaleRange = new Vector2(0.5f, 2.0f);
    public Vector2 yPositionRange = new Vector2(0.5f, 2.0f);

    public Material[] blenderShapesPool; // Pool of materials representing blender shapes

    void Start()
    {
        DuplicateObjects();
    }

    void DuplicateObjects()
    {
        // Check if the object to duplicate is provided
        if (objectToDuplicate == null)
        {
            Debug.LogError("Object to duplicate is not assigned.");
            return;
        }

        // Check if there are materials in the pool
        if (blenderShapesPool == null || blenderShapesPool.Length == 0)
        {
            Debug.LogError("Blender shapes pool is not assigned or is empty.");
            return;
        }

        // Get the center point (script position)
        Vector3 centerPoint = transform.position;

        // Get the original rotation of the object
        Quaternion originalRotation = objectToDuplicate.transform.rotation;

        // Duplicate the object multiple times
        for (int i = 0; i < numberOfDuplicates; i++)
        {
            // Generate a random angle within the specified range
            float randomAngle = Random.Range(0f, 360f);

            // Convert polar coordinates to Cartesian coordinates
            float randomRadius = Random.Range(innerRadius + padding, outerRadius - padding);
            Vector3 randomPosition = PolarToCartesian(randomRadius, randomAngle);

            // Ensure the y position stays within the specified range
            randomPosition.y = Mathf.Clamp(randomPosition.y, yPositionRange.x, yPositionRange.y);

            // Generate a random scale within the specified range
            float randomScale = Random.Range(scaleRange.x, scaleRange.y);
            Vector3 randomScaleVector = new Vector3(randomScale, randomScale, randomScale);

            // Calculate the direction to the center point
            Vector3 directionToCenter = centerPoint - randomPosition;

            // Generate a specific rotation to face the center point, based on the original rotation
            Quaternion randomRotation = Quaternion.LookRotation(-directionToCenter) * originalRotation;

            // Instantiate a new copy of the object at the random position with random scale and rotation
            GameObject newObject = Instantiate(objectToDuplicate, randomPosition, randomRotation);

            // Apply the random scale to the new object
            newObject.transform.localScale = randomScaleVector;

            // Assign random materials from the pool to each blender shape
            AssignRandomMaterials(newObject);

            // Optionally, you can modify the newObject further if needed
            // newObject.GetComponent<YourComponent>().YourMethod();
        }
    }

    // Convert polar coordinates to Cartesian coordinates
    Vector3 PolarToCartesian(float radius, float angle)
    {
        float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = Random.Range(yPositionRange.x, yPositionRange.y); // Y position within the specified range
        float z = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        return new Vector3(x, y, z) + transform.position;
    }

    // Assign random materials from the pool to each blender shape
    void AssignRandomMaterials(GameObject spawnedObject)
    {
        Renderer[] renderers = spawnedObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Check if the renderer has a valid material
            if (renderer != null)
            {
                // Assign a random material from the pool
                Material randomMaterial = blenderShapesPool[Random.Range(0, blenderShapesPool.Length)];
                renderer.material = randomMaterial;
            }
        }
    }
}
