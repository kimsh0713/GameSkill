using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_TYPE
{
    GERM,
    BACTERIA,
    CENCER,
    VIRUS,
}

public class Enemy : MonoBehaviour
{
    public ENEMY_TYPE enemy_type;

    public float FireDelay = 3;
    private float FireTime;

    public int HP;
    public float Speed;

    #region UnityMestod
    private void Update()
    {
        Move();
        HpManagement();
        Fire();
    }

    private void OnDisable()
    {
        ObjectPool.ReturnToPool(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PlayerBullet"))
        {
            HP -= col.GetComponent<PlayerBullet>().Damage;
            col.gameObject.SetActive(false);
            S.GM.Score += 10;
        }
    }
    #endregion

    #region Functions
    private void HpManagement()
    {
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            switch (enemy_type)
            {
                case ENEMY_TYPE.GERM:
                    S.SpawnItem("Power", transform.position, 3, ITEM_TYPE.POWER);
                    S.SpawnItem("Score", transform.position, 3, ITEM_TYPE.SCORE);
                    break;
                case ENEMY_TYPE.BACTERIA:
                    S.SpawnItem("Power", transform.position, 5, ITEM_TYPE.POWER);
                    S.SpawnItem("Score", transform.position, 4, ITEM_TYPE.SCORE);
                    break;
                case ENEMY_TYPE.CENCER:
                    S.SpawnItem("Power", transform.position, 4, ITEM_TYPE.POWER);
                    S.SpawnItem("Score", transform.position, 4, ITEM_TYPE.SCORE);
                    break;
                case ENEMY_TYPE.VIRUS:
                    S.SpawnItem("Power", transform.position, 6, ITEM_TYPE.POWER);
                    S.SpawnItem("Score", transform.position, 5, ITEM_TYPE.SCORE);
                    break;
            }
        }
    }
    private void Move()
    {
        transform.Translate(-Vector3.forward * Speed * Time.deltaTime);

        if (transform.position.z <= -28)
        {
            S.GM.Pain += 10;
            gameObject.SetActive(false);
        }
        if (enemy_type == ENEMY_TYPE.CENCER)
        {
            transform.Rotate(0, 1f, 0);
            transform.position += new Vector3(0, 0, -1) * (Speed / 2) * Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (FireTime >= FireDelay)
        {
            switch (enemy_type)
            {
                case ENEMY_TYPE.GERM:
                    FireDelay = 2.5f;
                    GermShot();
                    break;
                case ENEMY_TYPE.BACTERIA:
                    FireDelay = 4f;
                    StartCoroutine(BactriaShot());
                    break;
                case ENEMY_TYPE.CENCER:
                    FireDelay = 4.5f;
                    StartCoroutine(CencerShot());
                    break;
                case ENEMY_TYPE.VIRUS:
                    FireDelay = 3.5f;
                    VirusShot();
                    break;
            }
            FireTime = 0;
        }
        FireTime += Time.deltaTime;
    }
    #endregion


    #region EnemyShots
    private void GermShot()
    {
        var bullet = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
        var dir = (S.player.transform.position - transform.position).normalized;
        bullet.Dir = dir;
        bullet.Speed = 12;
        bullet.GetComponent<TrailRenderer>().Clear();
    }
    private IEnumerator BactriaShot()
    {
        for (int i = 0; i < 5; i++)
        {
            GermShot();
            yield return new WaitForSeconds(0.4f);
        }
    }
    private IEnumerator CencerShot()
    {
        for (int i = 0; i < 3; i++)
        {
            var bullet1 = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
            var bullet2 = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
            var bullet3 = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
            var bullet4 = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
            var bullet5 = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
            var dir = (S.player.transform.position - transform.position).normalized;
            bullet1.Dir = dir;
            bullet2.Dir = dir;
            bullet3.Dir = dir;
            bullet4.Dir = dir;
            bullet5.Dir = dir;
            bullet1.transform.Rotate(0, 15, 0);
            bullet2.transform.Rotate(0, -15, 0);
            bullet3.transform.Rotate(0, 30, 0);
            bullet4.transform.Rotate(0, -30, 0);
            bullet1.GetComponent<TrailRenderer>().Clear();
            bullet2.GetComponent<TrailRenderer>().Clear();
            bullet3.GetComponent<TrailRenderer>().Clear();
            bullet4.GetComponent<TrailRenderer>().Clear();
            bullet5.GetComponent<TrailRenderer>().Clear();
            yield return new WaitForSeconds(0.4f);
        }
    }
    private void VirusShot()
    {
        for (int i = 0; i < 360; i += 40)
        {
            var bullet = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
            bullet.Dir = -Vector3.forward;
            bullet.Speed = 15;
            bullet.transform.Rotate(0, i, 0);
            bullet.GetComponent<TrailRenderer>().startColor = new Color32(30, 30, 240, 255);
            bullet.GetComponent<TrailRenderer>().Clear();
        }
    }
    #endregion
}
