using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class S
{
    public static Player player;
    public static ObjectPool[] pools = new ObjectPool[(int)POOL_TYPE.END];

    public static ObjectPool Pool(POOL_TYPE type) => pools[(int)type];

    public static (ObjectPool pool, GameObject obj) PoolGet(POOL_TYPE type, Vector3 pos) => (pools[(int)type], pools[(int)type].Get(pos));
    public static (ObjectPool pool, T obj) PoolGet<T>(POOL_TYPE type, Vector3 pos) => (pools[(int)type], pools[(int)type].Get<T>(pos));

    public static (ObjectPool pool, GameObject obj) Shot(Vector3 spawnPos, Vector3 dir, float speed, int Damage)
    {
        var pool = PoolGet<PlayerBullet>(POOL_TYPE.PBULET, spawnPos);
        pool.obj.Dir = dir;
        pool.obj.BulletSpeed = speed;
        pool.obj.Damage = Damage;
        pool.obj.GetComponent<TrailRenderer>().Clear();

        return (pool.pool, pool.obj.gameObject);
    }
}
