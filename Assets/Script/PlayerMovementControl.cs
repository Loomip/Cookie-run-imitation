using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 카메라 밖으로 나가지 않게 하는 기능
public class PlayerMovementControl : MonoBehaviour
{
    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void Update()
    {
        // 게임 오버 판정이 왼쪽이라면
        if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Left && transform.position.x > cameraFollow.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            // 플레이어가 카메라의 오른쪽 밖으로 나가지 않게 막음
            transform.position = new Vector2(cameraFollow.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        }
        // 게임 오버 판정이 오른쪽 이라면
        else if (cameraFollow.gameOverDirection == CameraFollow.GameOverDirection.Right && transform.position.x < cameraFollow.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height)
        {
            // 플레이어가 카메라의 왼쪽 밖으로 나가지 않게 막음
            transform.position = new Vector2(cameraFollow.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height, transform.position.y);
        }
    }
}
