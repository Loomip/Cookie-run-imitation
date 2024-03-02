using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� ���ھ�
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // ���� ����
    private int score;

    // ��Ʈ �浹 �迭
    public GameObject[] hearts;

    // ���� �����
    private int lifeCount;

    public int LifeCount { get => lifeCount; set => lifeCount = value; }

    public void IncreaseScore(int amount)
    {
        score += amount;
        string Score = score.ToString("D10");
        Score = string.Format("{0:#,###}", int.Parse(Score));
        scoreText.text = Score;
    }

    private void Awake()
    {
        scoreText.text = "0";
        lifeCount = hearts.Length;
    }
}
