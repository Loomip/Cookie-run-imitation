using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    // ������ ������
    [SerializeField] protected GameObject coinPrefab;

    // ������ ����
    [SerializeField] protected int coinCount;

    // ������ ���� �Ÿ�
    [SerializeField] protected float gapDistance;

    [SerializeField] protected bool isCreate = false;

    public virtual void Create()
    {
        isCreate = true;
    }
}
