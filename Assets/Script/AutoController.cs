using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoController : MonoBehaviour
{
    // 몬스터 이동속도
    public float speed = 2f;

    // 바라보는 방향 왼쪽:false, 오른쪽 True
    [SerializeField] private bool movingRight = false;

    // 벽을 감지할 레이케스트
    private RaycastHit2D hit;

    // 레이저 길이
    public float rayLength;

    // 레이케스트를 발사할 Transfrom
    [SerializeField] private Transform rayTransform;

    // Ground 레이어 마스크
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime); // 왼쪽으로 이동합니다.
        hit = Physics2D.Raycast(rayTransform.position, -transform.right, rayLength, groundLayer);
        Debug.DrawRay(rayTransform.position, -transform.right * rayLength, Color.red);

        if (hit.collider != null)
        {
            movingRight = !movingRight;
            // 몬스터의 움직임을 반대 방향으로 변경합니다.
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}
