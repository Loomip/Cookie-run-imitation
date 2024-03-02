using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // 총알 프리팹
    [SerializeField] GameObject bulletPrefab;

    // 총알 발포 위치
    [SerializeField] Transform[] shotTransforms;

    // 오디오 재생기 컴포넌트
   // [SerializeField] AudioSource audioSource;

    // 발포 갯수
    [SerializeField] int shotCount;

    // 발포 관련 제어 시간들
    [SerializeField] float shotBetweenTime; // 간격 시간

    [SerializeField] float shotTimer; // 계산용

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

            // 연속 발포 지연 시간이 설정되지 않았다면
            if (shotBetweenTime <= 0)
            {
                // 발포 소리 재생
                // audioSource.Play();
            }

            for(int i = 0; i < shotCount; i++)
            {
                // 레이저 발포
                ObjectPoolingManager.Instance.GetObjFromPool(bulletPrefab, shotTransforms[i].position, shotTransforms[i].rotation);

                // 연속 발포 지연 시간이 설정되어 있다면
                if (shotBetweenTime > 0)
                {
                    // 발포 소리 재생
                    //audioSource.Play();
                }

                // 연속 발포 지연 처리
                yield return new WaitForSeconds(shotBetweenTime);
            }
        }
    }

}
