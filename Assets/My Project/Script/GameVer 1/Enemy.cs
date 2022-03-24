using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;

    private void Update()
    {
        if(HP <= 0)
            Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Border"))
            Destroy(gameObject);

        if(col.CompareTag("PlayerBullet"))
        {
            HP -= col.GetComponent<PlayerBullet>().Damage;
        }
    }
}
