using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Border")
            Destroy(gameObject);
    }
}
