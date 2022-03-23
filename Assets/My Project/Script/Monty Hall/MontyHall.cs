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
        //�������� ����ġ ������
        int Ran = Random.Range(0, 3);
        Hall[Ran] = true;

        //�ϳ��� �������� ����
        Choice = Random.Range(0, 3);

        //���Ұ� �ִ� ���� �ϳ� �˷���
        for(int i = 0; i < 3; i++)
        {
            if(!Hall[i] && i != Choice)
            {
                Checker = i;
                break;
            }
        }

        // �ٽ� �ѹ� ������ ��ȸ�� �� / ������ ������ �ٲپ����
        for(int i = 0; i < 3; i++)
        {
            if (i != Choice && i != Checker)
                LastChoice = i;
        }

        // ���� �̾Ҵ��� ���̾Ҵ��� Ȯ��
        if (Hall[LastChoice] == true)
            WIN++;
        else
            LOSE++;

        per = WIN * 100 / Count;
        print("Count : " + Count + ", " + per + "%");
    }
}
