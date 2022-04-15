using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMap : MonoBehaviour
{
    private MeshRenderer mesh;

    public float Speed;
    public float offset;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));
        offset -= Time.deltaTime * Speed;
    }
}
