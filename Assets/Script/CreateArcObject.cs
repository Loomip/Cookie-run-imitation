using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݿ� ������ ���� ��ũ��Ʈ
public class CreateArcObject : CreateObject
{
    // X�� ������
    [SerializeField] private float xRadius;
    // Y�� ������
    [SerializeField] private float yRadius;
    // ���� ���� ���� ��
    [SerializeField] private float yCorrection;

    public override void Create()
    {
        if (!isCreate)
        {
            // ��ֹ� �ֺ��� ���� ������ ����
            for (int i = 0; i < coinCount; i++)
            {
                // ������ ��� (0���� 180������)
                float theta = Mathf.PI * i / (coinCount - 1);
                // ���� ��ǥ ���
                float x = xRadius * Mathf.Cos(theta);
                float y = yRadius * Mathf.Sin(theta);
                Vector2 coinPosition = new Vector2(transform.position.x + x, transform.position.y + yCorrection + y);
                ObjectPoolingManager.Instance.GetObjFromPool(coinPrefab, coinPosition, Quaternion.identity);
            }
        }

        base.Create();
    }
}
