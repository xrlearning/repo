using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketToggleController : MonoBehaviour
{
    [System.Serializable]
    public class SocketTogglePair
    {
        public XRSocketInteractor Socket;
        public Toggle ToggleToActivate;
    }

    public List<SocketTogglePair> SocketTogglePairs;

    [Header("Zavrsni uslovi")]
    public List<Toggle> RequiredToggles;
    public Button CompletionButton;
    public List<GameObject> CompletionObjects;

    [Header("Dugme za zamenu objekata")]
    public Button SwitchButton;
    public List<GameObject> ObjectsToEnable;
    public List<GameObject> ObjectsToDisable;

    void Start()
    {
        foreach (var pair in SocketTogglePairs)
        {
            pair.Socket.selectEntered.AddListener((args) => OnSocketOccupied(pair));
        }

        if (CompletionButton != null)
            CompletionButton.gameObject.SetActive(false);
        
        if (CompletionObjects != null)
        {
            foreach (var obj in CompletionObjects)
                if (obj != null) obj.SetActive(false);
        }

        if (SwitchButton != null)
            SwitchButton.onClick.AddListener(SwitchObjects);
    }

    void OnSocketOccupied(SocketTogglePair pair)
    {
        if (pair.ToggleToActivate != null)
        {
            pair.ToggleToActivate.isOn = true;
            CheckCompletion();
        }
    }

    void CheckCompletion()
    {
        foreach (var toggle in RequiredToggles)
        {
            if (!toggle.isOn)
                return;
        }

        if (CompletionButton != null)
            CompletionButton.gameObject.SetActive(true);

        if (CompletionObjects != null)
        {
            foreach (var obj in CompletionObjects)
                if (obj != null) obj.SetActive(true);
        }
    }

    void SwitchObjects()
    {
        foreach (var go in ObjectsToDisable)
            if (go != null) go.SetActive(false);

        foreach (var go in ObjectsToEnable)
            if (go != null) go.SetActive(true);
    }
}
