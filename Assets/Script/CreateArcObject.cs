using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 반원 프리팹 생성 스크립트
public class CreateArcObject : CreateObject
{
    // X축 반지름
    [SerializeField] private float xRadius;
    // Y축 반지름
    [SerializeField] private float yRadius;
    // 코인 높이 보정 값
    [SerializeField] private float yCorrection;

    public override void Create()
    {
        if (!isCreate)
        {
            // 장애물 주변에 코인 프리팹 생성
            for (int i = 0; i < coinCount; i++)
            {
                // 각도를 계산 (0부터 180도까지)
                float theta = Mathf.PI * i / (coinCount - 1);
                // 원의 좌표 계산
                float x = xRadius * Mathf.Cos(theta);
                float y = yRadius * Mathf.Sin(theta);
                Vector2 coinPosition = new Vector2(transform.position.x + x, transform.position.y + yCorrection + y);
                ObjectPoolingManager.Instance.GetObjFromPool(coinPrefab, coinPosition, Quaternion.identity);
            }
        }

        base.Create();
    }
}
