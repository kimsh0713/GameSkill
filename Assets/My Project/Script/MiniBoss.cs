using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniBoss : MonoBehaviour
{
    public int HP;
    public int MaxHP;

    public Slider Hpbar;

    private void Start()
    {
        StartCoroutine(Shot());
        Hpbar = Instantiate(Hpbar, GameObject.Find("IngameCanvas").transform);
        Hpbar.maxValue = MaxHP;
    }
    private void Update()
    {
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            Hpbar.gameObject.SetActive(false);
        }

        Hpbar.transform.position = transform.position + new Vector3(0, 10, 3);
        Hpbar.value = HP;
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(4f);
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            for (int i = 0; i < 360; i += 20)
            {
                var bullet = ObjectPool.SpawnPoolObj<EnemyBullet>("E_Bullet", transform.position);
                bullet.Speed = 18;
                bullet.Dir = -Vector3.forward;
                bullet.transform.Rotate(0, i, 0);
                bullet.GetComponent<TrailRenderer>().startColor = new Color32(20, 20, 240, 255);
                bullet.GetComponent<TrailRenderer>().Clear();
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PlayerBullet"))
        {
            HP -= col.GetComponent<PlayerBullet>().Damage;
            col.gameObject.SetActive(false);
        }
    }
}
