using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupManager : MonoBehaviour
{
    [System.Serializable]
    public class ToggleLineGroup
    {
        public Toggle Toggle;
        public List<GameObject> ControlledObjects; 
    }

    [Header("Grupa 'i'")]
    public List<ToggleLineGroup> iGroup;

    [Header("Grupa 'B'")]
    public List<ToggleLineGroup> BGroup;

    private Dictionary<GameObject, int> activeReferences = new Dictionary<GameObject, int>();
    private List<GameObject> temporarilyActivatedBObjects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < iGroup.Count; i++)
        {
            int index = i;
            iGroup[i].Toggle.onValueChanged.AddListener((isOn) => HandleToggleChanged(iGroup, BGroup, index, isOn, true));
            BGroup[i].Toggle.onValueChanged.AddListener((isOn) =>
            {
                HandleToggleChanged(BGroup, iGroup, index, isOn, false);
                CheckAndApplySpecialBGroupLogic();
            });
        }
    }

    void HandleToggleChanged(List<ToggleLineGroup> activeGroup, List<ToggleLineGroup> oppositeGroup, int index, bool isOn, bool isIGroup)
    {
        var groupEntry = activeGroup[index];
        var oppositeEntry = oppositeGroup[index];

        if (isOn && oppositeEntry.Toggle.isOn)
            oppositeEntry.Toggle.isOn = false;

        if (isOn && isIGroup)
        {
            for (int j = 0; j < activeGroup.Count; j++)
            {
                if (j != index && activeGroup[j].Toggle.isOn)
                {
                    activeGroup[j].Toggle.isOn = false;
                }
            }
        }

        foreach (var obj in groupEntry.ControlledObjects)
        {
            if (isOn)
            {
                if (!activeReferences.ContainsKey(obj))
                    activeReferences[obj] = 0;
                activeReferences[obj]++;
                obj.SetActive(true);
            }
            else
            {
                if (activeReferences.ContainsKey(obj))
                {
                    activeReferences[obj]--;
                    if (activeReferences[obj] <= 0)
                    {
                        obj.SetActive(false);
                        activeReferences.Remove(obj);
                    }
                }
            }
        }
    }

    void CheckAndApplySpecialBGroupLogic()
    {
        int activeCount = 0;
        for (int i = 0; i < BGroup.Count; i++)
        {
            if (BGroup[i].Toggle.isOn)
                activeCount++;
        }

        foreach (var obj in temporarilyActivatedBObjects)
        {
            if (activeReferences.ContainsKey(obj))
            {
                activeReferences[obj]--;
                if (activeReferences[obj] <= 0)
                {
                    obj.SetActive(false);
                    activeReferences.Remove(obj);
                }
            }
        }
        temporarilyActivatedBObjects.Clear();


        if (activeCount == BGroup.Count - 1)
        {
            for (int i = 0; i < BGroup.Count; i++)
            {
                if (!BGroup[i].Toggle.isOn)
                {
                    foreach (var obj in BGroup[i].ControlledObjects)
                    {
                        if (!activeReferences.ContainsKey(obj))
                            activeReferences[obj] = 0;
                        activeReferences[obj]++;
                        obj.SetActive(true);
                        temporarilyActivatedBObjects.Add(obj);
                    }
                    break; 
                }
            }
        }
    }
}
