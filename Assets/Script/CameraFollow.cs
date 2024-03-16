using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 카메라 연출 스크립트
public class CameraFollow : SingletonDontDestroy<CameraFollow>
{
    [SerializeField] private Vector3 startPosition; // 카메라 시작위치

    [SerializeField] private float moveSpeed;

    // 카메라 위치 조절
    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;

    // 게임 오버가 되는 방향 결정
    public enum GameOverDirection { Left, Right }

    public GameOverDirection gameOverDirection;

    private void Start()
    {
        transform.position = startPosition;

        // 씬이 로드될 때마다 호출되는 이벤트에 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // GameManager 인스턴스가 null인 경우 메서드 종료
        if (this == null)
        {
            return;
        }

        // 타이틀 씬인 경우 CameraFollow 객체 파괴
        if (scene.name == "Title")
        {
            Destroy(gameObject);
            return;
        }

        // 씬이 로드될 때마다 moveSpeed 증가, 단 최대값은 4.5
        moveSpeed = Mathf.Min(Mathf.Abs(moveSpeed) + 0.5f, 4.5f);

        // 짝수 씬에서는 moveSpeed 부호 반전 및 gameOverDirection 변경
        if (SceneManager.GetActiveScene().buildIndex % 2 == 0)
        {
            moveSpeed = -moveSpeed;
            gameOverDirection = GameOverDirection.Right;
        }
        else
        {
            moveSpeed = Mathf.Abs(moveSpeed);
            gameOverDirection = GameOverDirection.Left;
        }
    }

    private void Update()
    {
        float newPosition = transform.position.x + moveSpeed * Time.deltaTime;
        newPosition = Mathf.Clamp(newPosition, minPosX, maxPosX);
        transform.position = new Vector3(newPosition, startPosition.y, startPosition.z);
    }
}
