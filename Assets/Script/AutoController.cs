using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoController : MonoBehaviour
{
    // ���� �̵��ӵ�
    public float speed = 2f;

    // �ٶ󺸴� ���� ����:false, ������ True
    [SerializeField] private bool movingRight = false;

    // ���� ������ �����ɽ�Ʈ
    private RaycastHit2D hit;

    // ������ ����
    public float rayLength;

    // �����ɽ�Ʈ�� �߻��� Transfrom
    [SerializeField] private Transform rayTransform;

    // Ground ���̾� ����ũ
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime); // �������� �̵��մϴ�.
        hit = Physics2D.Raycast(rayTransform.position, -transform.right, rayLength, groundLayer);
        Debug.DrawRay(rayTransform.position, -transform.right * rayLength, Color.red);

        if (hit.collider != null)
        {
            movingRight = !movingRight;
            // ������ �������� �ݴ� �������� �����մϴ�.
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
