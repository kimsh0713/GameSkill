using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    public float BulletSpeed;
    public Vector3 Dir;
    public int Damage;

    private void FixedUpdate()
    {
        transform.Translate(Dir * Time.deltaTime * BulletSpeed);

        if (Vector3.Distance(S.player.transform.position, transform.position) >= 500)
        {
            S.Pool(POOL_TYPE.PBULET).Return(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            S.Pool(POOL_TYPE.PBULET).Return(this.gameObject);
        }
    }
}
