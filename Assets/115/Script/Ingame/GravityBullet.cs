using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : MonoBehaviour
{
    private Rigidbody rigid;
    private MeshRenderer mr;

    public int Damage;

    float x, z;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (transform.position.x > 35 || transform.position.x < -32.5f || transform.position.z < -28.5f)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        x = Random.Range(-12f, 12f);
        z = Random.Range(5f, 20f);

        mr.material.color = new Color32((byte)Random.Range(30, 255), (byte)Random.Range(30, 255), (byte)Random.Range(30, 255), 255);

        rigid.velocity = Vector3.zero;
        rigid.AddForce(new Vector3(x, 0, z), ForceMode.Impulse);
    }

    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }
}
