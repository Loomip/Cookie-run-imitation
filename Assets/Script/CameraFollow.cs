using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ī�޶� ���� ��ũ��Ʈ
public class CameraFollow : SingletonDontDestroy<CameraFollow>
{
    [SerializeField] private Vector3 startPosition; // ī�޶� ������ġ

    [SerializeField] private float moveSpeed;

    // ī�޶� ��ġ ����
    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;

    // ���� ������ �Ǵ� ���� ����
    public enum GameOverDirection { Left, Right }

    public GameOverDirection gameOverDirection;

    private void Start()
    {
        transform.position = startPosition;

        // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ�� �޼��� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // GameManager �ν��Ͻ��� null�� ��� �޼��� ����
        if (this == null)
        {
            return;
        }

        // Ÿ��Ʋ ���� ��� CameraFollow ��ü �ı�
        if (scene.name == "Title")
        {
            Destroy(gameObject);
            return;
        }

        // ���� �ε�� ������ moveSpeed ����, �� �ִ밪�� 4.5
        moveSpeed = Mathf.Min(Mathf.Abs(moveSpeed) + 0.5f, 4.5f);

        // ¦�� �������� moveSpeed ��ȣ ���� �� gameOverDirection ����
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
