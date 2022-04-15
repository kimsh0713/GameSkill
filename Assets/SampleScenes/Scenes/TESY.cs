using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESY : MonoBehaviour
{
    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            for (int i = 0; i < 10; i++)
                Instantiate(test, new Vector3(0, 10, 0), Quaternion.identity);
        }
    }
}
