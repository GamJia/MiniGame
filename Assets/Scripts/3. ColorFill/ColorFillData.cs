using UnityEngine;

public class ColorFillData : MonoBehaviour
{
    [Range(1, 10)] // 최소 1, 최대 10까지 사이즈 설정 가능
    public int gridSize = 3;
    public bool[,] grid;

    private void OnValidate()
    {
        // gridSize에 맞춰 bool 배열의 크기 조정
        grid = new bool[gridSize, gridSize];
    }
}
