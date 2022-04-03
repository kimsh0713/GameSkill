using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class S
{
    public static Player player;
    public static GameManager GM;

    public static void Fire(string tag, Vector3 spawnPos, Vector3 dir, float Speed, int Damage, BULLET_TYPE type)
    {
        var bullet = ObjectPool.SpawnPoolObj<PlayerBullet>(tag, spawnPos);
        bullet.Dir = dir;
        bullet.Speed = Speed;
        bullet.Damage = Damage;
        bullet.bullet_type = type;
    }

    public static void SpawnItem(string tag, Vector3 pos, int count, ITEM_TYPE type)
    {
        for (int i = 0; i < count; i++)
        {
            var item = ObjectPool.SpawnPoolObj<Items>(tag, pos);
            item.item_type = type;
        }
    }

    public static IEnumerator ShakeCamera(float ShakeTime, float ShakePower)
    {
        var startPosition = Camera.main.transform.position;

        while (ShakeTime > 0.0f)
        {
            Camera.main.transform.position = startPosition + Random.insideUnitSphere * ShakePower;
            ShakeTime -= Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = startPosition;
    }
}