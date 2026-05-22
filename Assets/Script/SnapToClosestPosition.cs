using UnityEngine;

public class SnapToClosestPosition : MonoBehaviour
{
    [SerializeField] private GameObject position1; // First snap position
    [SerializeField] private GameObject position2; // Second snap position
    [SerializeField] private GameObject position3; // Third snap position
    [SerializeField] private GameObject objectToMove; // Object to snap

    private bool isGrabbed = false; // Tracks if the object is grabbed

    // Update is called once per frame
    void Update()
    {
        SnapToClosest();
    }

    // Call this method to set the grabbed state (e.g., from another script)
    /*public void SetGrabbedState(bool grabbed)
    {
        isGrabbed = grabbed;
    }*/

    private void SnapToClosest()
    {
        if (objectToMove == null || position1 == null || position2 == null || position3 == null)
        {
            Debug.LogWarning("SnapToClosestPosition: Missing references in the inspector.");
            return;
        }

        // Calculate distances to each position
        float distanceToPos1 = Vector3.Distance(objectToMove.transform.position, position1.transform.position);
        float distanceToPos2 = Vector3.Distance(objectToMove.transform.position, position2.transform.position);
        float distanceToPos3 = Vector3.Distance(objectToMove.transform.position, position3.transform.position);

        // Find the closest position
        GameObject closestPosition = position1;
        float closestDistance = distanceToPos1;

        if (distanceToPos2 < closestDistance)
        {
            closestPosition = position2;
            closestDistance = distanceToPos2;
        }

        if (distanceToPos3 < closestDistance)
        {
            closestPosition = position3;
        }

        // Snap the object to the closest position
        objectToMove.transform.position = closestPosition.transform.position;
        objectToMove.transform.rotation = closestPosition.transform.rotation;
    }
}