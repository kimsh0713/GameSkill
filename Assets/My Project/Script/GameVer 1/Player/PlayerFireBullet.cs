using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBullet : MonoBehaviour
{
    public GameObject Bullet;
    private GameObject Bullets;
    public float fireDelay = 0.3f;
    private float fireTime = 0;

    public Transform[] FirePosition;

    private void Awake()
    {
        Bullets = GameObject.Find("Bullets");
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if (fireTime >= fireDelay)
            {
                //Instantiate(Bullet, FirePosition[0].position, Quaternion.identity, Bullets.transform);
                fireTime = 0;
            }
        }
        fireTime += Time.deltaTime;
    }
}
