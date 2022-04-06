using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Vairables")]
    public int Stage;
    public int HiScore;
    public int Score;
    public int Pain;

    [Header("UI Objects")]
    public Text HiScore_T;
    public Text Score_T;
    public Image[] PlayerHpImg;
    public Text PlayerDamage_T;
    public Text DamageF_T;

    #region UnityMestod
    private void Awake()
    {
        S.GM = this;
    }
    private void Start()
    {
        Stage = 1;
    }
    private void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i < S.player.HP)
                PlayerHpImg[i].color = new Color32(141, 255, 220, 190);
            else
                PlayerHpImg[i].color = new Color32(141, 255, 220, 80);
        }

        Score_T.text = "Score : " + Score.ToString();
        if (Score >= HiScore)
            HiScore = Score;
        HiScore_T.text = "HiScore : " + HiScore.ToString();
        PlayerDamage_T.text = "Power : " + S.player.Power.ToString("F2");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int ran = Random.Range(-20, 20);
            var enemy = ObjectPool.SpawnPoolObj<Enemy>("Germ", new Vector3(ran, 0, 20));
            enemy.HP = 100;
            enemy.enemy_type = ENEMY_TYPE.GERM;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            int ran = Random.Range(-20, 20);
            var enemy = ObjectPool.SpawnPoolObj<Enemy>("Bacteria", new Vector3(ran, 0, 20));
            enemy.HP = 150;
            enemy.enemy_type = ENEMY_TYPE.BACTERIA;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int ran = Random.Range(-20, 20);
            var enemy = ObjectPool.SpawnPoolObj<Enemy>("Cencer", new Vector3(ran, 0, 20));
            enemy.HP = 180;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            int ran = Random.Range(-20, 20);
            var enemy = ObjectPool.SpawnPoolObj<Enemy>("Virus", new Vector3(ran, 0, 20));
            enemy.HP = 240;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
            Saver.ins.Write("Status", Saver.ins.save);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            Saver.ins.Write("Status", Saver.ins.save, true);
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Saver.ins.get = Saver.ins.Read<Save_Data>("Status");
    }
    #endregion
}


        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    Saver.ins.Write("Temp", Saver.ins.save);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    Saver.ins.Write("Temp", Saver.ins.save, true);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    Saver.ins.get = Saver.ins.Read<Save_Data>("Temp");
        //}