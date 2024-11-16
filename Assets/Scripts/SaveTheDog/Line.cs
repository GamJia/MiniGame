using System.Collections.Generic;
using UnityEngine;
using SaveTheDog;

namespace SaveTheDog
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private LayerMask backgroundLayer; // 백그라운드 레이어 설정
        private LineRenderer lineRenderer;
        private EdgeCollider2D edgeCollider;
        private List<Vector2> linePoints = new List<Vector2>();
        private bool isDrawing = false; // 초기값은 true로 설정하여 첫 그림은 허용

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        void Update()
        {
            if (isDrawing) return; 

            if (Input.GetMouseButton(0)) 
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Raycast로 마우스 위치가 backgroundLayer에 속하는지 확인
                if (IsMouseOnBackground(mousePos))
                {
                    return;
                }

                if (linePoints.Count == 0 || Vector2.Distance(linePoints[^1], mousePos) > 0.1f)
                {
                    linePoints.Add(mousePos);
                    UpdateLineRenderer();
                }
            }

            if (Input.GetMouseButtonUp(0)) 
            {
                isDrawing = true; 
                UpdateCollider();
            }
        }

        private void UpdateLineRenderer()
        {
            lineRenderer.positionCount = linePoints.Count;
            for (int i = 0; i < linePoints.Count; i++)
            {
                lineRenderer.SetPosition(i, linePoints[i]);
            }
        }

        private void UpdateCollider()
        {
            if (!edgeCollider)
            {
                edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
                edgeCollider.edgeRadius = 0.05f;
            }

            List<Vector2> edgePoints = new List<Vector2>();
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                Vector3 pos = lineRenderer.GetPosition(i);
                edgePoints.Add(new Vector2(pos.x, pos.y)); // 2D 좌표로 변환
            }
            edgeCollider.SetPoints(edgePoints);

            if (!gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.AddComponent<Rigidbody2D>();
            }
        }

        private bool IsMouseOnBackground(Vector2 mousePos)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0, backgroundLayer);
            return hit.collider != null; 
        }
    }

}
