using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
public class Func_DIarySave : Func_SaveSticker
{
    [SerializeField] private bool canSave = false;
    [SerializeField] private string profileName = "";
    [SerializeField] private List<string> recordFileNames = new List<string>();
    [SerializeField] private List<int> recordUsedNum = new List<int>();
    [SerializeField] private List<int> bubbleUsedNum = new List<int>();
    [SerializeField] private List<int> freeUsedNum = new List<int>();
    [SerializeField] private List<int> gunUsedNum = new List<int>();
    [SerializeField] private Func_DiaryInventory func_DiaryInventory = null;
    [SerializeField] private UI_PictureDiary uI_PictureDiary = null;

    // fix Func_SaveSticker location
    protected override void Start()
    {
        savePath = Application.persistentDataPath;
        uI_PictureDiary = FindObjectOfType<UI_PictureDiary>();
        func_DiaryInventory = FindObjectOfType<Func_DiaryInventory>();
        //calculate all position
        saveImageRect = saveImage.GetComponent<RectTransform>();
        startXPos = saveImageRect.rect.position.x + 960 + 244;
        startYPos = saveImageRect.rect.position.y + 540;
        widthValue = (int)saveImageRect.rect.width;
        heightValue = (int)saveImageRect.rect.height;

        saveFileName = DateTime.Now.ToString("yyyy_MM_dd");
    }

    public void AddRecordList(string path)
    {
        recordFileNames.Add(path);
    }

    #region OnClicks
    public void Onclick_Capture()
    {

    }
    public void OnClick_SetProfileName()
    {
        profileName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void OnClick_SetCansave(bool value) //set canSave State; 프로필 선택시 트루 , 프로필 선택창 닫기시 폴스 
    {
        if (value == false)
            canSave = true;
        else
            canSave = false;
    }
    //폴더 갯수
    public int GetDirecotoryCount(string filePath)
    {
        int count = 0;
        try
        {
            if (Directory.Exists(filePath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                foreach (var item in directoryInfo.GetDirectories())
                {
                    count++;

                }
            }
        }
        catch
        {; }
        return count;
    }
    public void OnClick_Save()
    {
        if (GetDirecotoryCount(Application.persistentDataPath + "/" + "Profile") == 0)
        {
            return;
        }
        StartCoroutine(Co_SaveEndLoadScene());
    }
    #endregion 
    IEnumerator Co_SaveEndLoadScene()
    {
        isSaveDone = false;
        if (canSave == true)
        {
            if (recordFileNames.Count == 0)
                base.SaveTexture(StickerType.Diary, profileName);
            else
            {
                base.SaveTexture(StickerType.Diary, profileName, true);
            }
        }
        //저장 완료시 씬전환
        yield return new WaitUntil(() => isSaveDone == true);
        //사용한 스티커 삭제하기
        for (int i = 0; i < recordUsedNum.Count; i++)
        {
            string signBuffer = func_DiaryInventory.GetRecordingSignList(recordUsedNum[i]);
            string stickerBuffer = func_DiaryInventory.GetRecordingStickerList(recordUsedNum[i]);
            DeleteFile(signBuffer);
            DeleteFile(stickerBuffer);
        }

        for (int i = 0; i < freeUsedNum.Count; i++)
        {
            string freeBuffer = func_DiaryInventory.GetFreeStickerList(freeUsedNum[i]);
            DeleteFile(freeBuffer);
        }
        string[] allfiles;
        if (Directory.Exists(Application.persistentDataPath + "/BubbleGunSticker"))
        {
            allfiles = Directory.GetFiles(Application.persistentDataPath + "/BubbleGunSticker", "*.png", SearchOption.AllDirectories);
            for (int i = 0; i < gunUsedNum.Count; i++)
            {

#if UNITY_EDITOR
                Manager_Main.Instance.UseSticker(allfiles[gunUsedNum[i]].Split("BubbleGunSticker\\")[1].Split(".")[0]);
#elif UNITY_ANDROID
                Manager_Main.Instance.UseSticker(allfiles[gunUsedNum[i]].Split("BubbleGunSticker/")[1].Split(".")[0]);
#endif
#if UNITY_EDITOR
                if (int.Parse(Manager_Main.Instance.GetCurStickerUserCount(allfiles[gunUsedNum[i]].Split("BubbleGunSticker\\")[1].Split(".")[0])) == 0)
#elif UNITY_ANDROID
                if (int.Parse(Manager_Main.Instance.GetCurStickerUserCount(allfiles[gunUsedNum[i]].Split("BubbleGunSticker/")[1].Split(".")[0]))==0)
#endif
                {
                    DeleteFile(allfiles[gunUsedNum[i]]);
                }
            }
        }
        if (Directory.Exists(Application.persistentDataPath + "/BubbleSticker"))
        {
            allfiles = Directory.GetFiles(Application.persistentDataPath + "/BubbleSticker", "*.png", SearchOption.AllDirectories);
            for (int i = 0; i < bubbleUsedNum.Count; i++)
            {
#if UNITY_EDITOR
                Manager_Main.Instance.UseSticker(allfiles[bubbleUsedNum[i]].Split("BubbleSticker\\")[1].Split(".")[0]);
#elif UNITY_ANDROID
                Manager_Main.Instance.UseSticker(allfiles[bubbleUsedNum[i]].Split("BubbleSticker/")[1].Split(".")[0]);
#endif
#if UNITY_EDITOR
                if (int.Parse(Manager_Main.Instance.GetCurStickerUserCount(allfiles[bubbleUsedNum[i]].Split("BubbleSticker\\")[1].Split(".")[0])) == 0)
#elif UNITY_ANDROID
               if (int.Parse(Manager_Main.Instance.GetCurStickerUserCount(allfiles[bubbleUsedNum[i]].Split("BubbleSticker/")[1].Split(".")[0])) == 0)
#endif
                {
                    DeleteFile(allfiles[bubbleUsedNum[i]]);
                }
            }
        }


        //씬전환
        Debug.Log("세이브 상태 :" + isSaveDone);
        Cursor.SetCursor(default, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("PictureDiary");
    }
    public void SaveTempCapture()
    {
        StartCoroutine(Co_ScreenShotFrame());
    }
    private IEnumerator Co_ScreenShotFrame()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tex = new Texture2D(widthValue, heightValue, TextureFormat.RGB24, false);

        Rect rex = new Rect(startXPos, startYPos, widthValue, heightValue);

        tex.ReadPixels(rex, 0, 0);
        tex.Apply();

        //
        Debug.Log("BGC : " + backGroundColor);
        Texture2D newTex = new Texture2D(widthValue, heightValue);
        for (int x = 0; x < widthValue; x++)
        {
            for (int y = 0; y < heightValue; y++)
            {
                Color pixelColor = tex.GetPixel(x, y);
                if (pixelColor != backGroundColor)
                {
                    newTex.SetPixel(x, y, pixelColor);
                }
                else
                {
                    newTex.SetPixel(x, y, Color.clear);
                }
            }
        }
        newTex.Apply();
        //
        saveTemp.texture = newTex;
        uI_PictureDiary.OnClick_OpenProfileButton();
    }

    private void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
    //람다식 전환하기 => 리펙토링 때
    public List<int> GetUsedRecordList()
    {
        return recordUsedNum;
    }
    public List<int> GetUsedFreeList()
    {
        return freeUsedNum;
    }
    public List<int> GetUsedGunList()
    {
        return gunUsedNum;
    }
    public List<int> GetUsedBubbleList()
    {
        return bubbleUsedNum;
    }
    public int GetUsedRecordNum()
    {
        return recordUsedNum.Count;
    }
    public int GetUsedFreeNum()
    {
        return freeUsedNum.Count;
    }
    public int GetUsedGunNum()
    {
        return gunUsedNum.Count;
    }
    public int GetUsedBubbleNum()
    {
        return bubbleUsedNum.Count;
    }
    public void SetUsedRecordNum(int num)
    {
        recordUsedNum.Add(num);
        Debug.Log("사용된 녹음 스티커 수 " + recordUsedNum.Count);
    }
    public void SetUsedFreeNum(int num)
    {
        freeUsedNum.Add(num);
        Debug.Log("사용된 프리 스티커 수 " + freeUsedNum.Count);
    }
    public void SetUsedGunNum(int num)
    {
        gunUsedNum.Add(num);
        Debug.Log("사용된 프리 스티커 수 " + gunUsedNum.Count);
    }
    public void SetUsedBubbleNum(int num)
    {
        bubbleUsedNum.Add(num);
        Debug.Log("사용된 프리 스티커 수 " + bubbleUsedNum.Count);
    }
    public void RemoveRecordAtList(int num)
    {
        recordUsedNum.Remove(num);
    }
    public void RemoveFreeAtList(int num)
    {
        freeUsedNum.Remove(num);
    }
    public void RemoveGunAtList(int num)
    {
        gunUsedNum.Remove(num);
    }
    public void RemoveBubbleAtList(int num)
    {
        bubbleUsedNum.Remove(num);
        Debug.Log(bubbleUsedNum.Count + "<= 지워지고 난 후 숫자");
    }
}
