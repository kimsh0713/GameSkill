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
        if (isNomalAttack)
        {
            var pool = PoolGet<PlayerBullet>(POOL_TYPE.PBULET, spawnPos);
            pool.obj.Dir = dir;
            pool.obj.BulletSpeed = speed;
            pool.obj.Damage = Damage;
            pool.obj.GetComponent<TrailRenderer>().Clear();
            return (pool.pool, pool.obj.gameObject);
        }
        else
        {
            var pool = PoolGet<Bezier>(POOL_TYPE.PBULET, player.transform.position);
            var colliders = Physics.OverlapSphere(player.transform.position, 100, 1 << 8);
            GameObject target = null;
            foreach (Collider targetCol in colliders)
            {
                float distance = Vector3.Distance(player.transform.position, targetCol.transform.position);

                if (target < Vector3.Distance(player.transform.position, colliders.position))
                {

                }
            }

            pool.obj.Init(player.transform, target.transform, 2, 20);
            pool.obj.GetComponent<TrailRenderer>().Clear();

            if (targe != null)
                return (pool.pool, pool.obj.gameObject);
            else
                return (null, null);
        }
    }
}
