using Oculus.Interaction; // Ensure this is included for access to the SnapInteractable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCounter : MonoBehaviour
{
    public int counter = 0;

    public void IncrementCounter()
    {
        counter++;
        Debug.Log("Piece snapped! Total pieces: " + counter);
    }
}
