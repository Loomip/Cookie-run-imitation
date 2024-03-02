using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // 생성될 프리펩
    [SerializeField] protected GameObject coinPrefab;

    // 생성할 갯수
    [SerializeField] protected int coinCount;

    // 프리펩 간의 거리
    [SerializeField] protected float gapDistance;

    [SerializeField] protected bool isCreate = false;

    public virtual void Create()
    {
        isCreate = true;
    }
}
