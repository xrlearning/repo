using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExclusiveToggleController : MonoBehaviour
{
    [System.Serializable]
    public class ToggleEntry
    {
        public Toggle toggle;
        public List<GameObject> linkedObjects;
    }

    public List<ToggleEntry> toggleEntries;

    private bool isInternalChange = false;

    void Start()
    {
        foreach (ToggleEntry entry in toggleEntries)
        {
            entry.toggle.onValueChanged.AddListener(delegate { OnToggleChanged(entry); });
        }

        EnsureAtLeastOneToggleActive();
    }

    void OnToggleChanged(ToggleEntry changedEntry)
    {
        if (isInternalChange) return;

        if (changedEntry.toggle.isOn)
        {
            isInternalChange = true;

            foreach (ToggleEntry entry in toggleEntries)
            {
                bool isThis = entry == changedEntry;
                entry.toggle.isOn = isThis;

                foreach (GameObject obj in entry.linkedObjects)
                {
                    obj.SetActive(isThis);
                }
            }

            isInternalChange = false;
        }
        else
        {

            if (!AnyOtherToggleOn())
            {
                isInternalChange = true;
                changedEntry.toggle.isOn = true;

                foreach (GameObject obj in changedEntry.linkedObjects)
                {
                    obj.SetActive(true);
                }

                isInternalChange = false;
            }
        }
    }

    bool AnyOtherToggleOn()
    {
        int activeCount = 0;
        foreach (ToggleEntry entry in toggleEntries)
        {
            if (entry.toggle.isOn) activeCount++;
        }
        return activeCount > 0;
    }

    void EnsureAtLeastOneToggleActive()
    {
        bool anyActive = false;
        foreach (ToggleEntry entry in toggleEntries)
        {
            if (entry.toggle.isOn)
            {
                anyActive = true;
                break;
            }
        }

        if (!anyActive && toggleEntries.Count > 0)
        {
            isInternalChange = true;
            toggleEntries[0].toggle.isOn = true;

            foreach (GameObject obj in toggleEntries[0].linkedObjects)
            {
                obj.SetActive(true);
            }

            isInternalChange = false;
        }
    }
}
