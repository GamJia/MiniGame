using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorFill
{
    [AddComponentMenu("Color Fill/Color Fill Player")]
    public class Player : MonoBehaviour
    {
        private float moveSpeed = 0f; 
        private Vector2 moveDirection; // 이동 방향
        private Vector2 startMousePosition; // 마우스 시작 위치
        private bool isMoving = false; // 이동 중인지 여부

        private void Update()
        {
            
            // 마우스 클릭 시 (드래그 시작)
            if (Input.GetMouseButtonDown(0)&&!isMoving)
            {
                startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                isMoving = true; 
                moveSpeed=7;
            }

            if (isMoving)
            {
                Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

                // X 또는 Y 방향으로만 이동 (좌우/상하)
                if (Mathf.Abs(currentMousePosition.x - startMousePosition.x) > Mathf.Abs(currentMousePosition.y - startMousePosition.y))
                {
                    // X 방향으로 이동 (왼쪽/오른쪽)
                    if (currentMousePosition.x > startMousePosition.x && moveDirection != Vector2.right)
                    {
                        moveDirection = Vector2.right; // 오른쪽으로 이동
                    }
                    else if (currentMousePosition.x < startMousePosition.x && moveDirection != Vector2.left)
                    {
                        moveDirection = Vector2.left; // 왼쪽으로 이동
                    }
                }
                else
                {
                    // Y 방향으로 이동 (위/아래)
                    if (currentMousePosition.y > startMousePosition.y && moveDirection != Vector2.up)
                    {
                        moveDirection = Vector2.up; // 위쪽으로 이동
                    }
                    else if (currentMousePosition.y < startMousePosition.y && moveDirection != Vector2.down)
                    {
                        moveDirection = Vector2.down; // 아래쪽으로 이동
                    }
                }

                // 계속 이동 (마우스를 떼도 계속 이동)
                
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

        // 충돌 시 이동 멈추기
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 충돌한 경우 이동 멈추기
            if ((moveDirection == Vector2.right && collision.transform.position.x > transform.position.x + 0.9f)
            || (moveDirection == Vector2.left && collision.transform.position.x+0.9f < transform.position.x)
            || (moveDirection == Vector2.up && collision.transform.position.y> transform.position.y+0.9f)
            || (moveDirection == Vector2.down && collision.transform.position.y+0.9f < transform.position.y))
            {
                isMoving = false; // 이동 멈추기
                moveSpeed = 0f;   // 이동 속도 0으로 설정
            }
            
        }
    }
}
