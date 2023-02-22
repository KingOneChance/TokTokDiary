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

    public void OnClick_SetCansave(bool value) //set canSave State; ������ ���ý� Ʈ�� , ������ ����â �ݱ�� ���� 
    {
        if (value == false)
            canSave = true;
        else
            canSave = false;
    }
    public void OnClick_Save()
    {
        StartCoroutine(Co_SaveEndLoadScene());
    }
    #endregion 
    IEnumerator Co_SaveEndLoadScene()
    {
        Debug.Log("���� �ڷ�ƾ ����");
       //base.OnClick_SaveImgae(StickerType.FreeSticker);
       isSaveDone = false;
        if (canSave == true)
        {
            Debug.Log("���� if ����");
            if (recordFileNames.Count == 0)
                base.SaveTexture(StickerType.Diary, profileName);
            else
            {
                Debug.Log("else ��");
                base.SaveTexture(StickerType.Diary, profileName, true);
            }
        }
        Debug.Log("���� �Ϸ�");
        //���� �Ϸ�� ����ȯ
        yield return new WaitUntil(() => isSaveDone == true);
        //����� ��ƼĿ �����ϱ�
        Debug.Log("recordUsedNum ���� : " + recordUsedNum.Count);

        for (int i = 0; i < recordUsedNum.Count; i++)
        {
            Debug.Log("recordUsedNum ���� : " + recordUsedNum.Count);

            string signBuffer = func_DiaryInventory.GetRecordingSignList(recordUsedNum[i]);
            string stickerBuffer = func_DiaryInventory.GetRecordingStickerList(recordUsedNum[i]);
            DeleteFile(signBuffer);
            DeleteFile(stickerBuffer);
        }

        for (int i = 0; i < freeUsedNum.Count; i++)
        {
            Debug.Log("freeUsedNum ���� : " + freeUsedNum.Count);

            string freeBuffer = func_DiaryInventory.GetFreeStickerList(freeUsedNum[i]);
            DeleteFile(freeBuffer);
        }

        string[] allfiles = Directory.GetFiles(Application.persistentDataPath + "/BubbleFreeSticker", "*.png", SearchOption.AllDirectories);
        
        for (int i = 0; i < gunUsedNum.Count; i++)
        {
            if (int.Parse(Manager_Main.Instance.GetCurStickerUserCount(allfiles[gunUsedNum[i]].Split("BubbleFreeSticker")[1].Split(".")[0])) == 1) // ���ڰ� ��ƼĿ �̸�����
            {
                string freeBuffer = func_DiaryInventory.GetGunStickerList(gunUsedNum[i]);
                DeleteFile(freeBuffer);
            }
            else
            {
                Manager_Main.Instance.UseSticker(allfiles[gunUsedNum[i]].Split("BubbleFreeSticker")[1].Split(".")[0]);
            }
        }
        allfiles = Directory.GetFiles(Application.persistentDataPath + "/BubbleGun", "*.png", SearchOption.AllDirectories);
        for (int i = 0; i < bubbleUsedNum.Count; i++)
        {
            if (int.Parse(Manager_Main.Instance.GetCurStickerUserCount(allfiles[bubbleUsedNum[i]].Split("BubbleGun")[1].Split(".")[0])) == 1) //����
            {
                string freeBuffer = func_DiaryInventory.GetbubbleStickerList(bubbleUsedNum[i]);
                DeleteFile(freeBuffer);
            }
            else
            {
                Manager_Main.Instance.UseSticker(allfiles[bubbleUsedNum[i]].Split("BubbleGun")[1].Split(".")[0]);
            }
        }
        //����ȯ
        Debug.Log("���̺� ���� :" + isSaveDone);
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
    public void SetUsedRecordNum(int num)
    {
        recordUsedNum.Add(num);
        Debug.Log("���� ���� ��ƼĿ �� " + recordUsedNum.Count);
    }
    public void SetUsedFreeNum(int num)
    {
        freeUsedNum.Add(num);
        Debug.Log("���� ���� ��ƼĿ �� " + freeUsedNum.Count);
    }
    public void SetUsedGunNum(int num)
    {
        gunUsedNum.Add(num);
        Debug.Log("���� ���� ��ƼĿ �� " + gunUsedNum.Count);
    }
    public void SetUsedBubbleNum(int num)
    {
        bubbleUsedNum.Add(num);
        Debug.Log("���� ���� ��ƼĿ �� " + bubbleUsedNum.Count);
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
    }
}
