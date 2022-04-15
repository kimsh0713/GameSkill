using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covid : MonoBehaviour
{
    [Header("Stauts")]
    public int Hp;
    public float Speed;
    public int Damage;

    [Header("Other")]
    public float rotspeed;
    public GameObject body;
    bool isSpawn;
    public bool isFight;

    public float FireDelay;
    public float FireTime;
    public int index;

    public GameObject[] mini;

    #region UnityMestod
    private void Update()
    {
        if (transform.position.z > 15)
        {
            transform.Translate(-Vector3.forward * 20 * Time.deltaTime);
            isFight = true;
        }

        if (isFight)
        {
            if (Hp <= 0)
            {
                StartCoroutine(S.CameraShake(4f, 1f));
                StartCoroutine(Explosion(15));
                isFight = false;
            }


            if (FireTime >= FireDelay)
            {
                index = Random.Range(0, 4);
                switch (index)
                {
                    case 0:
                        StartCoroutine(FireArange());
                        FireDelay = 5;
                        break;
                    case 1:
                        StartCoroutine(FireGravity());
                        FireDelay = 4;
                        break;
                    case 2:
                        StartCoroutine(Santan());
                        FireDelay = 2;
                        break;
                    case 3:
                        FireDelay = 5;
                        StartCoroutine(ONEDOT());
                        break;
                }
                FireTime = 0;
            }
            FireTime += Time.deltaTime;

            body.transform.Rotate(Vector3.down * rotspeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (isFight)
        {
            if (col.CompareTag("P_Bullet"))
            {
                col.gameObject.SetActive(false);
                Hp -= col.GetComponent<Bullet>().Damage;
                S.GM.Score += 10;
            }
        }
    }
    #endregion

    private IEnumerator Explosion(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            ObjectPool.Get("Explosion_2", transform.position + new Vector3(Random.Range(-10f, 10f), 10, Random.Range(-10f, 10f)));
        }
        S.SpawnItem("Score", transform.position, Random.Range(30, 50));
        S.SpawnItem("Power", transform.position, Random.Range(15, 20));
        S.GM.PD.Play();
        Destroy(gameObject);
    }

    private IEnumerator FireArange()
    {
        for (int count = 0; count < 10; count++)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 360; i += 20)
            {
                for (int j = 0; j < mini.Length; j++)
                    S.Shot("E_Bullet", mini[j].transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, i, 0)), 10, 10, BULLET_TYPE.B_AROUND);
            }
        }
    }

    private IEnumerator FireGravity()
    {
        for (int i = 0; i < Random.Range(50, 60); i++)
        {
            yield return new WaitForSeconds(0.03f);
            var obj = ObjectPool.Get<GravityBullet>("Gravity", transform.position);
            obj.Damage = 10;
            //S.Shot("Gravity", transform.position, Vector3.forward, Quaternion.identity, 10, 10, BULLET_TYPE.END);
        }
    }

    private IEnumerator Santan()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 target = (S.player.transform.position - transform.position).normalized;
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, 32, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, 24, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, 16, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, 8, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(Vector3.zero), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, -8, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, -16, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, -24, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(new Vector3(0, -32, 0)), 15, Damage, BULLET_TYPE.B_AROUND);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator ONEDOT()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 target = (S.player.transform.position - transform.position).normalized;
            S.Shot("E_Bullet", transform.position, target, Quaternion.Euler(Vector3.zero), 40, Damage, BULLET_TYPE.B_AROUND);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
