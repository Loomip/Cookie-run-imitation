using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 일자 프리팹 생성 스크립트
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
