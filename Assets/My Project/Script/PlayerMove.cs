using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;

    public GameObject Body;
    public GameObject Water;

    private void Start()
    {
        StartCoroutine(ERotation());
    }

    private void Update()
    {
        PlayerMovement();
        MovingWater();
    }

    private void PlayerMovement()
    {
        float h = 0;
        float v = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            h = Input.GetAxisRaw("Horizontal") * MoveSpeed * 2 * Time.deltaTime;
            v = Input.GetAxisRaw("Vertical") * MoveSpeed * 2 * Time.deltaTime;
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal") * MoveSpeed * Time.deltaTime;
            v = Input.GetAxisRaw("Vertical") * MoveSpeed * Time.deltaTime;
        }

        v = Mathf.Clamp(v, -0.07f, 1);

        Vector3 movedir = new Vector3(h, 0, v) + new Vector3(0, 0, 50 * Time.deltaTime);
        transform.Translate(movedir);
    }

    private void MovingWater()
    {
        Water.transform.position = new Vector3(Water.transform.position.x, Water.transform.position.y, transform.position.z);
    }

    private IEnumerator ERotation()
    {
        float x;

        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");
            Body.transform.rotation = Quaternion.Lerp(Body.transform.rotation, Quaternion.Euler(Vector3.forward * x * 25), Time.deltaTime * 10);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
