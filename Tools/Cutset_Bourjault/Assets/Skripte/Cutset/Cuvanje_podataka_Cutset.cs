using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SaveMultipleTables : MonoBehaviour
{
    public GameObject[] tables;
    public Button saveButton;
    public TMP_InputField fileNameInput;

    private string saveDirectory = Path.Combine(Application.dataPath, "Sacuvane_tabele_Cutset");

    void Start()
    {
        saveButton.onClick.AddListener(SaveToTXT);
    }

    void SaveToTXT()
    {
        if (string.IsNullOrEmpty(fileNameInput.text))
        {
            Debug.LogError("Unesi naziv fajla pre nego sto sacuvas!");
            return;
        }

        string savePath = Path.Combine(saveDirectory, fileNameInput.text + ".txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(savePath, false))
            {
                foreach (GameObject table in tables)
                {
                    List<string> allValues = new List<string>();

                    foreach (Transform rowTransform in table.transform)
                    {
                        TMP_InputField[] inputFields = rowTransform.GetComponentsInChildren<TMP_InputField>();

                        foreach (TMP_InputField inputField in inputFields)
                        {
                            string text = string.IsNullOrEmpty(inputField.text) ? "-" : inputField.text;
                            allValues.Add(text);
                        }
                    }

                    writer.WriteLine(string.Join("\t", allValues));
                }
            }

            Debug.Log("Sve tabele su uspesno sacuvane u: " + savePath);

            FormatTextFile(savePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Greska pri cuvanju: " + e.Message);
        }
    }

    void FormatTextFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string> formattedLines = new List<string>();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] values = line.Split('\t');
                if (values.Length != 30)
                {
                    Debug.LogError("Neispravan broj podataka u tabeli!");
                    continue;
                }

                string formattedTable = "";

                for (int i = 0; i < 20; i++)
                {
                    formattedTable += values[i] + "\t";
                    if ((i + 1) % 5 == 0) formattedTable = formattedTable.TrimEnd() + "\n";
                }

                for (int i = 20; i < 30; i += 2)
                {
                    formattedTable += values[i] + ";" + values[i + 1] + "\t";
                }

                formattedLines.Add(formattedTable.TrimEnd());
            }

            File.WriteAllText(filePath, string.Join("\n\n", formattedLines));

            Debug.Log("Fajl je uspesno formatiran u 5x5 oblik");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Greska pri formatiranju fajla: " + e.Message);
        }
    }
}
