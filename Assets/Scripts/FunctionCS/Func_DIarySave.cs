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
    public void OnClick_Save()
    {
        //제이슨 저장 02.21 김원찬 해야할 일
       // Manager_Main.Instance.func_DiaryToJson.SetProfileName(profileName);
       // Manager_Main.Instance.func_DiaryToJson.SaveJson();

        //base.OnClick_SaveImgae(StickerType.FreeSticker);
        if (canSave == true)
        {
            base.SaveTexture(StickerType.Diary, profileName);
        }
        //저장 완료시 씬전환
        StartCoroutine(Co_SaveEndLoadScene());
    }
    IEnumerator Co_SaveEndLoadScene()
    {
        isSaveDone = false;
        yield return new WaitUntil(() => isSaveDone == true);
        //씬전환
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
