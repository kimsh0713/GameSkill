using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMap : MonoBehaviour
{
    public float speed;
    public float offset = 0;
    private MeshRenderer Mrender;

    #region UnityMestod
    private void Start()
    {
        Mrender = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        offset -= Time.deltaTime * speed;

        Mrender.material.SetTextureOffset("_BumpMap", new Vector2(0, offset));
    }
    #endregion
}
