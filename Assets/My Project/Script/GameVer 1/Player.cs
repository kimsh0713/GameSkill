using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("PlayerMove")]
    public GameObject body;
    public float MoveSpeed;
    public float NomalMoveSpeed;
    private GameObject Water;

    [Header("Fire")]
    public int LV = 1;
    public int Damage;
    public float FireSpeed;
    public float FireDelay;
    private float FireTime;
    public GameObject MissileObj;

    public GameObject OBJ;


    #region UnityMetsod
    private void Awake()
    {
        S.player = this;
        Water = GameObject.Find("Water");
    }

    private void Start()
    {
        StartCoroutine(ERotation());
    }
    private void Update()
    {
        PlayerMove();
        MoveWater();
        Fire();
    }
    #endregion

    #region PlayerMoving
    private void PlayerMove()
    {
        float h = 0, v = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            h = Input.GetAxisRaw("Horizontal") * (MoveSpeed * 1.5f) * Time.deltaTime;
            v = Input.GetAxisRaw("Vertical") * (MoveSpeed * 1.5f) * Time.deltaTime;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime;
            v = Input.GetAxisRaw("Vertical") * MoveSpeed * Time.deltaTime;
        }
        v = Mathf.Clamp(v, -0.4f, 2);

        Vector3 movedir = new Vector3(h * 0.8f, 0, v) + Vector3.forward * NomalMoveSpeed * Time.deltaTime;
        transform.Translate(movedir);
    }

    private IEnumerator ERotation()
    {
        float x = 0;
        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(Vector3.forward * x * 25), Time.deltaTime * 8);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void MoveWater()
    {
        Water.transform.position = new Vector3(Water.transform.position.x, Water.transform.position.y, transform.position.z);
    }

    #endregion

    #region PlayerFireBullet

    private void Fire()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if (FireTime >= FireDelay)
            {
                S.Shot(transform.position, Vector3.forward, FireSpeed, Damage, true);
                FireTime = 0;
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            S.Shot(transform.position, Vector3.forward, FireSpeed, Damage, false);
        }
        FireTime += Time.deltaTime;
    }

    #endregion
}
