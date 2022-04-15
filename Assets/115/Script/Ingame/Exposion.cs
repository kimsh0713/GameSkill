using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposion : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Explosion());
    }

    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
