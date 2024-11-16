using UnityEngine;
using SaveTheDog;
using System.Collections;

namespace SaveTheDog
{
    public class LineManager : MonoBehaviour
    {
        [SerializeField] GameObject line; 
        [SerializeField] Animator animator;
        private Coroutine timerCoroutine; // 타이머 코루틴 변수

        public static LineManager Instance => instance;
        private static LineManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(line, Vector3.zero, Quaternion.identity, this.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                if (timerCoroutine==null)
                {
                    timerCoroutine = StartCoroutine(StartTimer());
                }
            }
        }

        private IEnumerator StartTimer()
        {
            float timerDuration = 5f;
            float timeRemaining = timerDuration;

            while (timeRemaining > 0f)
            {
                timeRemaining -= 1f;
                UIManager.Instance.UpdateTimerText(Mathf.FloorToInt(timeRemaining)); // UI 업데이트
                yield return new WaitForSeconds(1f); // 1초 기다리기
            }

            animator.SetBool("isClear", true); 
            timerCoroutine = null;
        }

        public void StopTimer()
        {
            if(timerCoroutine!=null)
            {
                StopCoroutine(timerCoroutine); // 타이머 멈추기
                timerCoroutine = null; // 타이머 코루틴을 null로 설정하여 재시작 가능하게 만듬
            }
        }


    }

}
