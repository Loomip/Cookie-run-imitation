using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionMovement : MonoBehaviour
{
    // �Ѿ� �̵� �ӵ�
    [SerializeField]
    float speed;

    // �̵� ����
    [SerializeField] Vector2 direction;

    // �̵� ó��
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
