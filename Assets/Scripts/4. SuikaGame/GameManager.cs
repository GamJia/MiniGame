using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuikaGame;

namespace SuikaGame
{   
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> fruitPrefabs; // 랜덤으로 생성할 과일 프리팹 리스트
        private GameObject fruit; // 현재 드래그 중인 과일

        public static GameManager Instance => instance;
        private static GameManager instance;

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



        void Start()
        {
            CreateFruit();
        }

        void Update()
        {
    
            if (Input.GetMouseButton(0)) // 마우스 버튼을 누르고 있을 때
            {
                DragFruit();
            }

            if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 뗄 때
            {
                DropFruit();
            }
        }

        void CreateFruit()
        {
            // 과일 리스트에서 랜덤으로 선택
            int randomIndex = Random.Range(0, 4);
            fruit = Instantiate(fruitPrefabs[randomIndex]);
            fruit.name = fruitPrefabs[randomIndex].name;

            // 과일 초기 위치 설정 (화면 중앙 위)
            fruit.transform.position = new Vector3(0f, 4f, 0f);

            // Rigidbody2D를 Kinematic으로 설정
            Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }


        void DragFruit()
        {
            if (fruit != null)
            {
                // 마우스 위치를 가져와 X 좌표만 업데이트
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f; // 2D 환경을 가정하여 Z 좌표를 0으로 고정
                mousePos.x = Mathf.Clamp(mousePos.x, -3f, 3f); // X 좌표 -3 ~ 3으로 제한

                // 과일 위치 업데이트
                fruit.transform.position = new Vector3(mousePos.x, fruit.transform.position.y, fruit.transform.position.z);
            }
        }

        void DropFruit()
        {
            if (fruit != null)
            {
                // Rigidbody2D를 Dynamic으로 변경
                Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }

                // 현재 과일 초기화
                fruit = null;

                StartCoroutine(DelayFruit());

            }
        }

        public void DestroyFruit(GameObject first, GameObject second)
        {
            if (!first.GetComponent<Fruit>().isMerged&&!second.GetComponent<Fruit>().isMerged)
            {
                return;
            }

            first.GetComponent<Animator>().SetTrigger("isDisappear");
            second.GetComponent<Animator>().SetTrigger("isDisappear");

            int index = -1;
            for (int i = 0; i < fruitPrefabs.Count; i++)
            {
                if (fruitPrefabs[i].name == first.gameObject.name)
                {
                    index = i;
                    break;
                }
            }

            // 다음 레벨 과일 생성
            if (index == -1 || index + 1 >= fruitPrefabs.Count)
            {
                return;
            }

            Vector3 spawnPosition = (first.transform.position + second.transform.position) / 2;
            GameObject newFruit = Instantiate(fruitPrefabs[index + 1], spawnPosition, Quaternion.identity);

            // 생성된 과일의 이름 설정
            newFruit.name = fruitPrefabs[index + 1].name;
        }


        IEnumerator DelayFruit()
        {
            yield return new WaitForSeconds(1f); // 1초 대기
            CreateFruit();
        }
    }
    

}

