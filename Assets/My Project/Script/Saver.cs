using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class Save_Data
{
    public int Hp;
    public float Speed;
    public int Damage;
}

public class Saver : MonoBehaviour
{
    public static Saver ins;

    private void Awake() => ins = this;

    public Save_Data save;
    public Save_Data get;

    public string GetPath(string fileName) => $@"C:\Users\pc\Desktop\{fileName}.json";

    public void Write<T>(string fileName, T obj, bool isForce = false)
    {
        string path = GetPath(fileName);
        if (!File.Exists(path) || isForce)
            File.WriteAllText(path, JsonUtility.ToJson(obj));
    }

    public T Read<T>(string fileName)
    {
        return JsonUtility.FromJson<T>(File.ReadAllText(GetPath(fileName)));
    }
}
