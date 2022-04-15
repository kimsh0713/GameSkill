using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Stauts")]
    public int Hp;
    public int MaxHp;
    public float Speed;
    public float Power;
    public float FireDelay;
    private float FireTime;

    public bool isHurt;
    public bool isHurt_C;
    public bool isHurt_S;

    [Header("Other objects")]
    public GameObject body;
    public GameObject ColliderMark;
    public GameObject Shield_obj;

    [Header("Other System")]
    public PlayableDirector PD;

    #region UnityMestod
    private void Awake()
    {
        S.player = this;
    }

    private void Start()
    {
        Hp = MaxHp;
        StartCoroutine(ERotation());
        StartCoroutine(Shield(3, false));
    }

    private void Update()
    {

        Hp = Mathf.Clamp(Hp, 0, MaxHp);

        if (!S.GM.GameOver)
        {
            Move();
            Fire();

            if (Hp == 0 || S.GM.Pain == 100)
            {
                StartCoroutine(Die());
                S.GM.GameOver = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (isHurt && isHurt_C && isHurt_S)
        {
            if (col.CompareTag("E_Bullet"))
            {
                Hp -= col.GetComponent<Bullet>().Damage;
                col.gameObject.SetActive(false);
                StartCoroutine(S.CameraShake(0.5f, 1));
                ObjectPool.Get("Explosion", transform.position);
                StartCoroutine(Hurt(1.5f));
                if (Power >= 5) Power -= 1;
            }
            if (col.CompareTag("GravityBullet"))
            {
                Hp -= col.GetComponent<GravityBullet>().Damage;
                col.gameObject.SetActive(false);
                StartCoroutine(S.CameraShake(0.5f, 1));
                ObjectPool.Get("Explosion", transform.position);
                StartCoroutine(Hurt(1.5f));
                if (Power >= 5) Power -= 1;
            }
            if (col.CompareTag("Enemy"))
            {
                Hp -= (col.GetComponent<Enemy>().Damage / 2);
                col.gameObject.SetActive(false);
                StartCoroutine(S.CameraShake(0.5f, 1));
                ObjectPool.Get("Explosion", transform.position);
                StartCoroutine(Hurt(1.5f));
                if (Power >= 5) Power -= 1;
            }
        }
    }
    #endregion

    private void Move()
    {
        float h = 0, v = 0;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        var curPos = transform.position;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.6f;
            ColliderMark.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            ColliderMark.SetActive(false);
        }

        curPos += new Vector3(h, 0, v) * Speed * Time.deltaTime;

        curPos.x = Mathf.Clamp(curPos.x, -28f, 28);
        curPos.z = Mathf.Clamp(curPos.z, -22.5f, 21.5f);

        transform.position = curPos;
    }

    private IEnumerator ERotation()
    {
        float x = 0;
        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");

            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(0, 0, x * 50), Time.deltaTime * 13);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (FireTime >= FireDelay)
            {
                switch ((int)Power)
                {
                    case 1:
                        S.Shot("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, Quaternion.Euler(Vector3.zero), 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(Vector3.zero), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, Quaternion.Euler(Vector3.zero), 100, 10, BULLET_TYPE.P_NORMAL);
                        break;
                    case 2:
                        S.Shot("P_Bullet", transform.position + new Vector3(1, 0, 0), Vector3.forward, Quaternion.Euler(Vector3.zero), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position + new Vector3(0.4f, 0, 0), Vector3.forward, Quaternion.Euler(Vector3.zero), 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(Vector3.zero), 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-0.4f, 0, 0), Vector3.forward, Quaternion.Euler(Vector3.zero), 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-1, 0, 0), Vector3.forward, Quaternion.Euler(Vector3.zero), 60, 10, BULLET_TYPE.P_HOMING);
                        break;
                    case 3:
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, 10, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(1f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.identity, 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-1f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, -10, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        break;
                    case 4:
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, 20, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, 10, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(1f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-1f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, -10, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, -20, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        break;
                    case 5:
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, 30, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, 20, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, 10, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position + new Vector3(1.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(0.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(1f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-0.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-1f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position + new Vector3(-1.5f, 0, 0), Vector3.forward, Quaternion.identity, 100, 10, BULLET_TYPE.P_NORMAL);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, -10, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, -20, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        S.Shot("P_Bullet", transform.position, Vector3.forward, Quaternion.Euler(new Vector3(0, -30, 0)), 60, 10, BULLET_TYPE.P_HOMING);
                        break;
                }
                FireTime = 0;
            }

        }

        Power = Mathf.Clamp(Power, 1f, 5f);

        FireTime += Time.deltaTime;
    }

    public IEnumerator Shield(float time, bool on = true)
    {
        isHurt_S = false;
        if (on)
            Shield_obj.SetActive(true);

        yield return new WaitForSeconds(time);
        isHurt_S = true;
        if (on)
            Shield_obj.SetActive(false);
    }

    public IEnumerator Hurt(float time)
    {
        isHurt = false;
        yield return new WaitForSeconds(time);
        isHurt = true;
    }

    private IEnumerator Die()
    {
        isHurt = false;

        float time = 2;
        while (time > 0.0f)
        {
            yield return null;
            time -= Time.deltaTime;
            var cam_camera = new Vector3(Camera.main.transform.position.x, 50, Camera.main.transform.position.z);
            cam_camera = Vector3.Lerp(cam_camera, transform.position, Time.deltaTime);
            Camera.main.transform.position = cam_camera;
        }
        StartCoroutine(S.CameraShake(3f, 2, false));
        for (int i = 0; i < 20; i++)
        {
            ObjectPool.Get("Explosion_2", transform.position + new Vector3(Random.Range(-10f, 10f), 10, Random.Range(-10f, 10f)));
            yield return new WaitForSeconds(0.1f);
        }
        PD.Play();
    }

    public void Ending()
    {
        transform.position = new Vector3(0, -500, 0);
        PD.Play();
    }

    public void Gotitle()
    {
        SceneManager.LoadScene(0);
    }
}
