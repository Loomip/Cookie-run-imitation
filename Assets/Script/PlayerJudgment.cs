using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

// Player 판정 스크립트
public class PlayerJudgment : MonoBehaviour
{
    // 게임 매니저 컨포넌트
    private GameManager gameManager;

    private Rigidbody2D rgd;

    // 튕겨져 나가는 힘
    [SerializeField] private float recoilForce;

    // SpriteRenderer 컴포넌트 참조
    private SpriteRenderer sprite;

    private void Awake()
    {
        rgd = GetComponentInParent<Rigidbody2D>();
        sprite = GetComponentInParent<SpriteRenderer>();
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
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물
        if (collision.gameObject.CompareTag("Scenery"))
        {
            StartCoroutine(Assaulted(collision));
        }
    }

    IEnumerator Assaulted(Collision2D collision)
    {
        // 피격시 
        sprite.color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(5f);

        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
