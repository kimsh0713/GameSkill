using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(Return());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        ObjectPool.Return(gameObject);
    }

    IEnumerator Return()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
