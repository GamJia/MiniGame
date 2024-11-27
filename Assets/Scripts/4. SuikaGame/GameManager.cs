using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuikaGame;
using TMPro;   
using UnityEngine.UI;

namespace SuikaGame
{   
    public class GameManager : MonoBehaviour
    {

        [System.Serializable]
        public class Fruits
        {
            public GameObject prefab; // 과일 프리팹
            public int score; // 점수
        }
        [SerializeField] private List<Fruits> fruits; // 랜덤으로 생성할 과일 프리팹 리스트
        [SerializeField] private GameObject guide;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button restartButton; 
        private GameObject fruit; // 현재 드래그 중인 과일
        private int score=0;


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
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(RestartGame);
            }

            gameOver.SetActive(false);
            guide.SetActive(false);

            CreateFruit();
        }

        void Update()
        {
            if (gameOver.activeSelf)
            {
                return;
            }


            if (Input.GetMouseButton(0)) // 마우스 버튼을 누르고 있을 때
            {
                DragFruit();
            }

            if (Input.GetMouseButtonUp(0)) // 마우스 버튼을 뗄 때
            {
                DropFruit();
            }
        }

        void RestartGame()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            gameOver.SetActive(false);
            guide.SetActive(false);

            score=0;
            scoreText.text="0";

            fruit=null;
            
            StartCoroutine(DelayFruit(0.3));
        }

        void CreateFruit()
        {
            // 과일 리스트에서 랜덤으로 선택
            int randomIndex = Random.Range(0, 4);
            fruit = Instantiate(fruits[randomIndex].prefab,transform.position, Quaternion.identity, this.transform);
            fruit.name = fruits[randomIndex].prefab.name;

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

                if(!guide.activeSelf)
                {
                    guide.SetActive(true);
                }

                guide.transform.position=new Vector3(mousePos.x, guide.transform.position.y, guide.transform.position.z);
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

                StartCoroutine(DelayFruit(1));

                if(guide.activeSelf)
                {
                    guide.SetActive(false);
                }


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
            for (int i = 0; i < fruits.Count; i++)
            {
                if (fruits[i].prefab.name == first.gameObject.name)
                {
                    index = i;
                    break;
                } 
            }

            // 다음 레벨 과일 생성
            if (index == -1 || index + 1 >= fruits.Count)
            {
                return;
            }

            Vector3 spawnPosition = (first.transform.position + second.transform.position) / 2;
            GameObject newFruit = Instantiate(fruits[index+1].prefab, spawnPosition, Quaternion.identity,this.transform);

            UpdateScore(fruits[index+1].score);

            // 생성된 과일의 이름 설정
            newFruit.name = fruits[index+1].prefab.name;
        }

        void UpdateScore(int add)
        {
            score+=add;
            scoreText.text=score.ToString();
        }


        IEnumerator DelayFruit(double time)
        {
            yield return new WaitForSeconds((float)time);  // double을 float로 변환
            CreateFruit();
        }

    }
    

}

