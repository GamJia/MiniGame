using System.Collections;
using UnityEngine;

namespace SaveTheDog
{
    [AddComponentMenu("Save The Dog/Bee")]
    public class Bee : MonoBehaviour
    {
        [SerializeField] private float speed = 1f; // 이동 속도
        [SerializeField] private Vector3 targetPosition; // 목표 위치
        private float originalSpeed; // 원래 속도 저장

        private void Start()
        {
            // 초기 속도 저장
            originalSpeed = speed;
        }

        private void Update()
        {
            // 목표 위치로 이동
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            // 현재 위치에서 목표 위치로 일정 속도로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            // 충돌 발생 시 속도를 0으로 설정
            StartCoroutine(TemporaryStop());
        }

        private IEnumerator TemporaryStop()
        {
            speed = 0f;

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // 원래 속도로 복구
            speed = originalSpeed;
        }
    }
}
