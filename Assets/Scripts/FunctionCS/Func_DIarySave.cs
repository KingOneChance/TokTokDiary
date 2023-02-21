using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Func_DIarySave : Func_SaveSticker
{
    [SerializeField] private UI_PictureDiary uI_PictureDiary = null;
    [SerializeField] private bool canSave = false;
    [SerializeField] private string profileName = "";
    [SerializeField] private List<string> recordFileNames = new List<string>();

    // fix Func_SaveSticker location
    protected override void Start()
    {
        uI_PictureDiary = FindObjectOfType<UI_PictureDiary>();
        savePath = Application.persistentDataPath;
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
        //base.OnClick_SaveImgae(StickerType.FreeSticker);
        isSaveDone = false;
        if (canSave == true)
        {
            if (recordFileNames.Count == 0)
                base.SaveTexture(StickerType.Diary, profileName);
            else
                base.SaveTexture(StickerType.Diary, profileName, true);
        }
        //���� �Ϸ�� ����ȯ
        yield return new WaitUntil(() => isSaveDone == true);
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
}
