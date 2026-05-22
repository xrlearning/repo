using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyManager : MonoBehaviour
{
    public Toggle[] SiToggles;
    public Toggle[] SjToggles;
    public GameObject[] originalParts;
    public GameObject ParentSj;
    public GameObject[] copiedParts;
    public GameObject[] copiedPhysicsColliders;
    public Button resetButton;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;
    private Vector3 parentSjInitialPosition;
    private Quaternion parentSjInitialRotation;

    void Start()
    {
        initialPositions = new Vector3[originalParts.Length];
        initialRotations = new Quaternion[originalParts.Length];

        for (int i = 0; i < originalParts.Length; i++)
        {
            initialPositions[i] = originalParts[i].transform.position;
            initialRotations[i] = originalParts[i].transform.rotation;

            int index = i;
            SiToggles[i].isOn = true;
            SiToggles[i].onValueChanged.AddListener(delegate { ToggleSi(index); });

            SjToggles[i].isOn = false;
            SjToggles[i].onValueChanged.AddListener(delegate { ToggleSj(index); });
        }

        parentSjInitialPosition = ParentSj.transform.position;
        parentSjInitialRotation = ParentSj.transform.rotation;

        resetButton.onClick.AddListener(ResetAssembly);
        ParentSj.SetActive(false);
        UpdateAssembly();
    }

    void ToggleSi(int index)
    {
        if (SiToggles[index].isOn)
        {
            SjToggles[index].isOn = false;
        }
        UpdateAssembly();
    }

    void ToggleSj(int index)
    {
        if (SjToggles[index].isOn)
        {
            SiToggles[index].isOn = false;
        }
        UpdateAssembly();
    }

    void UpdateAssembly()
    {
        bool anySjActive = false;

        for (int i = 0; i < originalParts.Length; i++)
        {
            if (SiToggles[i].isOn)
            {
                originalParts[i].SetActive(true);
                copiedParts[i].SetActive(false);
                copiedPhysicsColliders[i].SetActive(false);
                FreezeTransform(originalParts[i], true);
            }
            else if (SjToggles[i].isOn)
            {
                originalParts[i].SetActive(false);
                copiedParts[i].SetActive(true);
                copiedPhysicsColliders[i].SetActive(true);
                anySjActive = true;

            }
            else
            {
                originalParts[i].SetActive(false);
                copiedParts[i].SetActive(false);
                copiedPhysicsColliders[i].SetActive(false);
            }
        }

        ParentSj.SetActive(anySjActive);
    }

    void FreezeTransform(GameObject obj, bool freeze)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = freeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
        }
    }

    void ResetAssembly()
    {
        for (int i = 0; i < originalParts.Length; i++)
        {
            originalParts[i].transform.position = initialPositions[i];
            originalParts[i].transform.rotation = initialRotations[i];
            originalParts[i].SetActive(true);
            copiedParts[i].SetActive(false);
            copiedPhysicsColliders[i].SetActive(false);
            SiToggles[i].isOn = true;
            SjToggles[i].isOn = false;
            FreezeTransform(originalParts[i], true);
        }

        ParentSj.transform.position = parentSjInitialPosition;
        ParentSj.transform.rotation = parentSjInitialRotation;
        ParentSj.SetActive(false);
    }
}
