using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BULLET_TYPE
{
    P_NORMAL,
    P_HOMING,
    E_NORMAL,
    E_NORMAL2,
    E_AROUND,
    B_AROUND,
    END
}

public class Bullet : MonoBehaviour
{
    public float Speed;
    public Vector3 Dir;
    public Quaternion rot;
    public int Damage;
    public BULLET_TYPE type;

    private TrailRenderer tr;
    public Gradient[] gradients;

    #region UnityMestod
    private void Awake()
    {
        tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        BulletInit();
        BulletMove();
    }
    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }
    #endregion

    private void BulletInit()
    {
        tr.colorGradient = gradients[(int)type];
        switch (type)
        {
            case BULLET_TYPE.P_NORMAL:
                tr.time = 0.05f;
                tr.startWidth = 0f;
                tr.endWidth = 0f;
                break;
            case BULLET_TYPE.P_HOMING:
                tr.time = 0.05f;
                tr.startWidth = 0f;
                tr.endWidth = 0f;
                break;
            case BULLET_TYPE.E_NORMAL:
                tr.time = 0.08f;
                tr.startWidth = 0f;
                tr.endWidth = 0f;
                break;
            case BULLET_TYPE.E_NORMAL2:
                tr.time = 0.12f;
                tr.startWidth = 0f;
                tr.endWidth = 0f;
                break;
            case BULLET_TYPE.B_AROUND:
            case BULLET_TYPE.E_AROUND:
                tr.time = 0.1f;
                tr.startWidth = 1f;
                tr.endWidth = 1f;
                break;
            default:
                break;
        }
    }

    private void BulletMove()
    {
        transform.rotation = rot;

        if (type == BULLET_TYPE.P_HOMING)
        {
            var col = Physics.OverlapSphere(transform.position, 50, 1 << 8);
            if (col.Length != 0)
            {
                GameObject target = col[0].gameObject;
                float dis = Vector3.Distance(transform.position, target.transform.position);
                foreach (var Found in col)
                {
                    float _dis = Vector3.Distance(transform.position, Found.transform.position);
                    if (_dis < dis)
                    {
                        target = Found.gameObject;
                        dis = _dis;
                    }
                    Dir = (target.transform.position - transform.position).normalized;
                }
            }
        }
        transform.Translate(Dir * Speed * Time.deltaTime);

        if (transform.position.x > 35 || transform.position.x < -32.5f || transform.position.z > 28 || transform.position.z < -28.5f)
            gameObject.SetActive(false);
    }
}
