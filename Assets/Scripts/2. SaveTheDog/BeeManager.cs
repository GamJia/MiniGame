using System.Collections;
using UnityEngine;

namespace SaveTheDog
{
    [AddComponentMenu("Save The Dog/Bee Manager")]
    public class BeeManager : MonoBehaviour
    {
        [SerializeField] private int count; 
        [SerializeField] private GameObject bee; // 벌 프리팹

        public static BeeManager Instance => instance;
        private static BeeManager instance;

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

        public void StartSpawnBee()
        {
            StartCoroutine(SpawnBees());
        }

        private IEnumerator SpawnBees()
        {
            for (int i = 0; i < count; i++)
            {
                // BeeManager의 자식으로 벌 생성
                GameObject newBee = Instantiate(bee, transform.position, Quaternion.identity, this.transform);

                // 0.3초 대기
                yield return new WaitForSeconds(0.3f);
            }
        }

        public void DisableBee()
        {
            // BeeManager의 모든 자식 개수만큼 반복
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                // 자식의 Bee 컴포넌트를 찾아 enabled를 false로 설정
                Bee beeComponent = child.GetComponent<Bee>();
                if (beeComponent != null)
                {
                    beeComponent.enabled = false;
                }
            }
        }
    }
}
