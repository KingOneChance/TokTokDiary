using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data_JsonMGR : MonoBehaviour
{
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "dongfoo.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            Debug.Log("no File");
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                for (int i = 0; i < saveData.recordName.Count; i++)
                {
                    Func_Json.instance.recordName.Add(saveData.recordName[i]);
                }
                for (int i = 0; i < saveData.recordNamePos.Count; i++)
                {
                    Func_Json.instance.recordNamePos.Add(saveData.recordNamePos[i]);
                }
            }
        }
    }
    public void JsonSave()
    {
        SaveData saveData = new SaveData();

        for (int i = 0; i < 10; i++)
        {
            saveData.recordName.Add("테스트 데이터 no " + i);
        }

        for (int i = 0; i < 10; i++)
        {
            saveData.recordNamePos.Add(new Vector3(0,0,0));//임시 벡터값
        }
        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}
