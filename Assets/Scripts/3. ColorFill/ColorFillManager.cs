using UnityEngine;

public class ColorFillManager : MonoBehaviour
{
    public GameObject enablePrefab;  // true일 때 인스턴스화할 Prefab
    public GameObject disablePrefab; // false일 때 인스턴스화할 Prefab

    public bool[,] grid;  // 그리드 배열

    public int gridSize = 5;   // 그리드 크기
    public float gridScale = 1.2f;

    void Awake()
    {
        // 그리드 크기 확인 및 초기화
        if (grid == null || grid.GetLength(0) != gridSize || grid.GetLength(1) != gridSize)
        {
            grid = new bool[gridSize, gridSize];
        }

        LoadGridData();  // 데이터를 로드
    }

    void Start()
    {
        UpdateGridPrefabs(); // 게임 시작 시 Prefab 생성
    }

    // 그리드에 맞춰 Prefab 인스턴스화
    public void UpdateGridPrefabs()
    {
        // 기존에 생성된 Prefab 삭제
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 그리드를 순회하며 Prefab 인스턴스화
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // Prefab 선택
                GameObject prefab = grid[x, y] ? enablePrefab : disablePrefab;

                // Prefab 인스턴스화
                GameObject tile = Instantiate(prefab, transform);

                // 위치 설정
                float positionX = x * gridScale;
                float positionY = y * -gridScale; // y는 음수 방향으로 이동
                tile.transform.localPosition = new Vector2(positionX, positionY);
            }
        }

        // 그리드 전체 중심으로 위치 조정
        float gridW = gridSize * gridScale;
        float gridH = gridSize * gridScale;
        transform.position = new Vector2(-gridW / 2 + gridScale / 2, gridH / 2 - gridScale / 2);
    }

    // grid 배열을 PlayerPrefs에 저장
    public void SaveGridData()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // grid 배열 값을 0 또는 1로 저장
                PlayerPrefs.SetInt($"grid_{x}_{y}", grid[x, y] ? 1 : 0);
            }
        }

        // PlayerPrefs 저장
        PlayerPrefs.Save();
    }

    // 저장된 grid 데이터를 불러오기
    public void LoadGridData()
    {
        grid = new bool[gridSize, gridSize];  // 데이터 초기화

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                // PlayerPrefs에서 값 읽어오기
                int value = PlayerPrefs.GetInt($"grid_{x}_{y}", 0); // 기본값 0
                grid[x, y] = value == 1;
            }
        }
    }
}
