using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;

    public static ObjectPoolingManager Instance { get => instance; set => instance = value; }

    // ������Ʈ Ǯ ����Ʈ�� ���� ��ŷ���
    Dictionary<GameObject, List<GameObject>> objectPoolMap = new Dictionary<GameObject, List<GameObject>>();

    // ������Ʈ�� ��Ƶ� ����Ʈ
    [SerializeField] List<GameObject> prefabsPool = new List<GameObject>();

    // ������Ʈ Ǯ ������
    [SerializeField] private int poolSize;

    // ������ �������� ���� ������Ʈ Ǯ ����
    private void CreateObjectPool(GameObject prefab, int poolSize)
    {
        // ������ �������� �̹� ������Ʈ Ǯ�� ������ �Ǿ� ���� �ʴٸ�
        if(!objectPoolMap.ContainsKey(prefab))
        {
            // ������Ʈ Ǯ ����Ʈ ��ü ����
            List<GameObject> objPoolList = new List<GameObject>();
            for(int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab,Vector3.zero, Quaternion.identity);
                obj.SetActive(false); // ���� ������Ʈ ��Ȱ��ȭ
                objPoolList.Add(obj); // ������Ʈ Ǯ�� ������ ���� ������Ʈ �߰�
            }

            // ������Ʈ Ǯ ��ųʸ��� ������ ������Ʈ Ǯ �߰�
            objectPoolMap.Add(prefab, objPoolList);
        }
    }

    // ������ �������� ������Ʈ Ǯ���� �ϳ��� ������Ʈ�� ��ȯ��.
    public GameObject GetObjFromPool(GameObject prefabKey, Vector3 position, Quaternion rotation)
    {
        // ������Ʈ Ǯ ��ųʸ��� ������ ������ Ű�� ���� ������Ʈ Ǯ ����Ʈ�� �����ϴ����� üũ
        if(objectPoolMap.ContainsKey(prefabKey))
        {
            // ������Ʈ Ǯ ��ųʸ����� ����Ʈ�� ������ (������ �������� �ִ���)
            List<GameObject> objPoolList = objectPoolMap[prefabKey];

            foreach(GameObject obj in objPoolList)
            {
                // ���� ���ӿ�����Ʈ�� Ȱ��ȭ�� �� ���°� �ƴϸ�
                if(!obj.activeInHierarchy)
                {
                    // ������Ʈ�� ��ġ�� ȸ���� �����ϰ� Ȱ��ȭ �ѵ� ������ ��ȯ
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // ������Ʈ Ǯ�� ���̻� �� �������� ���ٸ� �����ؼ� �־���
            GameObject newObj = Instantiate(prefabKey,position,rotation);
            objPoolList.Add(newObj);
            return newObj;
        }

        // �׷��� �ʴٸ� null�� ��ȯ
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
        // ������Ʈ Ǯ ��ųʸ��� ������Ʈ Ǯ ����Ʈ ����
        foreach (GameObject prefab in prefabsPool)
        {
            CreateObjectPool(prefab, poolSize);
        }
    }
}
