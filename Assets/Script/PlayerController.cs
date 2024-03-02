using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player 기본 이동 스크립트
public class PlayerController : MonoBehaviour
{
    Animator m_Animator;
    Rigidbody2D m_rigidbody;

    public Rigidbody2D m_Rigidbody { get => m_rigidbody; set => m_rigidbody = value; }

    [SerializeField]
    //점프력
    float jumpPower;

    [SerializeField]
    //전진 속도
    float runSpeed;

    // 이동 방향
    Vector2 direction;

    // 캐릭터의 점프 상태를 나타내는 변수
    int m_JumpState;

    // 캐릭터의 상태를 나타내는 변수
    public bool isGameOver;

    // 점프
    private void jump()
    {
        if (m_JumpState < 2) // 두 번 점프할 수 있음
        {
            m_Rigidbody.velocity = Vector2.zero;

            m_Rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            m_Animator.SetInteger("State", m_JumpState + 2);
            m_JumpState++;
        }
    }

    private void Run()
    {
        // 수평 수직 방향키 입력값
        float h = Input.GetAxisRaw("Horizontal");

        // 키 입력의 방향 벡터
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

