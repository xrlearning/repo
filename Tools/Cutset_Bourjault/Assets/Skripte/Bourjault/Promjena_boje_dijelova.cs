using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColorManager : MonoBehaviour
{
    [System.Serializable]
    public class ToggleColorGroup
    {
        public Toggle Toggle;
        public List<GameObject> ControlledObjects;
    }

    public List<ToggleColorGroup> toggleGroups;
    public Color activeColor = Color.red;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();

    void Start()
    {
        foreach (var group in toggleGroups)
        {
            foreach (var obj in group.ControlledObjects)
            {
                if (!originalMaterials.ContainsKey(obj))
                {
                    var renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        originalMaterials[obj] = renderer.materials;
                    }
                }
            }
        }

        foreach (var group in toggleGroups)
        {
            group.Toggle.onValueChanged.AddListener((_) => UpdateColors());
        }
    }

    void UpdateColors()
    {
        foreach (var pair in originalMaterials)
        {
            var renderer = pair.Key.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.materials = pair.Value;
            }
        }

        foreach (var group in toggleGroups)
        {
            if (group.Toggle.isOn)
            {
                foreach (var obj in group.ControlledObjects)
                {
                    var renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        Material[] newMats = new Material[renderer.materials.Length];
                        for (int i = 0; i < newMats.Length; i++)
                        {
                            newMats[i] = new Material(renderer.materials[i]);
                            newMats[i].color = activeColor;
                        }
                        renderer.materials = newMats;
                    }
                }
            }
        }
    }
}
