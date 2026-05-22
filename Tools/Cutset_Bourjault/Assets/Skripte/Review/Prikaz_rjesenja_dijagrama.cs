using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    [Header("Dugme koje aktivira objekte")]
    public Button activationButton;

    [Header("Objekti koji se aktiviraju")]
    public List<GameObject> objectsToActivate;

    void Start()
    {
        if (activationButton != null)
        {
            activationButton.onClick.AddListener(ActivateObjects);
        }
        else
        {
            Debug.LogWarning("Nije dodeljeno dugme za aktivaciju!");
        }
    }

    void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
