using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : PlayerBullet
{
    private float timerMax = 0;
    private float timerCur = 0;
    private float Speed;
    private bool isBezier = false;

    private Transform EndPos;

    Vector3[] points = new Vector3[3];

    public void Init(Transform startTrans, Transform endTrans, float speed, float pointDistance)
    {
        isBezier = true;
        Speed = speed;
        timerCur = 0;

        timerMax = Random.Range(0.7f, 1);

        points[0] = startTrans.position;

        points[1] = startTrans.position +
            (pointDistance * Random.Range(-1f, 1f) * startTrans.right) +
            (pointDistance * Random.Range(-0.1f, 0.5f) * startTrans.up) +
            (pointDistance * Random.Range(-0.3f, -1.3f) * startTrans.forward);

        EndPos = endTrans;

        transform.position = startTrans.position;
    }

    private void Update()
    {
        if(isBezier)
        {
            if (timerCur > timerMax)
                return;

            points[2] = EndPos.position;

            timerCur += Time.deltaTime * Speed;

            transform.position = new Vector3(
                BezierCurve(points[0].x, points[1].x, points[2].x),
                BezierCurve(points[0].y, points[1].y, points[2].y),
                BezierCurve(points[0].z, points[1].z, points[2].z));
        }
    }

    private float BezierCurve(float a, float b, float c)
    {
        float t = timerCur / timerMax;

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);

        return Mathf.Lerp(ab, bc, t);
    }
}
