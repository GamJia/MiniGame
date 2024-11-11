using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // TextMeshPro 사용을 위해 추가
using UnityEngine.SceneManagement; // SceneManager 사용을 위해 추가

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameover;
    [SerializeField] TextMeshProUGUI[] currentScore;
    [SerializeField] TextMeshProUGUI bestScoreText;

    private int score = 0;
    private int bestScore = 0;
    public static UIManager Instance => instance;
    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Singleton이 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 다른 인스턴스가 있으면 새로 생성된 것을 삭제
        }

        LoadBestScore();
    }

    public void UpdateScore()
    {
        score++;
        for (int i = 0; i < currentScore.Length; i++)
        {
            currentScore[i].text = score.ToString();
        }
    }

    public void GameOver()
    {
        gameover.SetActive(true);
        Time.timeScale = 0f;

        if (score > bestScore)
        {
            bestScore = score;
            bestScoreText.text = bestScore.ToString();
            SaveBestScore();
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene(); // 현재 Scene 가져오기
        SceneManager.LoadScene(currentScene.name); // 현재 Scene 이름으로 다시 로드
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    private void LoadBestScore()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreText.text = bestScore.ToString();
        }
    }
}
