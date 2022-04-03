using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BULLET_TYPE
{
    NORMAL,
    HOMING,
    BEZIER,
    END
}

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public Vector3 Dir;
    public int Damage;
    public BULLET_TYPE bullet_type;

    private TrailRenderer Trender;

    #region UnityMestod
    private void Awake()
    {
        Trender = GetComponent<TrailRenderer>();
    }
    private void Update()
    {
        switch (bullet_type)
        {
            case BULLET_TYPE.NORMAL:
                Trender.startColor = Color.cyan;
                Trender.endColor = Color.blue;
                Trender.time = 0.04f;
                transform.Translate(Dir * Speed * Time.deltaTime);
                break;
            case BULLET_TYPE.HOMING:
                Trender.startColor = Color.magenta;
                Trender.endColor = Color.magenta;
                Trender.time = 0.08f;

                var col = Physics.OverlapSphere(transform.position, 50, 1 << 8);
                if (col.Length != 0)
                {
                    GameObject target = col[0].gameObject;
                    float dis = Vector3.Distance(transform.position, target.transform.position);
                    foreach (var found in col)
                    {
                        float _dis = Vector3.Distance(transform.position, found.transform.position);
                        if (_dis < dis)
                        {
                            target = found.gameObject;
                            dis = _dis;
                        }
                    }
                    Dir = (target.transform.position - transform.position).normalized;
                    transform.Translate(Dir * Speed * Time.deltaTime);
                }
                else
                    transform.Translate(Dir * Speed * Time.deltaTime);
                break;
            case BULLET_TYPE.BEZIER:
                break;
            case BULLET_TYPE.END:
                break;
            default:
                break;
        }

        if (transform.position.x < -33 || transform.position.x > 33 ||
            transform.position.z < -30 || transform.position.z > 28)
            gameObject.SetActive(false);
    }
    #endregion

    private void OnDisable()
    {
        Trender.Clear();
        ObjectPool.ReturnToPool(gameObject);
    }
}
