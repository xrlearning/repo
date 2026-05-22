using UnityEngine;

public class PerformanceControl : MonoBehaviour
{
    [SerializeField] private PieceCounter PieceCounterRef;

    // script to avoid 
    void Start()
    {
        PieceCounterRef.counter++;

    }

    public void DecrementPieceCounter()
    {
        PieceCounterRef.counter--;
    }
}