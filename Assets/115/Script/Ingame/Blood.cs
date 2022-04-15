using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlOOD_TYPE
{
    RED,
    WHITE,
}

public class Blood : MonoBehaviour
{
    public BlOOD_TYPE type;

    private void OnTriggerEnter(Collider col)
    {
        if (type == BlOOD_TYPE.WHITE)
        {
            if (col.CompareTag("P_Bullet") || col.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                        S.SpawnItem("Score", transform.position, 10);
                        break;
                    case 1:
                        S.SpawnItem("Power", transform.position, 10);
                        break;
                    case 2:
                        S.SpawnItem("Heal", transform.position, 1);
                        break;
                    case 3:
                        S.SpawnItem("Pain", transform.position, 1);
                        break;
                    case 4:
                        S.SpawnItem("Shield", transform.position, 1);
                        break;
                }
            }
        }
        else if(type == BlOOD_TYPE.RED)
        {
            if (col.CompareTag("P_Bullet") || col.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                S.GM.Pain += 5;
            }
        }
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * 10 * Time.deltaTime);

        if (transform.position.x > 35 || transform.position.x < -32.5f)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }
}
