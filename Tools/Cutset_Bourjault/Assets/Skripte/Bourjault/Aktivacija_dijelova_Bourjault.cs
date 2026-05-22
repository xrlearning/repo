using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimpleToggleActivator : MonoBehaviour
{
    [System.Serializable]
    public class ToggleGroup
    {
        public Toggle toggle;                     
        public List<GameObject> controlledObjects; 
    }

    public List<ToggleGroup> toggleGroups = new List<ToggleGroup>();

    void Start()
    {
        foreach (var group in toggleGroups)
        {
            if (group.toggle != null)
            {
                group.toggle.onValueChanged.AddListener(delegate { UpdateObjects(group); });
                UpdateObjects(group);
            }
        }
    }

    void UpdateObjects(ToggleGroup group)
    {
        bool isOn = group.toggle.isOn;

        foreach (GameObject go in group.controlledObjects)
        {
            if (go != null)
                go.SetActive(isOn);
        }
    }
}
