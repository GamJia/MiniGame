using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    // LineRenderer와 EdgeCollider2D에 설정할 포인트 리스트
    private List<Vector2> linePoints = new List<Vector2>();

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        // EdgeCollider2D의 초기 설정 (충돌 감지 범위가 좁아지도록)
        edgeCollider.edgeRadius = 0.05f;
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // 마우스 포지션이 마지막 점과 충분히 떨어진 경우에만 추가
            if (linePoints.Count == 0 || Vector2.Distance(linePoints[linePoints.Count - 1], mousePos) > 0.1f)
            {
                linePoints.Add(mousePos);
                UpdateLine();
            }
        }
    }

    private void UpdateLine()
    {
        // LineRenderer의 점을 업데이트
        lineRenderer.positionCount = linePoints.Count;
        for (int i = 0; i < linePoints.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i]);
        }

        // EdgeCollider2D의 점을 업데이트
        edgeCollider.SetPoints(linePoints);
    }
}
