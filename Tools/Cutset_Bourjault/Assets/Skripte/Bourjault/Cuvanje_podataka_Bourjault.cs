using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TableDataSaver : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public GameObject[] baseColumns;
    }

    [System.Serializable]
    public class Table
    {
        public string tableName;
        public TMP_InputField fileNameInput;
        public Button saveButton;
        public List<Level> levels = new List<Level>();
    }

    public List<Table> tables = new List<Table>();

    private string saveDirectory = Path.Combine(Application.dataPath, "Sacuvane_tabele_Bourjault");

    private void Start()
    {
        foreach (var table in tables)
        {
            table.saveButton.onClick.AddListener(() => SaveTableToFile(table));
        }

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    void SaveTableToFile(Table table)
    {
        string fileName = table.fileNameInput.text.Trim();
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogWarning("Naziv fajla je prazan.");
            return;
        }

        List<string> rows = new List<string>();
        int maxRowCount = GetMaxRowCount(table);

        for (int row = 0; row < maxRowCount; row++)
        {
            string line = "";

            for (int levelIndex = 0; levelIndex < table.levels.Count; levelIndex++)
            {
                Level level = table.levels[levelIndex];

                string a = GetInputFieldValue(level.baseColumns[0], row);
                string b = GetInputFieldValue(level.baseColumns[1], row);
                string c = GetInputFieldValue(level.baseColumns[2], row);

                line += $"{a};{b}\t{c}";

                if (levelIndex < table.levels.Count - 1)
                    line += "\t\t";
            }

            rows.Add(line);
        }

        string path = Path.Combine(saveDirectory, fileName + ".txt");
        File.WriteAllLines(path, rows);
        Debug.Log($"Sacuvano u fajl: {path}");
    }

    string GetInputFieldValue(GameObject columnGO, int index)
    {
        TMP_InputField[] inputs = columnGO.GetComponentsInChildren<TMP_InputField>(true);
        if (index >= inputs.Length)
            return "-";

        string text = inputs[index].text.Trim();
        return string.IsNullOrEmpty(text) ? "-" : text;
    }

    int GetMaxRowCount(Table table)
    {
        int max = 0;
        foreach (var level in table.levels)
        {
            foreach (var columnGO in level.baseColumns)
            {
                TMP_InputField[] inputs = columnGO.GetComponentsInChildren<TMP_InputField>(true);
                if (inputs.Length > max)
                    max = inputs.Length;
            }
        }
        return max;
    }
}
