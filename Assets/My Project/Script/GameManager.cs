using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 SpawnPos;
    public GameObject Enemy;

    float time = 0;
    public float SpawnTime;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= SpawnTime)
        {
            SpawnPos = new Vector3(transform.position.x + Random.Range(-100f, 100f),transform.position.y, transform.position.z + 400);
            Instantiate(Enemy, SpawnPos, Quaternion.identity);
            time = 0;
        }
    }
}
