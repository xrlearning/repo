using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LineActivator : MonoBehaviour
{
    [System.Serializable]
    public class PointData
    {
        public Toggle SiToggle;
        public Toggle SjToggle;
        public GameObject Point;
        public Image PointImage;
    }

    [System.Serializable]
    public class LineData
    {
        public int PointIndex1;
        public int PointIndex2;
        public GameObject Line;
    }

    public List<PointData> Points;
    public List<LineData> Lines;
    public Color SjActiveColor = Color.red;
    public Color DefaultColor = Color.white;

    void Start()
    {
        foreach (PointData point in Points)
        {
            point.SiToggle.onValueChanged.AddListener(delegate { UpdatePointsAndLines(); });
            point.SjToggle.onValueChanged.AddListener(delegate { UpdatePointsAndLines(); });
        }

        UpdatePointsAndLines();
    }

    void UpdatePointsAndLines()
    {
        foreach (PointData point in Points)
        {
            bool isActive = point.SiToggle.isOn || point.SjToggle.isOn;
            point.Point.SetActive(isActive);

            if (point.PointImage != null)
            {
                point.PointImage.color = point.SjToggle.isOn ? SjActiveColor : DefaultColor;
            }
        }

        foreach (LineData line in Lines)
        {
            bool point1Active = Points[line.PointIndex1].Point.activeSelf;
            bool point2Active = Points[line.PointIndex2].Point.activeSelf;

            line.Line.SetActive(point1Active && point2Active);
        }
    }
}
