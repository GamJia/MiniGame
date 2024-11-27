using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SuikaGame
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownText; // 카운트다운을 표시할 TextMeshPro
        [SerializeField] private GameObject gameOver; // GameOver 화면

        private List<GameObject> fruits = new List<GameObject>(); // 트리거된 객체 리스트
        private Coroutine countdownCoroutine; // 카운트다운 코루틴
        private bool isCoroutine = false; // 코루틴 상태 확인용

        void Start()
        {
            countdownText.gameObject.SetActive(false); 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!fruits.Contains(other.gameObject))
            {
                fruits.Add(other.gameObject);

                // 리스트 길이가 1 이상이면 카운트다운 시작
                if (fruits.Count > 0 && !isCoroutine)
                {
                    countdownCoroutine = StartCoroutine(StartCountdown());
                    isCoroutine = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (fruits.Contains(other.gameObject))
            {
                fruits.Remove(other.gameObject);

                // 리스트가 비면 카운트다운 중지 및 초기화
                if (fruits.Count == 0)
                {
                    if (countdownCoroutine != null)
                    {
                        StopCoroutine(countdownCoroutine);
                        countdownCoroutine = null;
                        isCoroutine = false;
                    }
                    countdownText.gameObject.SetActive(false); // UI 비활성화
                }
            }
        }

        private IEnumerator StartCountdown()
        {
            yield return new WaitForSeconds(2f);
            
            float timeRemaining = 5f;
            countdownText.gameObject.SetActive(true);

            while (timeRemaining > 0)
            {
                countdownText.text = Mathf.Ceil(timeRemaining).ToString(); // 남은 시간을 UI에 표시
                timeRemaining -= Time.deltaTime;
                yield return null;
            }

            // 카운트다운 완료 시 GameOver UI 활성화
            gameOver.SetActive(true);
            countdownText.gameObject.SetActive(false); // UI 비활성화
        }

    
    }
}
