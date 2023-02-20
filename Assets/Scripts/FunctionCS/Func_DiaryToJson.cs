using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Func_DiaryToJson : MonoBehaviour
{
    [SerializeField] private string filePath = Application.dataPath + "/DiaryData.json"; //프로필로 수정해야함
    [SerializeField] private List<string> recordFilesNames = new List<string>(); 
    [SerializeField] private List<Vector2> recordFilesPos = new List<Vector2>();
    [SerializeField] private Data_Diary data;

    private void Start()
    {
        data = new Data_Diary();
    }
    private void SaveData(Data_Diary data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, jsonData);
    }
    private Data_Diary LoadData(string path)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<Data_Diary>(jsonData);
        }
        else
        {
            return null;
        }
    }
    public void AddRecordFileName(string recordName)
    {
        recordFilesNames.Add(recordName);
    }
    public void AddRecordPos(Vector2 recordPos)
    {
        recordFilesPos.Add(recordPos);
    }
    public void SaveJson()
    {
        SaveData(data);
    }
}