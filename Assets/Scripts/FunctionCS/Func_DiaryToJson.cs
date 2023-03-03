using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
/// <summary>
/// This script will be on mainMgr
/// </summary>
public class Func_DiaryToJson : MonoBehaviour
{
    [SerializeField] private string filePath; //�����ʷ� �����ؾ���
    [SerializeField] private string jsonTempName; //�����ʷ� �����ؾ���
    [SerializeField] private List<string> recordFilesNames = new List<string>();
    [SerializeField] private List<Vector2> recordFilesPos = new List<Vector2>();
    [SerializeField] private List<GameObject> recordObject = new List<GameObject>();
    [SerializeField] private Data_Diary saveData;
    [SerializeField] private Data_Diary loadData;

    private void Start()
    {
        filePath = Application.dataPath + "/DiaryData.json";
        saveData = new Data_Diary();
        loadData = new Data_Diary();
    }
    private void SaveData()
    {
        for (int i = 0; i < recordFilesNames.Count; i++)
        {
            int recordNum = i + 1;
            string time = DateTime.Now.ToString("yyyy_MM_dd");
            recordFilesPos.Add(recordObject[i].GetComponent<RectTransform>().position);
            string lastFilePos = recordFilesNames[i];
            string nowFilePos = filePath.Split("Jsons")[0] + "Records/" + jsonTempName + "-" + recordNum + ".wav";
            //Diary Case���� ����ϱ� ����
            recordFilesNames[i] = nowFilePos;
            File.Move(lastFilePos, nowFilePos);
        }
        saveData.recordFilePos = recordFilesPos;
        saveData.recordFileNames = recordFilesNames;
        string jsonData = JsonUtility.ToJson(saveData);
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
    public void AddRecordPos(GameObject recordPos) => recordObject.Add(recordPos);
    public void SetProfileName(string name) => filePath = name; //�ϱ� ������ �� ������ ����, �ϱ� �ҷ����� �� 
    public void SaveJson() => SaveData();
    public Data_Diary LoadRecord() => loadData = LoadData(filePath);
    public void SetJosonFileName(string name) => jsonTempName = name;
    public void DeleteListNumber(int num)
    {
        
        for(int i = 0; i < recordObject.Count; i++)
        {
            if (int.Parse(recordObject[i].name.Split("(")[0]) ==num)
            {
                recordFilesNames.Remove(recordFilesNames[i]);
                recordObject.Remove(recordObject[i]);
            }
        }
    }
}