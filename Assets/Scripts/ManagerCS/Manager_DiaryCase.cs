using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_DiaryCase : MonoBehaviour
{
    [SerializeField] GameObject[] panels = null;
    [Header("프로필 고르기")]
    [SerializeField] RawImage[] profiles = null;
    [SerializeField] Button[] profileButton = null;
    [SerializeField] TextMeshProUGUI[] profileName = null;

    [Header("날짜고르기")]
    [SerializeField] public RawImage previewImg = null;
    [SerializeField] public TextMeshProUGUI previewText = null;

    public Dictionary<string, Texture> allFilesDictionary = new Dictionary<string, Texture>();
    public List<Texture> allFilesTexture = new List<Texture>();
    public List<string> allFilesNames = new List<string>();
    [SerializeField] private string selectedProfileName = "";
    [SerializeField] private string selectedProfilPath = "";

    Func_CalendarController func_CalendarController;

    public int presentNum = 0;
    private void Start()
    {
        for (int i = 0; i < profiles.Length; i++)
        {
            if (profiles[i].texture == null) profileButton[i].interactable = false;
        }
        func_CalendarController = FindObjectOfType<Func_CalendarController>();   
    }

    public void OnClick_Profile(int idx)
    {
        selectedProfileName = profileName[idx - 1].text;
        selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Diary";
        AddDiaryFiles();
        panels[0].SetActive(false);
        panels[1].SetActive(true);
        
    }
    public List<string> allFiles = new List<string>();
    private void AddDiaryFiles()
    {

        allFiles.AddRange(Directory.GetFiles(selectedProfilPath, "*.png", SearchOption.AllDirectories));

        string filename = "";
        if (allFiles.Count == 0)
        {
            previewText.text = "일기가 없습니다";
            return;
        }
        previewText.gameObject.SetActive(false);


        for (int i = 0; i < allFiles.Count; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);
            filename = allFiles[i].Split("\\")[1].Split(".")[0];
            Debug.Log(filename);
            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);
                texture.name = filename;
                if (allFilesDictionary.ContainsKey(filename))
                {
                    continue;
                }
                allFilesDictionary.Add(filename, texture);
            }
        }
        presentNum = allFiles.Count-1;
        ShowPreviewDiary();
    }

    public void OnClick_BackButton()
    {
        allFilesDictionary.Clear();
        selectedProfilPath = "";
        selectedProfileName = "";
        previewImg.texture = null;
    }

    public void ShowPreviewDiary()
    {
        DateTime today = DateTime.Today;

        string month = today.Month.ToString();
        string year = today.Year.ToString();
        string day = today.Day.ToString();

        string fileName = year + "_" + month + "_" + day;


        if (allFilesDictionary.ContainsKey(fileName+"-1"))
        {
            previewImg.texture = allFilesDictionary[fileName+"-1"];
        }
        
    }
    public void OnClick_PrevDiary()
    {
        presentNum--;
        if (presentNum < 0)
        {
            presentNum = 0;
            return;
        }
        previewImg.texture = allFilesTexture[presentNum];
/*        func_CalendarController.preViewDate[0].text = ;
        func_CalendarController.preViewDate[1].text = ;
        func_CalendarController.preViewDate[2].text = ;*/
    }
    public void OnClick_NextDiary()
    {
        presentNum++;
        if (presentNum > allFiles.Count-1)
        {
            presentNum = allFiles.Count-1;
        }
        previewImg.texture = allFilesTexture[presentNum];
    }



}