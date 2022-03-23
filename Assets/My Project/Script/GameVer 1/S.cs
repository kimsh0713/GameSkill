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

    public static (ObjectPool pool, GameObject obj) Shot(Vector3 spawnPos, Vector3 dir, float speed, int Damage, bool isNomalAttack)
    {
        var pool = PoolGet<PlayerBullet>(POOL_TYPE.PBULET, spawnPos);
        if (isNomalAttack)
        {
            pool.obj.Dir = dir;
            pool.obj.BulletSpeed = speed;
            pool.obj.Damage = Damage;
            pool.obj.GetComponent<TrailRenderer>().Clear();
        }
        else
        {
            var target = Physics.OverlapSphere(player.transform.position, 50f, 1 << 8);
            if (target != null)
            {
                for (int i = 0; i < target.Length; i++)
                {
                    pool.obj.GetComponent<Bezier>().Init(player.transform, target[i].transform, 2, 10);
                }
            }
        }

        pool.obj.GetComponent<TrailRenderer>().Clear();
        return (pool.pool, pool.obj.gameObject);
    }
}
