using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMovement : MonoBehaviour
{
    // 총알 이동 속도
    [SerializeField]
    float speed;

    // 이동 방향
    [SerializeField] Vector2 direction;

    // 이동 처리
    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ObjectPoolingManager.Instance.RetrunObjectToPool(gameObject);
        }
    }
}
