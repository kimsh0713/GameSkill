using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public int Score;
    public int Pain;
    public int KillCount;

    public Text Stage_T;
    public Text HiScore_T;
    public Text Score_T;
    public Slider HP_S;
    public Text HP_T;
    public Slider Pain_S;
    public Text Pain_T;
    public Image Pain_progressbar;
    public Text Power_T;

    public Text KillCount_T;
    public Text Score_T_ingame;

    public bool isSpawnEnemy;
    public float SpawnTime;

    public bool GameOver;

    public GameObject Boss_1;
    public GameObject Boss_2;

    private GameObject boss;

    public PlayableDirector PD;
    public TimelineAsset S_1;
    public TimelineAsset S_2;

    private void Awake()
    {
        S.GM = this;
        PD = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        StartCoroutine(SpawnBoss());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(EenemySpawnStart(3));
        StartCoroutine(Spawnblood());

        if (S.Stage == 1)
            PD.playableAsset = S_1;
        else if (S.Stage == 2)
            PD.playableAsset = S_2;
    }

    private void Update()
    {
        Ui_Update();
        Cheat();
        {
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    float ran = Random.Range(-28, 30);
            //    var obj = ObjectPool.Get<Enemy>("Bacteria", new Vector3(ran, 0, 20));
            //    obj.Hp = 100;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    float ran = Random.Range(-28, 30);
            //    var obj = ObjectPool.Get<Enemy>("Grem", new Vector3(ran, 0, 20));
            //    obj.Hp = 100;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    float ran = Random.Range(-28, 30);
            //    var obj = ObjectPool.Get<Enemy>("Cencer", new Vector3(ran, 0, 20));
            //    obj.Hp = 100;
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha4))
            //{
            //    float ran = Random.Range(-28, 30);
            //    var obj = ObjectPool.Get<Enemy>("Virus", new Vector3(ran, 0, 20));
            //    obj.Hp = 100;
            //}
            //if (Input.GetKeyDown(KeyCode.I))
            //{
            //    S.SpawnItem("Score", new Vector3(0, 0, 20), 10);
            //}
        }
    }

    private void Ui_Update()
    {
        Stage_T.text = "Stage " + S.Stage.ToString();

        Score_T.text = "Score : " + Score.ToString();

        HiScore_T.text = "HiScore : " + S.HiScore.ToString();
        if (Score > S.HiScore)
        {
            S.HiScore = Score;
            HiScore_T.text = "HiScore : " + S.HiScore.ToString();
        }

        HP_S.maxValue = S.player.MaxHp;
        HP_S.value = S.player.Hp;
        HP_T.text = S.player.Hp.ToString() + "/" + S.player.MaxHp.ToString();

        Pain = Mathf.Clamp(Pain, 0, 100);
        Pain_S.maxValue = 100;
        Pain_S.value = Pain;
        Pain_T.text = Pain.ToString() + "/100";

        if (Pain <= 25)
            Pain_progressbar.color = new Color32(131, 255, 81, 255);
        else if (Pain <= 50)
            Pain_progressbar.color = new Color32(255, 255, 81, 255);
        else if (Pain <= 75)
            Pain_progressbar.color = new Color32(255, 171, 81, 255);
        else
            Pain_progressbar.color = new Color32(255, 100, 81, 255);

        Power_T.text = "Power : " + S.player.Power.ToString("N2");

        Score_T_ingame.text = "Score : " + Score.ToString();
        KillCount_T.text = "Kill : " + KillCount.ToString();
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (S.Stage == 1)
                yield return new WaitForSeconds(SpawnTime);
            else if(S.Stage == 2)
                yield return new WaitForSeconds(SpawnTime - 0.5f);
            if (isSpawnEnemy)
            {
                float x = Random.Range(-27f, 27f);
                int ranEnemy = Random.Range(0, 4);
                SpawnTime = Random.Range(0.3f, 4.5f);
                switch (ranEnemy)
                {
                    case 0:
                        float ran = Random.Range(-28, 30);
                        var bac = ObjectPool.Get<Enemy>("Bacteria", new Vector3(ran, 0, 30));
                        bac.Hp = 100 * 4;
                        bac.Damage = 20;
                        break;
                    case 1:
                        float ran_1 = Random.Range(-28, 30);
                        var grem = ObjectPool.Get<Enemy>("Grem", new Vector3(ran_1, 0, 30));
                        grem.Hp = 150 * 4;
                        grem.Damage = 8;
                        break;
                    case 2:
                        float ran_2 = Random.Range(-28, 30);
                        var cen = ObjectPool.Get<Enemy>("Cencer", new Vector3(ran_2, 0, 30));
                        cen.Hp = 180 * 4;
                        cen.Damage = 10;
                        break;
                    case 3:
                        float ran_3 = Random.Range(-28, 30);
                        var vir = ObjectPool.Get<Enemy>("Virus", new Vector3(ran_3, 0, 30));
                        vir.Hp = 200 * 4;
                        vir.Damage = 12;
                        break;
                }
            }
        }
    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(180);

        if (S.Stage == 1)
        {
            boss = Instantiate(Boss_1, new Vector3(0, 0, 40), Quaternion.identity);
        }
        else if (S.Stage == 2)
        {
            boss = Instantiate(Boss_2, new Vector3(0, 0, 45), Quaternion.identity);
        }
        isSpawnEnemy = false;
    }

    private IEnumerator EenemySpawnStart(float time)
    {
        if (S.Stage == 1)
            yield return new WaitForSeconds(time);
        else if (S.Stage == 2)
            yield return new WaitForSeconds(time - 0.5f);
        isSpawnEnemy = true;
    }

    private IEnumerator Spawnblood()
    {
        while (true)
        {
            if (S.Stage == 1)
                yield return new WaitForSeconds(4);
            else if (S.Stage == 2)
                yield return new WaitForSeconds(4 + 1);

            float ranPos = Random.Range(-28, 30);
            int ran = Random.Range(0, 10);
            if (ran < 5)
            {
                var red = ObjectPool.Get<Blood>("Red", new Vector3(ranPos, 0, 28));
                red.type = BlOOD_TYPE.RED;
            }
            else
            {
                var white = ObjectPool.Get<Blood>("White", new Vector3(ranPos, 0, 28));
                white.type = BlOOD_TYPE.WHITE;
            }
        }
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NextStage();
            //Stage
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            S.player.Power += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            S.player.isHurt_C = false;
            //StartCoroutine(S.player.Sh)
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            S.player.isHurt_C = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ObjectPool.AllObjSetFalse();
            if (boss != null)
                boss.GetComponent<Covid>().Hp = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            S.player.Hp = 100;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Pain = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            float ranPos = Random.Range(-28, 30);
            var red = ObjectPool.Get<Blood>("Red", new Vector3(ranPos, 0, 28));
            red.type = BlOOD_TYPE.RED;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            float ranPos = Random.Range(-28, 30);
            var white = ObjectPool.Get<Blood>("White", new Vector3(ranPos, 0, 28));
            white.type = BlOOD_TYPE.WHITE;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //    SceneManager.LoadScene(1);
    }

    public void NextStage()
    {
        if (S.Stage == 1)
            S.Stage = 2;
        else
            S.Stage = 1;
        SceneManager.LoadScene(1);
    }
}
