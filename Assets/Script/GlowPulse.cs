using UnityEngine;

public class GlowPulse : MonoBehaviour
{
    public Color emissionColor = Color.cyan; // Colore base del glow
    public float pulseSpeed = 10f;            // Quanto velocemente pulsa
    public float minIntensity = 0.5f;        // Intensità minima del glow
    public float maxIntensity = 1.5f;        // Intensità massima del glow

    private Material mat;
    private float emissionIntensity;

    void Start()
    {
        // Ottieni il materiale (assicurati che l’oggetto abbia un Renderer)
        mat = GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", emissionColor * 0); // to set the initial emission to 0
    }

    void Update()
    {
        // Calcolo un valore oscillante nel tempo
        emissionIntensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);

        // Applica l'intensità all'Emission
        mat.SetColor("_EmissionColor", emissionColor * emissionIntensity);
    }
}
