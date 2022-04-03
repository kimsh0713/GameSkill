using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covid_19 : MonoBehaviour
{
    public int HP;
    public float Speed;
    public bool isHurt;
    public float SpinSpeed;

    public GameObject bodys;
    public GameObject[] MiniBodys;

    private bool isSpawn = false;

    #region UnityMestod
    private void Start()
    {
        isSpawn = true;
        isHurt = false;
    }
    private void Update()
    {
        HpManagement();
        MoveManagement();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (isHurt)
        {
            if (col.CompareTag("PlayerBullet"))
            {
                HP -= col.GetComponent<PlayerBullet>().Damage;
                col.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    private void HpManagement()
    {
        if (MiniBodys[0].activeSelf == false && MiniBodys[1].activeSelf == false && MiniBodys[2].activeSelf == false && MiniBodys[3].activeSelf == false)
            isHurt = true;

        if (HP <= 0)
            Destroy(gameObject);
    }

    private void MoveManagement()
    {
        //spin
        bodys.transform.Rotate(0, SpinSpeed, 0);

        if (isSpawn)
        {
            if (transform.position.z >= 15)
            {
                transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
            }
        }
    }
}
