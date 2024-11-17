using UnityEngine;

public class ColorFillManager : MonoBehaviour
{
    public GameObject enablePrefab;  // true일 때 인스턴스화할 Prefab
    public GameObject disablePrefab; // false일 때 인스턴스화할 Prefab

    public bool[,] grid;  // 그리드 배열 (Inspector에서 보이는 배열)

    public int gridSize = 5;   // 그리드 크기 (Inspector에서 설정 가능)
    public float gridSpacingX = 0.5f; // X 간격
    public float gridSpacingY = 0.5f; // Y 간격
    public Vector3 startPosition = new Vector3(-2, 4, 0); // 첫 번째 생성 위치

    void Awake()
    {
        // 그리드 크기가 변경되면 새로 할당
        if (grid == null || grid.GetLength(0) != gridSize || grid.GetLength(1) != gridSize)
        {
            grid = new bool[gridSize, gridSize];
        }

        LoadGridData();  // 데이터를 로드하는 함수 호출
    }

    void Start()
    {
        // 그리드를 순회하며 Prefab 인스턴스화
        LoadGridData();  // 게임 시작 시 Prefab 인스턴스화
        UpdateGridPrefabs();
    }

    // 그리드에 맞춰 Prefab 인스턴스화하는 함수
    public void UpdateGridPrefabs()
    {
        // 기존에 생성된 Prefab 삭제 (선택적, 필요 시)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // 그리드를 순회하며 Prefab 인스턴스화
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector3 position = startPosition + new Vector3(x * gridSpacingX, -y * gridSpacingY, 0);

                // 배열 값에 맞춰 Prefab 인스턴스화
                if (grid[x, y])
                {
                    Instantiate(enablePrefab, position, Quaternion.identity, this.transform);
                }
                else
                {
                    Instantiate(disablePrefab, position, Quaternion.identity, this.transform);
                }
            }
        }
    }

    // grid 배열을 PlayerPrefs에 저장하는 함수
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

    // 저장된 grid 데이터를 불러오는 함수
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
