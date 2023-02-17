using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Func_Json : MonoBehaviour
{
    public static Func_Json instance;
    public List<string> recordName = new List<string>();
    public List<Vector3> recordNamePos = new List<Vector3>();
    void Awake()
    {
        instance = this;
    }
}
[System.Serializable]
public class SaveData
{
    public List<string> recordName = new List<string>();
    public List<Vector3> recordNamePos = new List<Vector3>();
}