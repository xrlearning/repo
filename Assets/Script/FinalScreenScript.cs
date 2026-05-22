using UnityEngine;
using TMPro;

public class FinalScreenScript : MonoBehaviour
{
    [SerializeField] private VRTimerController timeController;

    [SerializeField] public TextMeshProUGUI timerText; // UI Text per mostrare il timer (assegnalo in Inspector)

    [SerializeField] public TextMeshProUGUI actionsText; // UI Text per mostrare il timer (assegnalo in Inspector)

    [SerializeField] public TextMeshProUGUI piecesText; // UI Text per mostrare il timer (assegnalo in Inspector)

    [SerializeField] public MistakeCounter MistakeCounterRef;

    [SerializeField] public PieceCounter PieceCounterRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FinalScreen()
    {
        float time = timeController.elapsedTime; // Salva il tempo trascorso prima di fermare il timer

        timeController.StopTimer();

        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);

        timerText.text = "FINAL TIME: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        actionsText.text = "TOTAL NUMBER OF ACTIONS: " + MistakeCounterRef.counter;

        // piecesText.text = "NUMBER  OF ERRORS: " + (MistakeCounterRef.counter - timeController.ActionsNumber);  // unproper use of the pieces counter, but saves memory
        piecesText.text = "";

    }
}
