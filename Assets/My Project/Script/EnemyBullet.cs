using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed;
    public Vector3 Dir;

    #region UnityMestod
    private void Update()
    {
        transform.Translate(Dir * Speed * Time.deltaTime, Space.Self);

        if (transform.position.x < -33 || transform.position.x > 33 ||
            transform.position.z < -30 || transform.position.z > 28)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
    }
    #endregion
}