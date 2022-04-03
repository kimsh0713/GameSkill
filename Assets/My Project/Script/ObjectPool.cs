using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    static ObjectPool instance;

    private void Awake() => instance = this;

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] Pool[] pools;
    List<GameObject> objects;
    Dictionary<string, Queue<GameObject>> poolDic;

    public static GameObject SpawnPoolObj(string tag, Vector3 pos) => instance._PoolObj(tag, pos, Quaternion.identity);
    public static T SpawnPoolObj<T>(string tag, Vector3 pos) where T : Component
    {
        GameObject obj = instance._PoolObj(tag, pos, Quaternion.identity);
        if (obj.TryGetComponent(out T component))
            return component;
        else
        {
            obj.SetActive(false);
            throw new Exception("Component not found");
        }
    }
    GameObject _PoolObj(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDic.ContainsKey(tag))
            throw new Exception("태그가 존재 하지 않음");

        Queue<GameObject> poolQueue = poolDic[tag];
        if (poolQueue.Count == 0)
        {
            Pool pool = Array.Find(pools, x => x.tag == tag);
            var obj = CreateObj(pool.tag, pool.prefab);
            ArrangePool(obj);
        }

        GameObject _obj = poolQueue.Dequeue();
        _obj.transform.position = pos;
        _obj.transform.rotation = rot;
        _obj.SetActive(true);

        return _obj;
    }
    GameObject CreateObj(string tag, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false);
        return obj;
    }
    void ArrangePool(GameObject obj)
    {
        bool isFind = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == transform.childCount - 1)
            {
                obj.transform.SetSiblingIndex(i);
                objects.Insert(i, obj);
                break;
            }
            else if (transform.GetChild(i).name == obj.name)
                isFind = true;
            else if (isFind)
            {
                obj.transform.SetSiblingIndex(i);
                objects.Insert(i, obj);
                break;
            }
        }
    }
    public static void ReturnToPool(GameObject obj)
    {
        if (!instance.poolDic.ContainsKey(obj.name))
            throw new Exception($"Pool with tag {obj.name} doesn't exist.");

        instance.poolDic[obj.name].Enqueue(obj);
    }
    private void Start()
    {
        objects = new List<GameObject>();
        poolDic = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            poolDic.Add(pool.tag, new Queue<GameObject>());
            for (int i = 0; i < pool.size; i++)
            {
                var obj = CreateObj(pool.tag, pool.prefab);
                ArrangePool(obj);
            }

            if (poolDic[pool.tag].Count <= 0)
                Debug.LogError("Unimplemented ReturnToPool");
            else if (poolDic[pool.tag].Count != pool.size)
                Debug.LogError("Overlap of ReturnToPool");
        }
    }
}
