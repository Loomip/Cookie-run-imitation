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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            ObjectPoolingManager.Instance.RetrunObjectToPool(gameObject);
        }
    }
}
