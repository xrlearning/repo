using UnityEngine;

public class SnapToClosestPosition_shaft
 : MonoBehaviour
{
    [SerializeField] private GameObject position1; // First snap position
    [SerializeField] private GameObject objectToMove; // Object to snap

    private float snapDistance = 0.1f; // Distance threshold for snapping

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
        if (objectToMove == null || position1 == null)
        {
            Debug.LogWarning("SnapToClosestPosition: Missing references in the inspector.");
            return;
        }

        // Calculate distances to each position
        float distanceToPos1 = Vector3.Distance(objectToMove.transform.position, position1.transform.position);

        // Find the closest position
        GameObject closestPosition = position1;

        // Snap the object to the closest positionù
        if (distanceToPos1 < snapDistance)
        {
            closestPosition = position1;
        }
        else
        {
            closestPosition = objectToMove;
            return;
        }

        objectToMove.transform.position = closestPosition.transform.position;
        objectToMove.transform.rotation = closestPosition.transform.rotation;
    }

}