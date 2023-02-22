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

    [SerializeField] Func_CalendarController func_CalendarController;

    [Header("캔버스")]
    [SerializeField] private GameObject diaryPanel = null;
    [SerializeField] private Sprite openTrashCan = null;
    [SerializeField] private Sprite closeTrashCan = null;
    [SerializeField] private RawImage TrashCan = null;


    public int presentNum = 0;
    private void Start()
    {
        for (int i = 0; i < profiles.Length; i++)
        {
            if (profiles[i].texture == null) profileButton[i].interactable = false;
        }

    }

    public void OnClick_Profile(int idx)
    {
        TrashCan.texture = closeTrashCan.texture;
        selectedProfileName = profileName[idx - 1].text;
        selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Diary";
        AddDiaryFiles();
        if (allFiles.Count > 0)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
        }
        else
        {
            Debug.Log("일기가 없습니다");
        }
    }
    public List<string> allFiles = new List<string>();
    private void AddDiaryFiles()
    {
        //초기화
        allFiles.Clear();
        allFilesDictionary.Clear();
        allFilesTexture.Clear();
        allFilesNames.Clear();


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

            filename = allFiles[i].Split('\\')[1].Split(".")[0];
            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);
                texture.name = filename;

                allFilesDictionary.Add(filename, texture);

                allFilesTexture.Add(texture);
            }
        }
        presentNum = allFiles.Count - 1;

        ShowPreviewDiary();
        for (int i = 0; i < allFilesTexture.Count; i++)
        {
            Debug.Log(allFilesTexture[i].name);
        }
    }
    //처음 들어갔을때 가장 최근일기 띄워주는 함수
    public void ShowPreviewDiary()
    {
        previewImg.texture = allFilesTexture[presentNum];
    }
    //뒤로가기버튼
    public void OnClick_BackButton()
    {
        allFilesDictionary.Clear();
        selectedProfilPath = "";
        selectedProfileName = "";
        previewImg.texture = null;
    }

    //이전 일기장으로 연결해주는 버튼함수
    public void OnClick_PrevDiary()
    {
        presentNum--;
        if (presentNum < 0)
        {
            presentNum = 0;
            return;
        }
        previewImg.texture = allFilesTexture[presentNum];

        string year = previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = previewImg.texture.name.Split("-")[0].Split("_")[2];

        func_CalendarController.ShowPreviewDate(year, month, day);
        func_CalendarController.ChangeCalender();
    }
    //다음일기장으로 연결해주는 버튼함수
    public void OnClick_NextDiary()
    {
        presentNum++;
        if (presentNum > allFiles.Count - 1)
        {
            presentNum = allFiles.Count - 1;
        }
        previewImg.texture = allFilesTexture[presentNum];
        string year = previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = previewImg.texture.name.Split("-")[0].Split("_")[2];
        string num = previewImg.texture.name.Split("-")[1]; //일기장 개수 필요하면 Int.Parse(num)해서 사용하셈
        func_CalendarController.ShowPreviewDate(year, month, day);
        func_CalendarController.ChangeCalender();
    }

    //일기 삭제
    public void Onclick_TrashCan()
    {
        TrashCan.texture = openTrashCan.texture;
    }
    public void Onclick_CloseTrashCan()
    {
        TrashCan.texture = closeTrashCan.texture;
    }
    public void Onclick_DeleteDiary(RawImage deleltefile)
    {
        DeleteDiary(deleltefile);
    }

    public void DeleteDiary(RawImage deleltefile)
    {
        Manager_Main.Instance.GetAudio().PlaySound("CloseDelete", SoundType.Diary, gameObject, false, true);
        string fileTextureName = deleltefile.texture.name;

        string fileName = selectedProfilPath + "/" + fileTextureName + ".png";

        if (File.Exists(fileName))
        {
            try
            {
                Debug.Log(fileName);
                File.Delete(fileName);
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
                return;
            }

        }
        diaryPanel.SetActive(false);
        AddDiaryFiles();
        diaryPanel.SetActive(true);

        TrashCan.texture = closeTrashCan.texture;
    }
}