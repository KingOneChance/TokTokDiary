using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_DiaryCase : MonoBehaviour
{
    [SerializeField] Button goToMain = null;
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

    [SerializeField] private GameObject diaryPanel = null;
    [SerializeField] private Sprite openTrashCan = null;
    [SerializeField] private Sprite closeTrashCan = null;
    [SerializeField] private RawImage TrashCan = null;
    [SerializeField] private Button prevBtn = null; 
    [SerializeField] private Button nextBtn = null;

 
 
    [Header("===JsonData===")]
    [SerializeField] private List<string> recordFilesNames = new List<string>();
    [SerializeField] private List<Vector2> recordFilesPos = new List<Vector2>();
    [SerializeField] private List<GameObject> buttonPool = new List<GameObject>(); //��ư ������ƮǮ
    [SerializeField] private List<string> jsonFiles = new List<string>();
    [SerializeField] private Data_Diary dataDiary = null;
    [SerializeField] private GameObject buttonPrefab = null;

    public int presentNum = 0;
    private void Start()
    {
        dataDiary = new Data_Diary();

        nextBtn.gameObject.SetActive(false);

    }
    private void OnEnable()
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
#if UNITY_ANDROID
        selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Diary";
#else
          selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Diary";
#endif
        //������ ���� 
        Manager_Main.Instance.func_DiaryToJson.SetProfileName(Application.persistentDataPath + "/Profile/" + selectedProfileName);
        savedRecordFinePath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Records";
        savedJsonFilePath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Jsons";
        AddDiaryFiles();
        if (allFiles.Count > 0)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            
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
#if UNITY_ANDROID
            filename = allFiles[i].Split('/')[11].Split('.')[0];
#else
            filename = allFiles[i].Split('\\')[1].Split(".")[0];
#endif

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
            //���̽� ���� n��°�� �ִ� ���ڵ����ϰ� ������ �� �ѱ��
            for (int i = 0; i < jsonFiles.Count; i++)
            {
                //���̽� ���� n��° �̸� �������ֱ�
                Manager_Main.Instance.func_DiaryToJson.SetProfileName(jsonFiles[i]);
                //���̽� ���� n��° �ڷ� ��������
                dataDiary = Manager_Main.Instance.func_DiaryToJson.LoadRecord();
                for (int j = 0; j < dataDiary.recordFileNames.Count; j++)
                {
                    recordFilesNames.Add(dataDiary.recordFileNames[j]);
                    recordFilesPos.Add(dataDiary.recordFilePos[j]);
                }
            }
        }

        presentNum = allFiles.Count - 1;

        ShowPreviewDiary();
    }
    //ó�� ������ ���� �ֱ��ϱ� ����ִ� �Լ�
    public void ShowPreviewDiary()
    {
        previewImg.texture = allFilesTexture[presentNum];
        string lastDay = previewImg.texture.name;
        string year = previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = previewImg.texture.name.Split("-")[0].Split("_")[2];
        func_CalendarController.ShowPreviewDate(year, month, day);
        FindRecordFile(lastDay);
    }
    public void FindRecordFile(string lastDay)
    {
        for (int i = 0; i < jsonFiles.Count; i++)
        {
            for (int k = 0; k < buttonPool.Count; k++)
            {
                buttonPool[k].SetActive(false);
            }
            if (jsonFiles[i].Contains(lastDay) == true)
            {
                InsertRecordButton(lastDay);
                break;
            }
        }
    }
    List<string> theDayRecordNames = new List<string>();
    List<Vector2> theDayRecordPos = new List<Vector2>();
    public void InsertRecordButton(string lastDay)
    {
        theDayRecordNames.Clear();
        theDayRecordPos.Clear();

        string[] all = Directory.GetFiles(savedRecordFinePath, "*.wav", SearchOption.AllDirectories);
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].Contains(lastDay))
                theDayRecordNames.Add(all[i]);
        }
        for(int i = 0; i < recordFilesPos.Count; i++)
        {
            if (recordFilesNames[i].Contains(lastDay))
                theDayRecordPos.Add(recordFilesPos[i]);
        }

        for (int j = 0; j < theDayRecordNames.Count; j++) //����� �ҽ� �̸�,���ڵ� ������
        {
            CheckButtonPool();
        }
    }
    public void CheckButtonPool()
    {
        //������Ʈ Ǯ�� �����ϴٸ� 
        if (buttonPool.Count < theDayRecordNames.Count)
        {  //���� �ִ� ������Ʈ Ǯ�� �׳� �ִ� ����� ������ ���̸�ŭ ����
            for (int k = 0; k < theDayRecordNames.Count - buttonPool.Count; k++)
            {
                GameObject audioPool = GameObject.Instantiate(buttonPrefab, diaryPanel.transform);
                audioPool.name = k.ToString();
                buttonPool.Add(audioPool);
            }
        }

        for(int k = 0; k<buttonPool.Count; k++)
        {
            buttonPool[k].SetActive(false);
        }


        for (int k = 0; k < buttonPool.Count; k++)
        {
            if (k < theDayRecordNames.Count) //������ƮǮ
            {
                Vector2 temp = theDayRecordPos[k];
                buttonPool[k].SetActive(true);
                buttonPool[k].GetComponent<RectTransform>().position = temp;
                buttonPool[k].GetComponent<Func_SelfAddListner>().SetLissner(theDayRecordNames[k]);
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
        goToMain.gameObject.SetActive(true);
    }

    //���� �ϱ������� �������ִ� ��ư�Լ�
    public void OnClick_PrevDiary()
    {
        presentNum--;
        nextBtn.gameObject.SetActive(true);
        if (presentNum == 0) prevBtn.gameObject.SetActive(false);


        if (presentNum < 0)
        {           
            presentNum = 0;
            return;
        }
        
        previewImg.texture = allFilesTexture[presentNum];
        FindRecordFile(previewImg.texture.name);
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
        prevBtn.gameObject.SetActive(true);
        if(presentNum == allFiles.Count -1) nextBtn.gameObject.SetActive(false);
        if (presentNum > allFiles.Count - 1)
        {
           
            presentNum = allFiles.Count - 1;
        }
        previewImg.texture = allFilesTexture[presentNum];
        FindRecordFile(previewImg.texture.name);
        string year = previewImg.texture.name.Split("-")[0].Split("_")[0];
        string month = previewImg.texture.name.Split("-")[0].Split("_")[1];
        string day = previewImg.texture.name.Split("-")[0].Split("_")[2];
        string num = previewImg.texture.name.Split("-")[1]; //�ϱ��� ���� �ʿ��ϸ� Int.Parse(num)�ؼ� ����ϼ�
        func_CalendarController.ShowPreviewDate(year, month, day);
        func_CalendarController.ChangeCalender();
    }

    //�ϱ� ����
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
        string jsonName = selectedProfilPath + "/" + fileTextureName + ".png";
        string recordName = selectedProfilPath + "/" + fileTextureName + ".png";

        DeletFolder(fileName);
        DeletFolder(jsonName);
        DeletFolder(recordName);

        diaryPanel.SetActive(false);
        AddDiaryFiles();
        diaryPanel.SetActive(true);

        TrashCan.texture = closeTrashCan.texture;
    }
    private void DeletFolder(string fileName)
    {
        if (File.Exists(fileName))
        {
            try
            {
                File.Delete(fileName);
            }
            catch (IOException e)
            {
                return;
            }
        }
    }
}