using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    Vector2 lockInput, screenCenter, mouseDistance;

    private void Awake()
    {
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        lockInput = Input.mousePosition;

        mouseDistance = (lockInput - screenCenter) / screenCenter;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(mouseDistance.y * 30 * Time.deltaTime, mouseDistance.x * 30 * Time.deltaTime, 0);
    }
}
