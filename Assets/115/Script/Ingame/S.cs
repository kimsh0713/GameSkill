using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class S
{
    public static Player player;
    public static GameManager GM;
    public static int Stage = 1;
    public static int HiScore;
    public static void Shot(string tag, Vector3 spawnPos, Vector3 dir, Quaternion rot, float Speed, int Damage, BULLET_TYPE type)
    {
        var obj = ObjectPool.Get<Bullet>(tag, spawnPos);
        obj.Dir = dir;
        obj.Speed = Speed;
        obj.rot = rot;
        obj.Damage = Damage;
        obj.type = type;
        obj.GetComponent<TrailRenderer>().Clear();
    }

    public static void SpawnItem(string tag, Vector3 SpawnPos, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            ObjectPool.Get<Items>(tag, SpawnPos);
        }
    }

    public static IEnumerator CameraShake(float ShakeTime, float ShakePower, bool isInit = true)
    {
        var startPos = Camera.main.transform.position;

        while (ShakeTime > 0.0f)
        {
            Camera.main.transform.position = startPos + Random.insideUnitSphere * ShakePower;
            ShakeTime -= Time.deltaTime;
            yield return null;
        }
        if (isInit)
            Camera.main.transform.position = new Vector3(0, 50, 0);
    }
}
