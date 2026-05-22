using UnityEngine;
using UnityEngine.UI; // Per mostrare il timer su UI (opzionale)
using TMPro;
using System.Globalization;

public class VRTimerController : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI timerText; // UI Text per mostrare il timer (assegnalo in Inspector)

    [SerializeField] public TextMeshProUGUI actionsText; // UI Text per mostrare il timer (assegnalo in Inspector)

    [SerializeField] public TextMeshProUGUI piecesText; // UI Text per mostrare il timer (assegnalo in Inspector)

    [SerializeField] public MistakeCounter MistakeCounterRef;

    [SerializeField] public PieceCounter PieceCounterRef;

    public float elapsedTime = 0f;

    private bool timerRunning = false;

    public int PiecesNumber = 52; // Numero totale di pezzi, impostalo in base al tuo gioco

    // public int ActionsNumber = 59;

    // Metodo per avviare il timer, chiamalo quando premi il tasto di inizio
    public void StartTimer()
    {
        elapsedTime = 0f;
        timerRunning = true;
    }

    // Metodo per fermare il timer, chiamalo quando rilasci l'ultimo elemento
    public void StopTimer()
    {
        timerRunning = false;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerUI(elapsedTime);
    }

    private void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        actionsText.text = "ACTIONS: " + MistakeCounterRef.counter;

        piecesText.text = "PIECES: " + (PieceCounterRef.counter + 1) + "/" + PiecesNumber;
    }
}



