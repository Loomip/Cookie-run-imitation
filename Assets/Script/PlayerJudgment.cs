using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player ���� ��ũ��Ʈ
public class PlayerJudgment : MonoBehaviour
{
    // ���� �Ŵ��� ������Ʈ
    private GameManager gameManager;

    // Player ��Ʈ�ѷ� ������Ʈ
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ����
        if (collision.gameObject.CompareTag("Coin"))
        {
            ObjectPoolingManager.Instance.RetrunObjectToPool(collision.gameObject);
            gameManager.IncreaseScore(1000);
        }

        // ��ֹ� ����
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
                // ���� ���� ó��
                playerController.m_Rigidbody.velocity = Vector2.zero;
                playerController.isGameOver = true;
            }
        }
    }
}
