using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ������ ���� ��ũ��Ʈ
public class CreateStraightObject : CreateObject
{
    public override void Create()
    {
        if (!isCreate)
        {
            Vector2 startDistance = transform.position;

            for (int i = 0; i < coinCount; i++)
            {
                Vector2 coinPosition = new Vector2(startDistance.x + gapDistance * i, startDistance.y);
                ObjectPoolingManager.Instance.GetObjFromPool(coinPrefab, coinPosition, Quaternion.identity);
            }
        }

         base.Create();
    }
}
