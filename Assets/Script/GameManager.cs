using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 점수 스코어
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // 점수 저장
    private int score;

    // 하트 충돌 배열
    public GameObject[] hearts;

    // 현재 생명수
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
