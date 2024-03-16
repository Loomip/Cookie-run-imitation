using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestroy<GameManager>
{
    // 점수 스코어
    private TextMeshProUGUI scoreText;

    // 점수 저장
    private int score;

    // Plyaer 객체 참조
     private PlayerController playerController;

    // CamaraFollow 객체 참조
    private CameraFollow cameraFollow;

    // GameOver 텍스트
    private TextMeshProUGUI gameoverText;

    public void IncreaseScore(int amount)
    {
        score += amount;

        // 점수를 PlayerPrefs에 저장
        PlayerPrefs.SetInt("Score", score);

        // 점수 표시 갱신
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // 점수 표시 갱신
        string Score = score.ToString("D10");
        Score = string.Format("{0:#,###}", int.Parse(Score));
        scoreText.text = Score;
    }

    private void GameOver()
    {
        gameoverText.gameObject.SetActive(true);

        Time.timeScale = 0f;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Title");

            //죽엇으니 점수 초기화
            PlayerPrefs.SetInt("Score", 0);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // GameManager 인스턴스가 null인 경우 메서드 종료
        if (this == null)
        {
            return;
        }

        // 타이틀 씬인 경우 GameManager 객체 파괴
        if (scene.name == "Title")
        {
            Destroy(gameObject);
            return;
        }

        playerController = FindObjectOfType<PlayerController>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        scoreText = GameObject.Find("Txt_Score").GetComponent<TextMeshProUGUI>();
        gameoverText = GameObject.Find("Txt_GameOver").GetComponent<TextMeshProUGUI>();

        // GameOver 텍스트 비활성화
        gameoverText.gameObject.SetActive(false);

        // 점수 표시 갱신
        UpdateScoreText();
    }

    protected override void DoAwake()
    {
        PlayerPrefs.SetInt("Score", 0);

        playerController = FindObjectOfType<PlayerController>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        scoreText = GameObject.Find("Txt_Score").GetComponent<TextMeshProUGUI>();
        gameoverText = GameObject.Find("Txt_GameOver").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // PlayerPrefs에서 점수 불러오기
        score = PlayerPrefs.GetInt("Score", 0);

        // 점수 표시 갱신
        UpdateScoreText();

        // GameOver 텍스트 비활성화
        gameoverText.gameObject.SetActive(false);

        // 씬이 로드될 때마다 호출되는 이벤트에 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void Update()
    {
        // Player가 카메라 밖으로 나가면 게임 종료

        // 플레이어가 카메라의 왼쪽 경계를 넘어갓다면
        if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Left && playerController.transform.position.x < cameraFollow.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            GameOver();
        }
        // 플레이어가 카메라의 오른쪽 경계를 넘어갓다면
        else if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Right && playerController.transform.position.x > cameraFollow.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            GameOver();
        }
    }
}
