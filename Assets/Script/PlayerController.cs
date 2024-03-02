using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player �⺻ �̵� ��ũ��Ʈ
public class PlayerController : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody2D m_rigidbody;

    public Rigidbody2D m_Rigidbody { get => m_rigidbody; set => m_rigidbody = value; }

    [SerializeField]
    //������
    float jumpPower;

    [SerializeField]
    //���� �ӵ�
    float runSpeed;

    // �̵� ����
    Vector2 direction;

    // ĳ������ ���� ���¸� ��Ÿ���� ����
    int m_JumpState;

    // ĳ������ ���¸� ��Ÿ���� ����
    public bool isGameOver;

    // ����
    private void jump()
    {
        if (m_JumpState < 2) // �� �� ������ �� ����
        {
            m_Rigidbody.velocity = Vector2.zero;

            m_Rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            m_Animator.SetInteger("State", m_JumpState + 2);
            m_JumpState++;
        }
    }

    private void Run()
    {
        // ���� ���� ����Ű �Է°�
        float h = Input.GetAxisRaw("Horizontal");

        // Ű �Է��� ���� ����
        direction = new Vector2(h * runSpeed, m_Rigidbody.velocity.y);

        //m_Rigidbody.MovePosition(direction * runSpeed * Time.deltaTime);
        m_Rigidbody.velocity = direction;
    }

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_JumpState = 0;
        isGameOver = false;
        direction = transform.position;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            Run();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
            }

            if (m_Rigidbody.velocity.y < 0f && (m_JumpState == 1 || m_JumpState == 2))
            {
                m_Animator.SetInteger("State", 4);
            }
        }
    }

    private void LateUpdate()
    {
        if (direction.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_JumpState = 0;
            m_Animator.SetInteger("State", 1);
        }
    }
}

