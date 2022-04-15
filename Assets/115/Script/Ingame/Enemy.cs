using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_TYPE
{
    BACTERIA,
    GREM,
    CENCER,
    VIRUS,
    END
}

public class Enemy : MonoBehaviour
{
    public int Hp;
    public Vector3 Dir;
    public float Speed;
    public int Damage;

    public float FireDelay;
    private float FireTime;

    public ENEMY_TYPE enemy_type;

    private Vector3 ranVec;
    public float rotSpeed;

    private bool isPattern = false;

    #region UnityMestod

    private void Start()
    {
    }

    private void Update()
    {
        Move();
        HpManagemant();
        Fire();
    }

    private void OnEnable()
    {
        ranVec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotSpeed = Random.Range(50, 150);

        if (S.player.Power >= 5)
        {
            Hp *= 2;
        }
    }

    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("P_Bullet"))
        {
            col.gameObject.SetActive(false);
            Hp -= col.GetComponent<Bullet>().Damage;
            S.GM.Score += 10;
        }
    }
    #endregion

    private void Move()
    {
        // Move Ani
        switch (enemy_type)
        {
            case ENEMY_TYPE.CENCER:
            case ENEMY_TYPE.BACTERIA:
                transform.GetChild(0).Rotate(ranVec * 150 * Time.deltaTime);
                break;
            case ENEMY_TYPE.VIRUS:
                transform.GetChild(0).Rotate(Vector3.up * rotSpeed * Time.deltaTime);
                break;
        }
        transform.Translate(Dir * Speed * Time.deltaTime);

        if (!isPattern)
        {
            if (transform.position.x > 35 || transform.position.x < -32.5f)
                gameObject.SetActive(false);
        }

        if(transform.position.z < -28.5f)
        {
            gameObject.SetActive(false);
            S.GM.Pain += 5;
        }
    }

    private void HpManagemant()
    {
        if (Hp <= 0)
        {
            switch (enemy_type)
            {
                case ENEMY_TYPE.BACTERIA:
                    S.SpawnItem("Score", transform.position, Random.Range(1, 5));
                    S.SpawnItem("Power", transform.position, Random.Range(1, 4));
                    S.GM.KillCount++;
                    break;
                case ENEMY_TYPE.GREM:
                    S.SpawnItem("Score", transform.position, Random.Range(2, 5));
                    S.SpawnItem("Power", transform.position, Random.Range(2, 4));
                    S.GM.KillCount++;
                    break;
                case ENEMY_TYPE.CENCER:
                    S.SpawnItem("Score", transform.position, Random.Range(3, 7));
                    S.SpawnItem("Power", transform.position, Random.Range(3, 6));
                    S.GM.KillCount++;
                    break;
                case ENEMY_TYPE.VIRUS:
                    S.SpawnItem("Score", transform.position, Random.Range(2, 8));
                    S.SpawnItem("Power", transform.position, Random.Range(4, 6));
                    S.GM.KillCount++;
                    break;
            }
            ObjectPool.Get("Explosion", transform.position);
            gameObject.SetActive(false);
        }
    }

    private void Fire()
    {
        if (FireTime >= FireDelay)
        {
            switch (enemy_type)
            {
                case ENEMY_TYPE.GREM:
                    FireDelay = 1.2f;
                    StartCoroutine(GremShot());
                    break;
                case ENEMY_TYPE.CENCER:
                    FireDelay = 3;
                    StopCoroutine(CencerShot());
                    StartCoroutine(CencerShot());
                    break;
                case ENEMY_TYPE.VIRUS:
                    FireDelay = 1f;
                    VirusShot();
                    break;
                default:
                    break;
            }
            FireTime = 0;
        }
        FireTime += Time.deltaTime;
    }

    private IEnumerator GremShot()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 target = (S.player.transform.position - transform.position).normalized;
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(Vector3.zero), 40, Damage, BULLET_TYPE.E_NORMAL);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CencerShot()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 target = (S.player.transform.position - transform.position).normalized;
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, 14, 0)), 15, Damage, BULLET_TYPE.E_NORMAL2);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, 7, 0)), 15, Damage, BULLET_TYPE.E_NORMAL2);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(Vector3.zero), 15, Damage, BULLET_TYPE.E_NORMAL2);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, -7, 0)), 15, Damage, BULLET_TYPE.E_NORMAL2);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, -14, 0)), 15, Damage, BULLET_TYPE.E_NORMAL2);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void VirusShot()
    {
        for (int i = 0; i < 360; i += 20)
        {
            S.Shot("E_Bullet", transform.position, -Vector3.forward, Quaternion.Euler(0, i, 0), 10, Damage, BULLET_TYPE.E_AROUND);
        }
        //    int i = 0;
        //while (true)
        //{
        //    i += 30;
        //    S.Shot("E_Bullet", transform.position, -Vector3.forward, Quaternion.Euler(0, i, 0), 10, 10, BULLET_TYPE.E_AROUND);
        //    yield return new WaitForSeconds(0.1f);
        //    S.Shot("E_Bullet", transform.position, -Vector3.forward, Quaternion.Euler(0, i + 5, 0), 10, 10, BULLET_TYPE.E_AROUND);
        //}
    }
}
