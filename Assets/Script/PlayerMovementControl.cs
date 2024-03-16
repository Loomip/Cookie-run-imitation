using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ī�޶� ������ ������ �ʰ� �ϴ� ���
public class PlayerMovementControl : MonoBehaviour
{
    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void Update()
    {
        // ���� ���� ������ �����̶��
        if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Left && transform.position.x > cameraFollow.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            // �÷��̾ ī�޶��� ������ ������ ������ �ʰ� ����
            transform.position = new Vector2(cameraFollow.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        }
        // ���� ���� ������ ������ �̶��
        else if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Right && transform.position.x < cameraFollow.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            // �÷��̾ ī�޶��� ���� ������ ������ �ʰ� ����
            transform.position = new Vector2(cameraFollow.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        }
    }
}
