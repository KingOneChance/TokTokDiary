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
    public List<string> allFiles = new List<string>();
    [SerializeField] private string selectedProfileName = "";
    [SerializeField] private string selectedProfilPath = "";
    [SerializeField] private string savedRecordFinePath = "";
    [SerializeField] private string savedJsonFilePath = "";

    [SerializeField] Func_CalendarController func_CalendarController;

    [Header("캔버스")]

    [SerializeField] private GameObject diaryPanel = null;
    [SerializeField] private Sprite openTrashCan = null;
    [SerializeField] private Sprite closeTrashCan = null;
    [SerializeField] private RawImage TrashCan = null;

    [Header("===JsonData===")]
    [SerializeField] private List<string> recordFilesNames = new List<string>();
    [SerializeField] private List<Vector2> recordFilesPos = new List<Vector2>();
    [SerializeField] private List<GameObject> buttonPool = new List<GameObject>(); //버튼 오브젝트풀
    [SerializeField] private List<string> jsonFiles = new List<string>();
    [SerializeField] private Data_Diary dataDiary = null;
    [SerializeField] private GameObject buttonPrefab = null;


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

        TrashCan.texture = closeTrashCan.texture;

        selectedProfileName = profileName[idx - 1].text;
        selectedProfilPath = Application.persistentDataPath + "/Profile/" + selectedProfileName + "/Diary";

        //프로필 네임 
        Manager_Main.Instance.func_DiaryToJson.SetProfileName(Application.persistentDataPath + "/Profile/" + selectedProfileName);
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
            Debug.Log("일기가 없습니다");
        }
    }

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
        //일기 있을경우
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
        //제이슨 파일 불러오기 
        jsonFiles.AddRange(Directory.GetFiles(savedJsonFilePath, "*.json", SearchOption.AllDirectories));
        if (jsonFiles.Count != 0)
        {
            //제이슨 파일 n번째에 있는 레코드파일과 포지션 다 넘기기
            for (int i = 0; i < jsonFiles.Count; i++)
            {
                //제이슨 파일 n번째 이름 세팅해주기
                Manager_Main.Instance.func_DiaryToJson.SetProfileName(jsonFiles[i]);
                //제이슨 파일 n번째 자료 가져오기
                dataDiary = Manager_Main.Instance.func_DiaryToJson.LoadRecord();
                for (int j = 0; j < dataDiary.recordFileNames.Count; j++)
                {
                    Debug.Log(dataDiary.recordFileNames[j].ToString() + "카운트 개수");
                    recordFilesNames.Add(dataDiary.recordFileNames[j]);
                    recordFilesPos.Add(dataDiary.recordFilePos[j]);
                }
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
        string lastDay = previewImg.texture.name;
        Debug.Log("마지막 일기 띄움");
        FindRecordFile(lastDay);
    }
    public void FindRecordFile(string lastDay)
    {
        Debug.Log("마지막 일기 사운드 확인 띄움");
        Debug.Log(lastDay);
        for (int i = 0; i < jsonFiles.Count; i++)
        {
            Debug.Log(jsonFiles[i]);

            if (jsonFiles[i].Contains(lastDay) == true)
            {
                Debug.Log("마지막 일기 사운드 있음");
                InsertRecordButton(lastDay);
                break;
            }
            else
                Debug.Log("마지막 일기 사운드 없음");
        }
    }
    List<string> theDayRecordNames = new List<string>();
    List<Vector2> theDayRecordPos = new List<Vector2>();
    public void InsertRecordButton(string lastDay)
    {
        theDayRecordNames.Clear();
        theDayRecordPos.Clear();
        Debug.Log(savedRecordFinePath);
        Debug.Log(lastDay);
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

        for (int j = 0; j < theDayRecordNames.Count; j++) //오디오 소스 이름,레코드 포지션
        {
            CheckButtonPool();
        }
    }
    public void CheckButtonPool()
    {
        Debug.Log("오브젝트풀 생성");
        //오브젝트 풀이 부족하다면 
        if (buttonPool.Count < theDayRecordNames.Count)
        {  //현재 있는 오브젝트 풀과 그날 있는 오디오 개수의 차이만큼 생성
            Debug.Log("오브젝트풀 부족");
            for (int k = 0; k < theDayRecordNames.Count - buttonPool.Count; k++)
            {
                Debug.Log("오브젝트풀 생성중");

                GameObject audioPool = GameObject.Instantiate(buttonPrefab, diaryPanel.transform);
                audioPool.name = k.ToString();
                buttonPool.Add(audioPool);
            }
        }
        Debug.Log("오브젝트풀 생성완료");
        for(int k = 0; k<buttonPool.Count; k++)
        {
            buttonPool[k].SetActive(false);
        }


        for (int k = 0; k < buttonPool.Count; k++)
        {
            Debug.Log("오브젝트풀 처리중");
            if (k < theDayRecordNames.Count) //오브젝트풀
            {
                Debug.Log(k);
                Debug.Log(theDayRecordNames[k].ToString());
                Debug.Log(buttonPool.Count);
                Vector2 temp = theDayRecordPos[k];
                buttonPool[k].SetActive(true);
                buttonPool[k].GetComponent<RectTransform>().position = temp;
                buttonPool[k].GetComponent<Func_SelfAddListner>().SetLissner(theDayRecordNames[k]);
            }
        }
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
        FindRecordFile(previewImg.texture.name);
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
        FindRecordFile(previewImg.texture.name);
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
        string jsonName = selectedProfilPath + "/" + fileTextureName + ".png";
        string recordName = selectedProfilPath + "/" + fileTextureName + ".png";

        DeletFolder(fileName);

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