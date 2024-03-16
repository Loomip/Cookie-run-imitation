using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

// Player ���� ��ũ��Ʈ
public class PlayerJudgment : MonoBehaviour
{
    // ���� �Ŵ��� ������Ʈ
    private GameManager gameManager;

    private Rigidbody2D rgd;

    // ƨ���� ������ ��
    [SerializeField] private float recoilForce;

    // SpriteRenderer ������Ʈ ����
    private SpriteRenderer sprite;

    private void Awake()
    {
        rgd = GetComponentInParent<Rigidbody2D>();
        sprite = GetComponentInParent<SpriteRenderer>();
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
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ�
        if (collision.gameObject.CompareTag("Scenery"))
        {
            StartCoroutine(Assaulted(collision));
        }
    }

    IEnumerator Assaulted(Collision2D collision)
    {
        // �ǰݽ� 
        sprite.color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(5f);

        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
