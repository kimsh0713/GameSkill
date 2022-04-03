using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    int ran = 0;

    private void Start()
    {
        ran = Random.Range(-1, 1);
    }

    private void Update()
    {
        transform.Translate(0, 0, ran);
        transform.Rotate(5, 10, 0);
    }
}
