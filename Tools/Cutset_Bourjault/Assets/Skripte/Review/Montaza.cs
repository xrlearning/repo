using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketActivationController : MonoBehaviour
{
    [Header("Pocetna deaktivacija")]
    public List<GameObject> initiallyDisabledParts;

    [System.Serializable]
    public class SocketGroup
    {
        public XRSocketInteractor socket;
        public List<GameObject> partsToActivate;
        public List<GameObject> partsToDeactivate;
    }

    [Header("Socket konfiguracija")]
    public List<SocketGroup> socketGroups = new List<SocketGroup>();

    void Start()
    {
        foreach (GameObject part in initiallyDisabledParts)
        {
            if (part != null)
                part.SetActive(false);
        }

        foreach (SocketGroup group in socketGroups)
        {
            if (group.socket != null)
                group.socket.selectEntered.AddListener((args) => OnSocketActivated(group));
        }
    }

    void OnSocketActivated(SocketGroup group)
    {
        foreach (GameObject part in group.partsToActivate)
        {
            if (part != null)
                part.SetActive(true);
        }

        foreach (GameObject part in group.partsToDeactivate)
        {
            if (part != null)
                part.SetActive(false);
        }
    }
}
