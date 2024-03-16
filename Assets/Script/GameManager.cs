using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonDontDestroy<GameManager>
{
    // ���� ���ھ�
    private TextMeshProUGUI scoreText;

    // ���� ����
    private int score;

    // Plyaer ��ü ����
     private PlayerController playerController;

    // CamaraFollow ��ü ����
    private CameraFollow cameraFollow;

    // GameOver �ؽ�Ʈ
    private TextMeshProUGUI gameoverText;

    public void IncreaseScore(int amount)
    {
        score += amount;

        // ������ PlayerPrefs�� ����
        PlayerPrefs.SetInt("Score", score);

        // ���� ǥ�� ����
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // ���� ǥ�� ����
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

            //�׾����� ���� �ʱ�ȭ
            PlayerPrefs.SetInt("Score", 0);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // GameManager �ν��Ͻ��� null�� ��� �޼��� ����
        if (this == null)
        {
            return;
        }

        // Ÿ��Ʋ ���� ��� GameManager ��ü �ı�
        if (scene.name == "Title")
        {
            Destroy(gameObject);
            return;
        }

        playerController = FindObjectOfType<PlayerController>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        scoreText = GameObject.Find("Txt_Score").GetComponent<TextMeshProUGUI>();
        gameoverText = GameObject.Find("Txt_GameOver").GetComponent<TextMeshProUGUI>();

        // GameOver �ؽ�Ʈ ��Ȱ��ȭ
        gameoverText.gameObject.SetActive(false);

        // ���� ǥ�� ����
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
        // PlayerPrefs���� ���� �ҷ�����
        score = PlayerPrefs.GetInt("Score", 0);

        // ���� ǥ�� ����
        UpdateScoreText();

        // GameOver �ؽ�Ʈ ��Ȱ��ȭ
        gameoverText.gameObject.SetActive(false);

        // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ�� �޼��� ���
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void Update()
    {
        // Player�� ī�޶� ������ ������ ���� ����

        // �÷��̾ ī�޶��� ���� ��踦 �Ѿ�ٸ�
        if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Left && playerController.transform.position.x < cameraFollow.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            GameOver();
        }
        // �÷��̾ ī�޶��� ������ ��踦 �Ѿ�ٸ�
        else if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Right && playerController.transform.position.x > cameraFollow.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            GameOver();
        }
    }
}
