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
    Dictionary<string, Queue<GameObject>> Dic;

    public static GameObject Get(string tag, Vector3 pos) => instance.PoolObj(tag, pos);

    public static T Get<T>(string tag, Vector3 pos) where T : Component
    {
        var obj = instance.PoolObj(tag, pos);
        if (obj.TryGetComponent(out T compoenet))
            return compoenet;
        else
        {
            obj.SetActive(false);
            throw new Exception("컴포넌트를 찾을 수 없음");
        }
    }

    private GameObject PoolObj(string tag, Vector3 pos)
    {
        if (!instance.Dic.ContainsKey(tag))
            throw new Exception("태그가 다르거나 존재하지 않음");

        Queue<GameObject> poolQueue = Dic[tag];
        if (poolQueue.Count == 0)
        {
            Pool pool = Array.Find(pools, x => x.tag == tag);
            var obj = Create(pool.tag, pool.prefab);
            Arrange(obj);
        }

        GameObject _obj = poolQueue.Dequeue();
        _obj.transform.position = pos;
        _obj.SetActive(true);
        return _obj;
    }

    private GameObject Create(string tag, GameObject prefab)
    {
        var obj = Instantiate(prefab, transform);
        obj.name = tag;
        obj.SetActive(false);
        return obj;
    }

    public static void Return(GameObject obj)
    {
        if (!instance.Dic.ContainsKey(obj.name))
            throw new Exception("태그가 다르거나 존재하지 않음");

        instance.Dic[obj.name].Enqueue(obj);
    }

    private void Arrange(GameObject obj)
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

    public static void AllObjSetFalse()
    {
        for(int i = 0; i < instance.objects.Count; i++)
        {
            instance.objects[i].SetActive(false);
        }
    }

    private void Start()
    {
        objects = new List<GameObject>();
        Dic = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Dic.Add(pool.tag, new Queue<GameObject>());
            for (int i = 0; i < pool.size; i++)
            {
                var obj = Create(pool.tag, pool.prefab);
                Arrange(obj);
            }

            //if (Dic[pool.tag].Count >= pool.size)
            //    Debug.LogError("Retrun is Not");
            //else if (Dic[pool.tag].Count != pool.size)
            //    Debug.LogError("Overlap of Return");
        }
    }
}
