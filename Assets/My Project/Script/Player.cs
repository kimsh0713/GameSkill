using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Status")]
    public int HP;
    private int LV;
    public double Power;
    private int MaxHP = 6;
    public int Speed;
    public float FireDelay;
    private float FireTime;
    public bool isHurt = true;

    [Header("Other Object")]
    public GameObject ColliderMark;
    public GameObject body;
    public GameObject Explosion;


    #region UnityMestod
    private void Awake()
    {
        S.player = this;
    }

    private void Start()
    {
        StartCoroutine(ERotation());
    }

    private void Update()
    {
        PlayerMove();
        Fire();
        HpManagement();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(isHurt)
        {
            if (col.CompareTag("EnemyBullet"))
            {
                col.gameObject.SetActive(false);
                StartCoroutine(Death());
                HP--;
                Power--;
            }
        }
    }
    #endregion

    #region PlayerMove
    private void PlayerMove()
    {
        float h, v = 0;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        var curPos = transform.position;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.5f;
            ColliderMark.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            ColliderMark.SetActive(false);
        }
        curPos += new Vector3(h, 0, v) * Speed * Time.deltaTime;

        curPos.x = Mathf.Clamp(curPos.x, -26.8f, 26.8f);
        curPos.z = Mathf.Clamp(curPos.z, -21.6f, 21.6f);

        transform.position = curPos;
    }
    private IEnumerator ERotation()
    {
        float x = 0;
        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(Vector3.forward * x * 35), Time.deltaTime * 10);
            yield return new WaitForSeconds(0.01f);
        }
    }
    #endregion

    #region PlayerFire
    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Underscore))
            LV--;
        if (Input.GetKeyDown(KeyCode.Equals))
            LV++;

        if (Input.GetKey(KeyCode.Z))
        {
            if (FireTime >= FireDelay)
            {
                switch ((int)Power)
                {
                    case 1:
                        S.Fire("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position, Vector3.forward, 60, 10, BULLET_TYPE.HOMING);
                        S.Fire("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        break;
                    case 2:
                        S.Fire("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position, Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position + new Vector3(1.2f, 0, 0), Vector3.forward, 60, 10, BULLET_TYPE.HOMING);
                        S.Fire("P_Bullet", transform.position + new Vector3(-1.2f, 0, 0), Vector3.forward, 60, 10, BULLET_TYPE.HOMING);
                        break;
                    case 3:
                        S.Fire("P_Bullet", transform.position + new Vector3(1f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position + new Vector3(-1, 0, 0), Vector3.forward, 120, 10, BULLET_TYPE.NORMAL);
                        S.Fire("P_Bullet", transform.position, Vector3.forward, 60, 10, BULLET_TYPE.HOMING);
                        S.Fire("P_Bullet", transform.position + new Vector3(1f, 0, 0), Vector3.forward + new Vector3(0.2f, 0, 0), 60, 10, BULLET_TYPE.HOMING);
                        S.Fire("P_Bullet", transform.position + new Vector3(-1f, 0, 0), Vector3.forward + new Vector3(-0.2f, 0, 0), 60, 10, BULLET_TYPE.HOMING);
                        break;
                }
                FireTime = 0;
            }
        }

        FireTime += Time.deltaTime;

        Power = Mathf.Clamp((float)Power, 1, 3);
    }
    #endregion

    #region PlayerHpManagement
    private void HpManagement()
    {
        if (HP > MaxHP)
        {
            S.GM.Score += 1000;
            HP = MaxHP;
        }

        if (HP <= 0)
        {
            //GameOver;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HP++;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            HP--;
        }
    }

    private IEnumerator Death()
    {
        StartCoroutine(S.ShakeCamera(0.5f, 1));
        Instantiate(Explosion, transform.position, Quaternion.identity);
        transform.position = new Vector3(0, -10000, -21.6f);
        isHurt = false;

        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(0, 0, -21.6f);

        yield return new WaitForSeconds(2f);
        isHurt = true;
    }
    #endregion
}
