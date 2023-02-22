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
    [Header("������ ����")]
    [SerializeField] RawImage[] profiles = null;
    [SerializeField] Button[] profileButton = null;
    [SerializeField] TextMeshProUGUI[] profileName = null;

    [Header("��¥����")]
    [SerializeField] public RawImage previewImg = null;
    [SerializeField] public TextMeshProUGUI previewText = null;

    public Dictionary<string, Texture> allFilesDictionary = new Dictionary<string, Texture>();
    public List<Texture> allFilesTexture = new List<Texture>();
    public List<string> allFilesNames = new List<string>();
    public List<string> allFiles = new List<string>();
    [SerializeField] private string selectedProfileName = "";
    [SerializeField] private string selectedProfilPath = "";
    [SerializeField] private string savedRecordFinePath = "";
    [SerializeField] private string savedJsonFilePath = "";

    [SerializeField] Func_CalendarController func_CalendarController;

    [Header("ĵ����")]
    [SerializeField] GameObject diaryPanel = null;
    [Header("===JsonData===")]
    [SerializeField] private List<string> recordFilesNames = new List<string>();
    [SerializeField] private List<Vector2> recordFilesPos = new List<Vector2>();
    [SerializeField] private Data_Diary dataDiary = null;
    [SerializeField] private GameObject buttonPrefab = null;
    [SerializeField] private List<GameObject> buttonPool = new List<GameObject>(); //��ư ������ƮǮ
    [SerializeField] private List<string> jsonFiles = new List<string>();


    public int presentNum = 0;
    private void Start()
    {
        dataDiary = new Data_Diary();
        for (int i = 0; i < profiles.Length; i++)
        {
            if (profiles[i].texture == null) profileButton[i].interactable = false;
        }
    }

    public void OnClick_Profile(int idx)
    {
        selectedProfileName = profileName[idx - 1].text;
        selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Diary";

        //������ ���� 
        Manager_Main.Instance.func_DiaryToJson.SetProfileName(selectedProfileName);
        savedRecordFinePath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Records";
        savedJsonFilePath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Jsons";
        AddDiaryFiles();
        if (allFiles.Count > 0)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
        }
        else
        {
            Debug.Log("�ϱⰡ �����ϴ�");
        }
    }

    private void AddDiaryFiles()
    {
        //�ʱ�ȭ
        allFiles.Clear();
        allFilesDictionary.Clear();
        allFilesTexture.Clear();
        allFilesNames.Clear();


        allFiles.AddRange(Directory.GetFiles(selectedProfilPath, "*.png", SearchOption.AllDirectories));

        string filename = "";
        if (allFiles.Count == 0)
        {
            previewText.text = "�ϱⰡ �����ϴ�";
            return;
        }
        //�ϱ� �������
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
        //���̽� ���� �ҷ����� 
        jsonFiles.AddRange(Directory.GetFiles(savedJsonFilePath, "*.json", SearchOption.AllDirectories));
        if (jsonFiles.Count != 0)
        {
            dataDiary = Manager_Main.Instance.func_DiaryToJson.LoadRecord();
            for (int i = 0; i < jsonFiles.Count; i++)
            {
                recordFilesNames.Add(dataDiary.recordFileNames[i]);
                recordFilesPos.Add(dataDiary.recordFilePos[i]);
            }
        }

        presentNum = allFiles.Count - 1;

        ShowPreviewDiary();
        for (int i = 0; i < allFilesTexture.Count; i++)
        {
            Debug.Log(allFilesTexture[i].name);
        }
    }
    //ó�� ������ ���� �ֱ��ϱ� ����ִ� �Լ�
    public void ShowPreviewDiary()
    {
        previewImg.texture = allFilesTexture[presentNum];
        string lastDay = previewImg.texture.name.Split("-")[0];
        FindRecordFile(lastDay);
    }
    public void FindRecordFile(string lastDay)
    {
        for (int i = 0; i < jsonFiles.Count; i++)
        {
            if (jsonFiles.Contains(lastDay) == true)
            {
                InsertRecordButton(lastDay);
            }
            break;
        }
    }
    public void InsertRecordButton(string lastDay)
    {
        for (int j = 0; j < recordFilesNames.Count; j++) //����� �ҽ� �̸�,���ڵ� ������
        { //������ƮǮ ����ؾ���
            if (recordFilesNames[j].Contains(lastDay) == true)
            {
                CheckButtonPool();
                break;
            }
        }
    }
    public void CheckButtonPool()
    {
        //������Ʈ Ǯ�� �����ϴٸ� 
        if (buttonPool.Count < recordFilesNames.Count)
        {  //���� �ִ� ������Ʈ Ǯ�� �׳� �ִ� ����� ������ ���̸�ŭ ����
            for (int k = 0; k < recordFilesNames.Count - buttonPool.Count; k++)
            {
                GameObject audioPool = GameObject.Instantiate(buttonPrefab);
                buttonPool.Add(audioPool);
            }
        }
        else
        {
            for (int k = 0; k < buttonPool.Count; k++)
            {
                if (k <= recordFilesNames.Count) //������ƮǮ
                {
                    buttonPool[k].SetActive(true);
                    buttonPool[k].GetComponent<RectTransform>().position = recordFilesPos[k];
                    buttonPool[k].GetComponent<Button>().onClick.RemoveAllListeners();
                    buttonPool[k].GetComponent<Button>().onClick.AddListener(() =>
                    Manager_Main.Instance.GetAudio().PlayLocalSound(recordFilesNames[k], buttonPool[k], false, false)
                    );
                }
                else
                    buttonPool[k].SetActive(false);
            }
        }
    }

    //�ڷΰ����ư
    public void OnClick_BackButton()
    {
        allFilesDictionary.Clear();
        selectedProfilPath = "";
        selectedProfileName = "";
        previewImg.texture = null;
    }

    //���� �ϱ������� �������ִ� ��ư�Լ�
    public void OnClick_PrevDiary()
    {
        presentNum--;
        if (presentNum < 0)
        {
            presentNum = 0;
            return;
        }
        previewImg.texture = allFilesTexture[presentNum];
        FindRecordFile(previewImg.texture.name.Split("-")[0]);
        string year = previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = previewImg.texture.name.Split("-")[0].Split("_")[2];

        func_CalendarController.ShowPreviewDate(year, month, day);
        func_CalendarController.ChangeCalender();
    }
    //�����ϱ������� �������ִ� ��ư�Լ�
    public void OnClick_NextDiary()
    {
        presentNum++;
        if (presentNum > allFiles.Count - 1)
        {
            presentNum = allFiles.Count - 1;
        }
        previewImg.texture = allFilesTexture[presentNum];
        FindRecordFile(previewImg.texture.name.Split("-")[0]);
        string year = previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = previewImg.texture.name.Split("-")[0].Split("_")[2];
        string num = previewImg.texture.name.Split("-")[1]; //�ϱ��� ���� �ʿ��ϸ� Int.Parse(num)�ؼ� ����ϼ�
        func_CalendarController.ShowPreviewDate(year, month, day);
        func_CalendarController.ChangeCalender();
    }

    //�ϱ� ����
    public void Onclick_DeleteDiary(RawImage deleltefile)
    {
        DeleteDiary(deleltefile);
    }

    public void DeleteDiary(RawImage deleltefile)
    {
        Manager_Main.Instance.GetAudio().PlaySound("CloseDelete", SoundType.Diary, gameObject, false, true);
        string fileTextureName = deleltefile.texture.name;

        string fileName = selectedProfilPath + "/" + fileTextureName + ".png";
        string jsonName = selectedProfilPath + "/" + fileTextureName + ".png";
        string recordName = selectedProfilPath + "/" + fileTextureName + ".png";

        DeletFolder(fileName);
        
        diaryPanel.SetActive(false);
        AddDiaryFiles();
        diaryPanel.SetActive(true);


    }
    private void DeletFolder(string fileName)
    {
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
    }
}