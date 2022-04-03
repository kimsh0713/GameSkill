using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    POWER,
    SCORE,
    END
}

public class Items : MonoBehaviour
{
    Rigidbody rigid;

    public ITEM_TYPE item_type;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        float ranX = Random.Range(-5f, 5f);
        float ranY = Random.Range(5f, 10f);
        rigid.AddForce(ranX, 0, ranY, ForceMode.Impulse);
    }

    private void Update()
    {
        if (Vector3.Distance(S.player.transform.position, transform.position) < 10)
        {
            var dir = (S.player.transform.position - transform.position).normalized;
            rigid.AddForce(dir * 35);
        }

        if (transform.position.x < -33 || transform.position.x > 33 ||
            transform.position.z < -30 || transform.position.z > 28)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        ObjectPool.ReturnToPool(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PlayerGet"))
        {
            if (item_type == ITEM_TYPE.POWER)
                S.player.Power += 0.02d;
            else if (item_type == ITEM_TYPE.SCORE)
                S.GM.Score += 500;
            gameObject.SetActive(false);
        }
    }
}
