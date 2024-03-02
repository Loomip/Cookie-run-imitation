using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶� ���� ��ũ��Ʈ
public class CameraFollow : MonoBehaviour
{
    Transform player;
    public Vector3 offset; // �÷��̾�� ī�޶� ������ �Ÿ� ����

    // ī�޶� ��ġ ����
    [SerializeField] private float minPosX;
    [SerializeField] private float maxPosX;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x + offset.x, offset.y, offset.z);
        newPosition.x = Mathf.Clamp(newPosition.x, minPosX, maxPosX);
        transform.position = newPosition;
    }
}
