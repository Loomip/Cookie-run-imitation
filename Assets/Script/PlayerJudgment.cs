using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player 판정 스크립트
public class PlayerJudgment : MonoBehaviour
{
    // 게임 매니저 컨포넌트
    private GameManager gameManager;

    // Player 컨트롤러 컨포넌트
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인 점수
        if (collision.gameObject.CompareTag("Coin"))
        {
            ObjectPoolingManager.Instance.RetrunObjectToPool(collision.gameObject);
            gameManager.IncreaseScore(1000);
        }

        // 장애물 관련
        if (collision.gameObject.CompareTag("Scenery"))
        {
            if (gameManager.LifeCount > 1)
            {
                gameManager.LifeCount--;
                Destroy(gameManager.hearts[gameManager.LifeCount]);
                
            }
            else if(gameManager.LifeCount == 1)
            {
                gameManager.LifeCount--;
                Destroy(gameManager.hearts[gameManager.LifeCount]);
                // 게임 오버 처리
                playerController.m_Rigidbody.velocity = Vector2.zero;
                playerController.isGameOver = true;
            }
        }
    }
}
