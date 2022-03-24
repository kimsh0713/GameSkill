using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2D : MonoBehaviour
{
    float h = 0, v = 0;
    public GameObject body;
    public float Speed;

    private void Start()
    {
        StartCoroutine(ERotation());
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h,0,v) * Speed * Time.deltaTime);
    }

    private IEnumerator ERotation()
    {
        float x = 0;
        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, Quaternion.Euler(Vector3.forward * x * 25), Time.deltaTime * 15);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
