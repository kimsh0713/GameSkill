using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POOL_TYPE
{
    ENEMY,
    PBULET,
    EBULET,
    END
}

public class ObjectPool : MonoBehaviour
{
    public POOL_TYPE poolType;
    public GameObject PoolObj;
    private Queue<GameObject> objects = new Queue<GameObject>();

    private void Awake()
    {
        S.pools[(int)poolType] = this;
    }

    public GameObject Get(Vector3 pos)
    {
        GameObject obj = null;

        if (objects.Count == 0)
        {
            obj = Instantiate(PoolObj);
        }
        else
        {
            obj = objects.Dequeue();
        }

        obj.transform.SetParent(transform);
        obj.transform.position = pos;
        obj.SetActive(true);

        return obj;
    }

    public T Get<T>(Vector3 pos) => Get(pos).GetComponent<T>();

    public void Return(GameObject obj)
    {
        objects.Enqueue(obj);
        obj.SetActive(false);
    }

    public void WaitReturn(GameObject obj, float time)
    {
        StartCoroutine(EWaitReturn(obj, time));
    }

    private IEnumerator EWaitReturn(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Return(obj);
    }
}
