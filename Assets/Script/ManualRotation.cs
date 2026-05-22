using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualRotation : MonoBehaviour
{
    [Tooltip("Input shaft that the user turns manually")]
    public Transform inputShaft;

    [Tooltip("Output shaft, which rotates according to gear ratio and the input shaft's rotation")]
    public Transform outputShaft;

    [Tooltip("gear ratio (proportion between the rotations of the input and output shaft)")]
    public float gearRatio = 4;

    private float lastInputAngle;

    void Start()
    {
        // initializes the rotational angle
        lastInputAngle = inputShaft.localEulerAngles.z;
    }

    void Update()
    {
        // get current inputShaft angle on z axis
        float currentInputAngle = inputShaft.localEulerAngles.z;

        // calculates angular variation
        float deltaAngle = Mathf.DeltaAngle(lastInputAngle, currentInputAngle);

        // rotates output shaft on its z axis based on the gear ratio
        outputShaft.Rotate(0f, 0f, (deltaAngle / gearRatio), Space.Self);

        // updates rotational angle for the next frame
        lastInputAngle = currentInputAngle;
    }
}