using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour
{
    Rigidbody rb;

    public Transform controller;

    [Range(0.0f, 360.0f)]
    public float rotateBy = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move to the controller's position
        rb.MovePosition(controller.position);

        // Rotate based on the controller's rotation, plus extra rotation on the X axis
        Quaternion additionalRotation = Quaternion.Euler(rotateBy, 0, 0);
        rb.MoveRotation(controller.rotation * additionalRotation);
    }

}
