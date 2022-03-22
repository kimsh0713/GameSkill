using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float BulletSpeed;


    private void OnEnable()
    {
        GameObject player = GameObject.Find("Player");
        transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        Vector3 movedir = new Vector3(0, 0, BulletSpeed) * Time.deltaTime;
        transform.Translate(movedir);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
