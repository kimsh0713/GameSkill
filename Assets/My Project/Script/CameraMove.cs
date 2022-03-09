using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 50 * Time.deltaTime));
    }
}
