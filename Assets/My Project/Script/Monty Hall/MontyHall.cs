using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MontyHall : MonoBehaviour
{
    public bool[] Hall = new bool[3];

    public int Count;
    public int WIN;
    public int LOSE;

    public int Choice = 0;
    public int Checker = 0;
    public int LastChoice = 0;

    public float per;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Monty();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            for(int i = 0; i < 10; i ++)
                Monty();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < 100; i++)
                Monty();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < 1000; i++)
                Monty();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Count = 0;
            WIN = 0;
            LOSE = 0;
        }
    }

    private void Monty()
    {
        Checker = 0;
        Choice = 0;
        LastChoice = 0;
        Hall = new bool[3];

        Count++;
        //랜덤으로 차위치 정해줌
        int Ran = Random.Range(0, 3);
        Hall[Ran] = true;

        //하나를 무작위로 선택
        Choice = Random.Range(0, 3);

        //염소가 있는 방을 하나 알려줌
        for(int i = 0; i < 3; i++)
        {
            if(!Hall[i] && i != Choice)
            {
                Checker = i;
                break;
            }
        }

        // 다시 한번 선택의 기회를 줌 / 지금은 무조건 바꾸어야함
        for(int i = 0; i < 3; i++)
        {
            if (i != Choice && i != Checker)
                LastChoice = i;
        }

        // 차를 뽑았는지 못뽑았는지 확인
        if (Hall[LastChoice] == true)
            WIN++;
        else
            LOSE++;

        per = WIN * 100 / Count;
        print("Count : " + Count + ", " + per + "%");
    }
}
