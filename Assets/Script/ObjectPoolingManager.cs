using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;

    public static ObjectPoolingManager Instance { get => instance; set => instance = value; }

    // 오브젝트 풀 리스트를 담을 딕셔러니
    Dictionary<GameObject, List<GameObject>> objectPoolMap = new Dictionary<GameObject, List<GameObject>>();

    // 오브젝트를 담아둘 리스트
    [SerializeField] List<GameObject> prefabsPool = new List<GameObject>();

    // 오브젝트 풀 사이즈
    [SerializeField] private int poolSize;

    // 지정한 프리팹의 게임 오브젝트 풀 생성
    private void CreateObjectPool(GameObject prefab, int poolSize)
    {
        // 지정한 프리팹이 이미 오브젝트 풀로 생성이 되어 있지 않다면
        if(!objectPoolMap.ContainsKey(prefab))
        {
            // 오브젝트 풀 리스트 객체 생성
            List<GameObject> objPoolList = new List<GameObject>();
            for(int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab,Vector3.zero, Quaternion.identity);
                obj.SetActive(false); // 게임 오브젝트 비활성화
                objPoolList.Add(obj); // 오브젝트 풀에 생성된 게임 오브젝트 추가
            }

            // 오브젝트 풀 딕셔너리에 생성된 오브젝트 풀 추가
            objectPoolMap.Add(prefab, objPoolList);
        }
    }

    // 지정한 프리팹의 오브젝트 풀에서 하나의 오브젝트를 반환함.
    public GameObject GetObjFromPool(GameObject prefabKey, Vector3 position, Quaternion rotation)
    {
        // 오브젝트 풀 딕셔너리에 지정한 프리팹 키를 가진 오브젝트 풀 리스트가 존재하는지를 체크
        if(objectPoolMap.ContainsKey(prefabKey))
        {
            // 오브젝트 풀 딕셔너리에서 리스트를 참조함 (참조할 프리펩이 있는지)
            List<GameObject> objPoolList = objectPoolMap[prefabKey];

            foreach(GameObject obj in objPoolList)
            {
                // 현재 게임오브젝트가 활성화가 된 상태가 아니면
                if(!obj.activeInHierarchy)
                {
                    // 오브젝트의 위치와 회전을 설정하고 활성화 한뒤 참조를 반환
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // 오브젝트 풀에 더이상 쓸 프리팹이 없다면 생성해서 넣어줌
            GameObject newObj = Instantiate(prefabKey,position,rotation);
            objPoolList.Add(newObj);
            return newObj;
        }

        // 그렇지 않다면 null을 반환
        return null;
    }

    public void RetrunObjectToPool(GameObject returnobj)
    {
        returnobj.transform.position = Vector3.zero;
        returnobj.transform.rotation = Quaternion.identity;
        returnobj.SetActive(false);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 오브젝트 풀 딕셔너리에 오브젝트 풀 리스트 생성
        foreach (GameObject prefab in prefabsPool)
        {
            CreateObjectPool(prefab, poolSize);
        }
    }
}
