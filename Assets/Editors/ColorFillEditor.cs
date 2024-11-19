using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorFillManager))]
public class ColorFillEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ColorFillManager colorFillManager = (ColorFillManager)target;
        colorFillManager.gridSize = EditorGUILayout.IntSlider("Grid Size", colorFillManager.gridSize, 0, 11);

        // grid 배열 크기 맞추기
        if (colorFillManager.grid == null || colorFillManager.grid.GetLength(0) != colorFillManager.gridSize || colorFillManager.grid.GetLength(1) != colorFillManager.gridSize)
        {
            colorFillManager.grid = new bool[colorFillManager.gridSize, colorFillManager.gridSize];  // grid 크기 맞추기
        }

        // PlayerPrefs에서 grid 데이터 로드 (Editor에서 유지되는 데이터)
        colorFillManager.LoadGridData();

        // 2D 배열을 그리드처럼 표시
        for (int y = 0; y < colorFillManager.gridSize; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < colorFillManager.gridSize; x++)
            {
                // 각 칸에 대해 Toggle을 표시
                colorFillManager.grid[x, y] = EditorGUILayout.Toggle(colorFillManager.grid[x, y]);
            }
            EditorGUILayout.EndHorizontal();
        }

        // 그리드 수정 후 Prefab 인스턴스화
        if (GUI.changed)
        {
            colorFillManager.SaveGridData();  // 데이터를 PlayerPrefs에 저장
        }

        // 기본 Inspector 항목 표시
        DrawDefaultInspector();
    }
}
