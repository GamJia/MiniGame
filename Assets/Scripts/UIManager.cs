using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager 사용을 위해 추가

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject lobby; 
    [SerializeField] GameObject gameover; 
    public static UIManager Instance => instance;
    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // 다른 인스턴스가 있으면 새로 생성된 것을 삭제
        }
    }

    void Start()
    {
        
    }

    public void StartGame()
    {
        lobby.SetActive(false);
    }

    public void GameOver()
    {
        gameover.SetActive(true);
        Time.timeScale=0f;
    }

    public void ResetGame()
    {
        Scene currentScene = SceneManager.GetActiveScene(); // 현재 Scene 가져오기
        SceneManager.LoadScene(currentScene.name); // 현재 Scene 이름으로 다시 로드
    }
}
