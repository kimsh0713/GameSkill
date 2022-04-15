using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_TYPE
{
    SCORE,
    POWER,
    HEAL,
    SHIELD,
    PAIN,
    END
}
public class Items : MonoBehaviour
{
    public float Speed;
    public ITEM_TYPE type;
    private Rigidbody rigid;

    public float x, z;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Vector3.Distance(S.player.transform.position,transform.position) < 10)
        {
            var dir = (S.player.transform.position - transform.position).normalized;
            rigid.AddForce(dir * Speed);
        }

        if (transform.position.x > 40 || transform.position.x < -37f || transform.position.z > 33 || transform.position.z < -32f)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        x = Random.Range(-5f, 5f);
        z = Random.Range(3f, 9f);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(new Vector3(x, 0, z), ForceMode.Impulse);
    }

    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("GetItem"))
        {
            switch (type)
            {
                case ITEM_TYPE.SCORE:
                    S.GM.Score += 800;
                    break;
                case ITEM_TYPE.POWER:
                    S.player.Power += 0.01f;
                    break;
                case ITEM_TYPE.HEAL:
                    S.player.Hp += 10;
                    break;
                case ITEM_TYPE.SHIELD:
                    //StopCoroutine(S.player.Shield(3f));
                    //StartCoroutine(S.player.(3f));
                    break;
                case ITEM_TYPE.PAIN:
                    S.GM.Pain -= 10;
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
