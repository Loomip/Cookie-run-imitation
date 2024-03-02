using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // �Ѿ� ������
    [SerializeField] GameObject bulletPrefab;

    // �Ѿ� ���� ��ġ
    [SerializeField] Transform[] shotTransforms;

    // ����� ����� ������Ʈ
   // [SerializeField] AudioSource audioSource;

    // ���� ����
    [SerializeField] int shotCount;

    // ���� ���� ���� �ð���
    [SerializeField] float shotBetweenTime; // ���� �ð�

    [SerializeField] float shotTimer; // ����

    //private void Awake()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}

    private void Start()
    {
        StartCoroutine("AutoFire");
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotTimer);

            // ���� ���� ���� �ð��� �������� �ʾҴٸ�
            if (shotBetweenTime <= 0)
            {
                // ���� �Ҹ� ���
                // audioSource.Play();
            }

            for(int i = 0; i < shotCount; i++)
            {
                // ������ ����
                ObjectPoolingManager.Instance.GetObjFromPool(bulletPrefab, shotTransforms[i].position, shotTransforms[i].rotation);

                // ���� ���� ���� �ð��� �����Ǿ� �ִٸ�
                if (shotBetweenTime > 0)
                {
                    // ���� �Ҹ� ���
                    //audioSource.Play();
                }

                // ���� ���� ���� ó��
                yield return new WaitForSeconds(shotBetweenTime);
            }
        }
    }

}
