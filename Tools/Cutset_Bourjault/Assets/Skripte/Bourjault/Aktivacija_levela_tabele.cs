using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndependentToggleGroups : MonoBehaviour
{
    [System.Serializable]
    public class ToggleObjectGroup
    {
        public Toggle Toggle;
        public List<GameObject> ControlledObjects;
    }

    [System.Serializable]
    public class ToggleGroup
    {
        public string GroupName;
        public List<ToggleObjectGroup> ToggleGroups;
    }

    public List<ToggleGroup> ToggleGroupList;

    private bool isInternalChange = false;

    void Start()
    {
        foreach (var group in ToggleGroupList)
        {
            foreach (var togGroup in group.ToggleGroups)
            {
                Toggle localToggle = togGroup.Toggle;
                localToggle.onValueChanged.AddListener((isOn) => OnToggleChanged(group, togGroup, isOn));
            }

            EnsureOneToggleOn(group);
        }
    }

    void OnToggleChanged(ToggleGroup group, ToggleObjectGroup activeToggleGroup, bool isOn)
    {
        if (isInternalChange) return;

        if (isOn)
        {
            isInternalChange = true;

            foreach (var togGroup in group.ToggleGroups)
            {
                if (togGroup != activeToggleGroup)
                {
                    togGroup.Toggle.isOn = false;
                }
            }

            HashSet<GameObject> allObjectsInGroup = new HashSet<GameObject>();
            foreach (var togGroup in group.ToggleGroups)
            {
                foreach (var obj in togGroup.ControlledObjects)
                {
                    allObjectsInGroup.Add(obj);
                }
            }
            foreach (var obj in allObjectsInGroup)
            {
                obj.SetActive(false);
            }

            foreach (var obj in activeToggleGroup.ControlledObjects)
            {
                obj.SetActive(true);
            }

            isInternalChange = false;
        }
        else
        {
            if (!AnyToggleOnInGroup(group))
            {
                isInternalChange = true;
                activeToggleGroup.Toggle.isOn = true;
                isInternalChange = false;
            }
        }
    }

    bool AnyToggleOnInGroup(ToggleGroup group)
    {
        foreach (var tg in group.ToggleGroups)
        {
            if (tg.Toggle.isOn) return true;
        }
        return false;
    }

    void EnsureOneToggleOn(ToggleGroup group)
    {
        if (!AnyToggleOnInGroup(group) && group.ToggleGroups.Count > 0)
        {
            group.ToggleGroups[0].Toggle.isOn = true;
        }
    }
}
