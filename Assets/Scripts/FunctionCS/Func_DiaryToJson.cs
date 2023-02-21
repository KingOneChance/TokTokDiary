using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// This script will be on mainMgr
/// </summary>
public class Func_DiaryToJson : MonoBehaviour
{
    [SerializeField] private string filePath = Application.dataPath + "/DiaryData.json"; //프로필로 수정해야함
    [SerializeField] private List<string> recordFilesNames = new List<string>();
    [SerializeField] private List<Vector2> recordFilesPos = new List<Vector2>();
    [SerializeField] private Data_Diary saveData;
    [SerializeField] private Data_Diary loadData;

    private void Start()
    {
        saveData = new Data_Diary();
        loadData = new Data_Diary();
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
        else return null;
    }
    public void AddRecordFileName(string recordName) => recordFilesNames.Add(recordName);
    public void AddRecordPos(Vector2 recordPos) => recordFilesPos.Add(recordPos);
    public void SetProfileName(string name) => filePath = name; //일기 저장할 때 프로필 네임, 일기 불러오기 때 
    public void SaveJson() => SaveData(saveData);
    public Data_Diary LoadRecord() => loadData = LoadData(filePath);
}