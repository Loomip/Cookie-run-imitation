using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 연출 스크립트
public class CameraFollow : MonoBehaviour
{
    Transform player;
    public Vector3 offset; // 플레이어와 카메라 사이의 거리 설정

    // 카메라 위치 조절
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
